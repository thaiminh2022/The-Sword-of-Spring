using System;
using UnityEngine;
using Redcode.Extensions;
using TheSwordOfSpring.HealthSystemTM;
using TheSwordOfSpring.Misc;

namespace TheSwordOfSpring.EnemySystem.BossAbility
{

    public class ShockwaveHit : MonoBehaviour
    {
        private Transform self;
        private Vector3[] positions;
        private float damage;
        private float timeBeforeDamage;
        private float radius;
        Action onFinishAttack;

        private void Init()
        {
            Invoke(nameof(DealDamage), timeBeforeDamage);
        }

        private void DealDamage()
        {
            foreach (Vector3 position in positions)
            {
                Collider2D[] cols = Physics2D.OverlapCircleAll(position, radius);

                foreach (var col in cols)
                {
                    if (col.transform == self)
                    {
                        continue;
                    }

                    if (col.TryGetComponent<IDamageable>(out var damageable))
                    {
                        damageable?.Damage(damage);
                    }
                }
                var go = Instantiate(PSStaticHolder.Instance.Explosion_PS, position, Quaternion.identity);
                Destroy(go, 1f);
            }
            onFinishAttack?.Invoke();
            Destroy(gameObject, 1f);
        }



        public static Action Create(
         Transform self,
         Vector3[] positions,
         float damage,
         float timeBeforeDamage,
         float radius,
         Action onFinishAttack = null
        )
        {
            GameObject go = new GameObject("Shockwavehit");
            var comp = go.AddComponent<ShockwaveHit>();

            comp.self = self;
            comp.positions = positions;
            comp.damage = damage;
            comp.timeBeforeDamage = timeBeforeDamage;
            comp.radius = radius;
            comp.onFinishAttack = onFinishAttack;

            return comp.Init;
        }
    }
}