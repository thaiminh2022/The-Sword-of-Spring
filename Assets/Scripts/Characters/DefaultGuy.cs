using TheSwordOfSpring.CharacterSystem;
using TheSwordOfSpring.CharacterSystem.InventorySystemTM;
using UnityEngine;

namespace TheSwordOfSpring
{
    public class DefaultGuy : CharacterBase
    {
        protected override void Start()
        {
            // Do shit after this line of code
            base.Start();


            // construct the heart of farmer
            var heartOfFarmer = InventoryItem.Create(StaticItemsHolder.Instance.HeartOfFarmer);
            inventory.AddItem(heartOfFarmer);
            Destroy(heartOfFarmer.gameObject, .1f);

        }

        private void Update()
        {
            if (Input.GetKeyDown("t"))
            {
                this.GetCharacterBase()
                .Health.AddModifier(new StatSystem.StatModifier(10, StatSystem.StatModType.Flat));
            }
        }
    }
}
