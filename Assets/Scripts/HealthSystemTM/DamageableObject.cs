using System;
using UnityEngine;

namespace TheSwordOfSpring.HealthSystemTM
{
    public class DamageableObject : MonoBehaviour, IDamageable
    {
        HealthSystem healthSystem;

        private void Start()
        {
            HealthSystem.TryGetHealthSystem(gameObject, out healthSystem);
        }

        public void Damage(float damage)
        {
            healthSystem?.Damage(damage);
            print($"{gameObject.name} taken {damage} damage: IM HURT");
        }
    }
}