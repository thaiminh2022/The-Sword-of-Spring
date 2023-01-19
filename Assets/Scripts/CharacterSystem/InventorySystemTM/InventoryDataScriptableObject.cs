using TheSwordOfSpring.StatSystem;
using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{

    [CreateAssetMenu(fileName = "Inventory Data Holder", menuName = "TheSwordOfSpring/InventoryDataHolder")]
    public class InventoryDataScriptableObject : ScriptableObject
    {
        public List<InventoryItem> inventoryItems;

        public List<InventoryItem> GetInventoryItems()
        {
            if (inventoryItems == null)
            {
                inventoryItems = new List<InventoryItem>();
            }

            return inventoryItems;
        }
    }

}

