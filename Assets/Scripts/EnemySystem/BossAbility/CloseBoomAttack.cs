using System;
using UnityEngine;
using Redcode.Extensions;
using TheSwordOfSpring.HealthSystemTM;
using TheSwordOfSpring.Misc;
using System.Collections;
using System.Linq;

namespace TheSwordOfSpring.EnemySystem.BossAbility
{

    public class CloseBoomAttack : MonoBehaviour
    {
        private Transform self;
        private Vector3[] positions;
        private float damage;
        private float timeBeforeDamage;
        private float radius;
        private float timeOffSet;
        Action onFinishAttack;

        private void Init()
        {
            Invoke(nameof(DealDamage), timeBeforeDamage);
        }

        private void DealDamage()
        {
            StartCoroutine(AttackHandle());
        }

        IEnumerator AttackHandle()
        {
            for (int i = 0; i < positions.Length; i += 4)
            {
                Vector3[] overlapPositions = positions[i..(i + 4)];

                foreach (var overlapPosition in overlapPositions)
                {
                    Collider2D[] cols = Physics2D.OverlapCircleAll(overlapPosition, radius);
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
                    var go = Instantiate(PSStaticHolder.Instance.Explosion_PS, overlapPosition, Quaternion.identity);
                    Destroy(go, 1f);
                }

                yield return new WaitForSeconds(timeOffSet);
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
         float timeOffset,
         Action onFinishAttack = null
        )
        {
            GameObject go = new GameObject("CloseBoomAttack");
            var comp = go.AddComponent<CloseBoomAttack>();

            comp.self = self;
            comp.positions = positions;
            comp.damage = damage;
            comp.timeBeforeDamage = timeBeforeDamage;
            comp.radius = radius;
            comp.onFinishAttack = onFinishAttack;
            comp.timeOffSet = timeOffset;

            return comp.Init;
        }
    }
}