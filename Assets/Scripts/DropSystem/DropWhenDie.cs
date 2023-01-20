using UnityEngine;
using TheSwordOfSpring.HealthSystemTM;
using System;

namespace TheSwordOfSpring.DropSystem
{
    public class DropWhenDie : MonoBehaviour
    {
        HealthSystem healthSystem;
        public GameObject[] dropItems;


        private bool dropped = false;

        private void Start()
        {
            HealthSystem.TryGetHealthSystem(gameObject, out healthSystem);
            healthSystem.OnDead += HealthSystem_OnDead;
        }

        private void HealthSystem_OnDead(object sender, EventArgs args)
        {
            float randomNumber = UnityEngine.Random.Range(0f, 100f);

            // 10% to drop this item
            if (randomNumber <= 10f && dropped == false)
            {
                int randomIndex = UnityEngine.Random.Range(0, dropItems.Length);
                GameObject chosenObject = dropItems[randomIndex];

                Instantiate(chosenObject, transform.position, Quaternion.identity);
                dropped = true;
            }
        }
    }
}