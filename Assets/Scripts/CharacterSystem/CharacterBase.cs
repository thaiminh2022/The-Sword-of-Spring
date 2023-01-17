using UnityEngine;
using TheSwordOfSpring.HealthSystemTM;
using TheSwordOfSpring.StatSystem;
using TheSwordOfSpring.Modules;
using TheSwordOfSpring.CharacterSystem.InventorySystemTM;
using System;

namespace TheSwordOfSpring.CharacterSystem
{
    /// <summary>
    /// A class for inheriting the character, with some basic future implemented
    /// </summary>
    [RequireComponent(typeof(CharacterStartStats))]
    public class CharacterBase : MonoBehaviour, IGetHealthSystem, IGetCharacterBase, IDamageable
    {
        [SerializeField]
        protected CharacterStartStats baseCharacter;
        protected HealthSystem healthSystem;
        protected InventorySystem inventory;
        protected static PlayerInputActions inputActions;


        private void Awake()
        {
            inputActions = new PlayerInputActions();
            print(baseCharacter.Health.BaseValue);

            healthSystem = new HealthSystem(baseCharacter.Health.BaseValue);
            inventory = new InventorySystem(baseCharacter);
        }
        protected virtual void Start()
        {
            inputActions.Enable();
            inputActions.Player.Enable();

            baseCharacter.Health.OnModifierChange += Health_OnModifierChange;
        }

        private void Health_OnModifierChange(object sender, ModifierEventArgs e)
        {
            healthSystem.SetHealthMax(baseCharacter.Health.Value, false);
        }

        public HealthSystem GetHealthSystem()
        {
            return healthSystem;
        }

        public CharacterStartStats GetCharacterBase()
        {
            return baseCharacter;
        }

        public static PlayerInputActions GetInputActions()
        {
            return inputActions;
        }

        public InventorySystem GetInventory()
        {
            return inventory;
        }

        public Stat[] GetAllStatAsArray()
        {
            return baseCharacter.GetAllStatAsArray();
        }

        private void OnDestroy()
        {
            baseCharacter.Health.OnModifierChange -= Health_OnModifierChange;
        }

        public void Damage(float damage)
        {
            TakeDamage(damage);
        }
        protected virtual void TakeDamage(float damage)
        {
            healthSystem.Damage(damage);
            print($"Player Ouch: {healthSystem.GetHealth()}");
        }
    }
}
