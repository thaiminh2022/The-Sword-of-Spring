using UnityEngine;
using TheSwordOfSpring.Misc;
using TheSwordOfSpring.HealthSystemTM;

namespace TheSwordOfSpring.EffectsSystem
{
    public class BurningEffects : Effect
    {

        public const float BURN_INTERVAL = .3f;
        public const float BURN_DAMAGE = 1;

        private float burnInterval;


        private void Start()
        {
            burnInterval = BURN_INTERVAL;
            GameObject go = Instantiate(PSStaticHolder.Instance.Fire_PS, transform.position, Quaternion.identity, transform);
            go.transform.localScale = new Vector3(.5f, .5f, .5f);
        }

        private void Update()
        {

            if (burnInterval <= 0)
            {
                bool canDamage = TryGetComponent<IDamageable>(out var damageable);

                if (canDamage)
                {
                    damageable.Damage(BURN_DAMAGE);
                    burnInterval = BURN_INTERVAL;
                }
                else
                {
                    print($"Cannot damage: {gameObject.name}");
                }
            }
            else
            {
                burnInterval -= Time.deltaTime;
            }


        }
    }
}