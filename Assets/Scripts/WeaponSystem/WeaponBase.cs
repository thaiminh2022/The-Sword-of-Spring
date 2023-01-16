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

        public virtual bool Attack(float atkRange, float atkDamage)
        {
            Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f);

            if (collider != null && collider.GetComponent<IDamageable>() != null)
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

                return true;
            }
            print("I ATTACK NOTHING");
            return false;

        }
    }
}