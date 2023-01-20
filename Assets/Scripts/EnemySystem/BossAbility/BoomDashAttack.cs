using UnityEngine;
using TheSwordOfSpring.HealthSystemTM;
using System;
using Redcode.Extensions;
using TheSwordOfSpring.Misc;

namespace TheSwordOfSpring.EnemySystem.BossAbility
{
    ///<summary>
	/// Teleport behind /infornt the player
    ///</summary>
    public class BoomDashAttack : MonoBehaviour
    {
        private Vector3 targetPosition;
        private float timeBeforeHit;
        private float damage;
        private float radius;
        Transform self;

        Action onFinishMove;

        Vector3 startPosition;
        private void Init()
        {
            startPosition = self.transform.position;

            self.LeanMoveLocalY(self.position.y + 5f, timeBeforeHit - .2f).setEaseLinear().setOnComplete(() =>
            {
                // Attack now
                onFinishMove?.Invoke();
                DealDamage();
            });
        }
        private void DealDamage()
        {
            self.LeanMoveLocal(targetPosition, .2f).setEaseOutCirc().setOnComplete(() =>
            {
                var go = Instantiate(PSStaticHolder.Instance.Explosion_PS, targetPosition, Quaternion.identity);
                Destroy(go, 1f);
                // Attack now
                var colliders = Physics2D.OverlapCircleAll(targetPosition, radius);
                foreach (var collider in colliders)
                {
                    if (collider.transform == self.transform)
                    {
                        continue;
                    }

                    if (collider.TryGetComponent<IDamageable>(out var damageable))
                    {
                        damageable?.Damage(damage);
                    }
                }
                Destroy(gameObject, 1f);
            });
        }

        ///<summary>
        /// Create a game object and returns a function to init it. 
        ///</summary>
        public static Action Create(Transform self, Vector3 targetPosition, float timeBeforeHit, float damage, float radius, Action onFinishMove = null)
        {
            GameObject go = new GameObject("BoomDashAttack");
            var comp = go.AddComponent<BoomDashAttack>();
            comp.targetPosition = targetPosition;
            comp.timeBeforeHit = timeBeforeHit;
            comp.damage = damage;
            comp.self = self;
            comp.radius = radius;
            comp.onFinishMove = onFinishMove;

            return comp.Init;
        }

    }
}