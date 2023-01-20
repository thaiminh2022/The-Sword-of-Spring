using UnityEngine;
using System.Linq;
using TheSwordOfSpring.HealthSystemTM;

namespace TheSwordOfSpring.WeaponSystem
{
    public class WeaponBase : MonoBehaviour, IGetWeaponScriptableObject
    {
        [SerializeField] private WeaponScriptableObject baseWeapon;
        [SerializeField] bool useGizmos;

        [SerializeField] private Transform attackPoint;

        private float atkRange = 1f;

        public WeaponScriptableObject GetWeaponScriptableObject()
        {
            return baseWeapon;
        }

        public virtual bool Attack(float atkRange, float atkDamage)
        {
            this.atkRange = atkRange;
            Physics2D
            .OverlapCircleAll(attackPoint.position, atkRange)
            .ToList()
            .FindAll(item =>
            {
                if (item.gameObject == transform.root.gameObject)
                {
                    return false;
                }

                return item.TryGetComponent<IDamageable>(out var _);

            })
            .ForEach(damageableCollider =>
            {
                damageableCollider
                .GetComponent<IDamageable>()
                .Damage(atkDamage);
            });


            return true;

        }

        private void OnDrawGizmosSelected()
        {
            if (!useGizmos)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(attackPoint.position, atkRange);

        }
    }
}