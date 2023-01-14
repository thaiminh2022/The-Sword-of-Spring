using UnityEngine;
using System.Linq;
using TheSwordOfSpring.HealthSystemTM;

namespace TheSwordOfSpring.WeaponSystem
{
    public class WeaponBase : MonoBehaviour, IGetWeaponScriptableObject
    {
        [SerializeField] private WeaponScriptableObject baseWeapon;

        public WeaponScriptableObject GetWeaponScriptableObject()
        {
            return baseWeapon;
        }

        public virtual void Attack(float atkRange, float atkDamage)
        {


            Physics2D
            .OverlapCircleAll(transform.position, atkRange)
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

        }
    }
}