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
    public class FlashAttack : MonoBehaviour
    {
        private Transform targetTransform;
        private float timeBeforeHit;
        private float damage;
        private float radius;
        Transform self;

        Action onFinishMove;
        private void Init()
        {
            // Move behind target
            float newX = targetTransform.position.x + (targetTransform.right.x > 0 ? -1 : 1f);
            Vector3 newPosition = new Vector3(newX, targetTransform.position.y, targetTransform.position.z);

            var go = Instantiate(PSStaticHolder.Instance.Dash_PS, self.position, Quaternion.identity);
            Destroy(go, 1f);

            self.LeanMoveLocal(newPosition, .1f).setEaseInSine().setOnComplete(() =>
            {
                // Attack now
                onFinishMove?.Invoke();
                Invoke(nameof(DealDamage), timeBeforeHit);
            });
        }
        private void DealDamage()
        {
            var colliders = Physics2D.OverlapCircleAll(self.position, radius);
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

        }

        ///<summary>
        /// Create a game object and returns a function to init it. 
        ///</summary>
        public static Action Create(Transform self, Transform targetTransform, float timeBeforeHit, float damage, float radius, Action onFinishMove = null)
        {
            GameObject go = new GameObject("FlashAttack");
            var comp = go.AddComponent<FlashAttack>();
            comp.targetTransform = targetTransform;
            comp.timeBeforeHit = timeBeforeHit;
            comp.damage = damage;
            comp.self = self;
            comp.radius = radius;
            comp.onFinishMove = onFinishMove;

            return comp.Init;
        }

    }
}