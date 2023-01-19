using UnityEngine;
using TheSwordOfSpring.HealthSystemTM;
using System;
namespace TheSwordOfSpring.EnemySystem
{

    public class BossFightManager : MonoBehaviour
    {
        [SerializeField]
        private Transform boss;
        [SerializeField]
        private Transform player;

        HealthSystem enemyHealthSystem;
        HealthSystem playerHealthSystem;

        private bool halfHeal = false;


        private void Start()
        {
            HealthSystem.TryGetHealthSystem(boss.gameObject, out enemyHealthSystem);
            HealthSystem.TryGetHealthSystem(boss.gameObject, out playerHealthSystem);

            enemyHealthSystem.OnDamaged += HealthSystem_OnDamaged;

            boss.GetComponent<ITriggerable>().Trigger();
        }
        private void HealthSystem_OnDamaged(object sender, EventArgs args)
        {
            if (enemyHealthSystem.GetHealthNormalized() < .5f && halfHeal == false)
            {
                // Heal character to full
                enemyHealthSystem?.HealComplete();
                halfHeal = true;
            }
        }

        private void OnDestroy()
        {
            enemyHealthSystem.OnDamaged -= HealthSystem_OnDamaged;
        }



    }
}