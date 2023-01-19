using UnityEngine;
using TheSwordOfSpring.HealthSystemTM;
using TheSwordOfSpring.StatSystem;

namespace TheSwordOfSpring.EnemySystem
{
    public class EnemyBase : MonoBehaviour, IDamageable, IGetHealthSystem
    {
        [SerializeField]
        protected EnemyStartStats baseEnemy;
        protected HealthSystem healthSystem;
        protected EnemyState enemyState;

        private void Awake()
        {
            healthSystem = new HealthSystem(baseEnemy.Health.BaseValue);
        }

        protected virtual void Start()
        {
            baseEnemy.Health.OnModifierChange += Health_OnModifierChange;
        }
        private void Health_OnModifierChange(object sender, ModifierEventArgs e)
        {
            healthSystem.SetHealthMax(baseEnemy.Health.Value, false);
        }

        protected GameObject GetPlayer()
        {
            return GameObject.FindGameObjectWithTag("Player");
        }

        protected virtual void SetEnemyState(EnemyState newState)
        {
            enemyState = newState;
        }

        public void Damage(float damage)
        {
            healthSystem.Damage(damage);
            print($"Enemy Ouch: {healthSystem.GetHealth()}");

        }

        protected void DealDamage(GameObject target, float amount)
        {
            target.GetComponent<IDamageable>().Damage(amount);
        }

        public HealthSystem GetHealthSystem()
        {
            return healthSystem;
        }
    }
}