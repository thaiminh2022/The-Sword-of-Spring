using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TheSwordOfSpring.StatSystem
{
    [Serializable]
    public class Stat
    {
        public float BaseValue;
        public float MaxValue = float.MaxValue - 1;


        protected bool isDirty = true;
        protected float lastBaseValue;

        protected float _value;

        public float Value
        {
            get
            {
                if (isDirty || lastBaseValue != BaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        public event EventHandler<ModifierEventArgs> OnModifierAdded;
        public event EventHandler<ModifierEventArgs> OnModifierRemove;
        public event EventHandler<ModifierEventArgs> OnModifierChange;



        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public Stat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public Stat(float baseValue, float maxValue = float.MaxValue - 1) : this()
        {
            BaseValue = baseValue;
            MaxValue = maxValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            var eventArgs = new ModifierEventArgs(mod, this);

            statModifiers.Add(mod);
            OnModifierChange?.Invoke(this, eventArgs);
            OnModifierAdded?.Invoke(this, eventArgs);
        }



        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                var eventArgs = new ModifierEventArgs(mod, this);

                isDirty = true;

                OnModifierChange?.Invoke(this, eventArgs);
                OnModifierRemove?.Invoke(this, eventArgs);
                return true;

            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

            if (numRemovals > 0)
            {
                var eventArgs = new ModifierEventArgs(null, this);
                OnModifierChange?.Invoke(this, eventArgs);

                isDirty = true;

                OnModifierRemove?.Invoke(this, eventArgs);
                return true;
            }
            return false;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; //if (a.Order == b.Order)
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            statModifiers.Sort(CompareModifierOrder);

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }
            // Clamp final value
            finalValue = Math.Clamp(finalValue, float.MinValue + 1, MaxValue);
            // Workaround for float calculation errors, like displaying 12.00001 instead of 12
            return (float)Math.Round(finalValue, 4);
        }
    }
    public class ModifierEventArgs : EventArgs
    {
        public StatModifier addedStatModifier;
        public Stat senderStat;

        public ModifierEventArgs(StatModifier addedStatModifier, Stat senderStat)
        {
            this.addedStatModifier = addedStatModifier;
            this.senderStat = senderStat;
        }
    }
}


