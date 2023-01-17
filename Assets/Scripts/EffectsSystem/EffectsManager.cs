using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.EffectsSystem
{
    public class EffectsManager : MonoBehaviour
    {
        public static void ApplyEffectToGameObject(EffectsType type, GameObject target, float time)
        {
            void AddToTarget<TEffect>()
            where TEffect : Effect
            => HandleAddComponent<TEffect>(target, time);

            switch (type)
            {
                case EffectsType.BURNING:
                    AddToTarget<BurningEffects>();
                    break;
                default:
                    break;
            }
        }

        private static void HandleAddComponent<TEffect>(GameObject target, float time)
        where TEffect : Effect
        {
            Effect effect = target.AddComponent<TEffect>();
            effect.DestroySelf(time);
        }



    }
}