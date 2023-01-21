using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.CharacterSystem.InventorySystemTM;

namespace TheSwordOfSpring.CharacterSystem
{
    public class PlayerStartInventory : MonoBehaviour
    {
        [SerializeField]
        CharacterBase itemsHolder;

        private void Start()
        {
            itemsHolder.GetInventory().ClearInventory();
            InventoryItem item = new InventoryItem(StaticItemsHolder.Instance.HeartOfFarmer);
            itemsHolder.GetInventory().AddItemUnique(item);
        }
    }
}