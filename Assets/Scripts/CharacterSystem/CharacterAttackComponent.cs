using UnityEngine;
using TheSwordOfSpring.Modules;
using TheSwordOfSpring.WeaponSystem;

namespace TheSwordOfSpring.CharacterSystem
{
    public class CharacterAttackComponent : MonoBehaviour, IAttackComponent
    {
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private CharacterStartStats characterStartStats;

        private float startAtkCooldown;
        private float atkCoolDown;
        private WeaponBase baseWeapon;

        private void Start()
        {
            startAtkCooldown = GetComponent<CharacterStartStats>().GetAtkSpeed();
            baseWeapon = weaponHolder.GetComponentInChildren<WeaponBase>();

        }
        public void StartAttack()
        {
            if (atkCoolDown <= 0)
            {
                baseWeapon?.Attack(characterStartStats.GetAtkRange(), CalculateDamage());

                atkCoolDown = startAtkCooldown;
            }
        }

        private void Update()
        {
            // this if prevent us from floating point overflow
            if (atkCoolDown > 0)
            {
                atkCoolDown -= Time.deltaTime;
            }
        }

        private float CalculateDamage()
        {
            return characterStartStats.GetAtkDamage() * baseWeapon.GetWeaponScriptableObject().damageMultiplier;
        }

        private void OnValidate()
        {
            if (characterStartStats == null)
            {
                characterStartStats = GetComponent<CharacterStartStats>();
            }
        }
    }
}