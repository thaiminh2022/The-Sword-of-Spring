using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.HealthSystemTM;

namespace TheSwordOfSpring.EffectsSystem
{
    public class BurningEffects : Effect
    {
        HealthSystem healthSystem;

        public const float BURN_INTERVAL = .3f;
        public const float BURN_DAMAGE = 3;

        private float burnInterval;


        private void Start()
        {
            HealthSystem.TryGetHealthSystem(gameObject, out healthSystem);
            burnInterval = BURN_INTERVAL;
        }

        private void Update()
        {
            if (healthSystem == null)
            {
                return;
            }

            if (burnInterval <= 0)
            {
                healthSystem.Damage(BURN_DAMAGE);
                burnInterval = BURN_INTERVAL;
            }
            else
            {
                burnInterval -= Time.deltaTime;
            }


        }
    }
}