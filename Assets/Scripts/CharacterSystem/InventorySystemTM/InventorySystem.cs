using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class InventorySystem
    {
        public CharacterStartStats CharacterStartStats { get; private set; }

        private List<InventoryItem> inventoryItems;

        public InventorySystem(CharacterStartStats characterStartStats)
        {
            CharacterStartStats = characterStartStats;
            inventoryItems = new List<InventoryItem>();
        }
        public InventorySystem(CharacterStartStats characterStartStats, InventoryDataScriptableObject holder)
        {
            CharacterStartStats = characterStartStats;

            var inventoryItemsData = holder.GetInventoryItems();
            inventoryItems = inventoryItemsData;

            if (inventoryItemsData.Count > 0)
            {
                foreach (var item in inventoryItems)
                {
                    item.EquipItem(characterStartStats);
                }
            }
        }

        public void AddItem(InventoryItem item, int amount = 1)
        {
            AddItemMany(item, amount);
        }
        public void AddItemUnique(InventoryItem item)
        {
            if (!inventoryItems.Contains(item))
            {
                AddItemMany(item, 1);
            }
        }

        private void AddItemMany(InventoryItem item, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                item.EquipItem(CharacterStartStats);
                inventoryItems.Add(item);
            }
        }

        public List<InventoryItem> GetItems()
        {
            inventoryItems.ForEach(x => Debug.Log(x.GetItemBase().name));
            return inventoryItems;
        }
        public void ClearInventory()
        {
            inventoryItems.Clear();
        }

        private bool RemoveItem(InventoryItem item)
        {
            bool removed = inventoryItems.Remove(item);

            if (removed)
            {
                item.RemoveItem(CharacterStartStats);
            }

            return removed;
        }
        private bool RemoveItem(int index)
        {
            if (index < 0 || index >= inventoryItems.Count)
            {
                return false;
            }
            return RemoveItem(inventoryItems[index]);

        }


    }
}