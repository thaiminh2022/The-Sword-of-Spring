using UnityEngine;
using TheSwordOfSpring.CharacterSystem.InventorySystemTM;
using System.Collections.Generic;
using TheSwordOfSpring.Modules;
using System.Linq;
using System;
using TheSwordOfSpring.CharacterSystem;

namespace TheSwordOfSpring.SceneSystem
{

    public class RequiresItemToBoss : MonoBehaviour, IInteractable
    {
        public List<InventoryItem> requiresInventoryItem = new List<InventoryItem>();
        private List<InventoryItem> hasItems = new List<InventoryItem>();

        public event EventHandler OnInteract;


        public bool Interact(object source)
        {

            if (source is GameObject)
            {
                var srcObj = (GameObject)source;

                if (srcObj.TryGetComponent<CharacterBase>(out var baseCharacter))
                {
                    hasItems.Clear();
                    print("Item added");

                    var inventory = baseCharacter.GetInventory();
                    var inventoryItems = inventory.GetItems();

                    foreach (var item in inventoryItems)
                    {
                        var items = requiresInventoryItem.FindAll(requireItem =>
                        {
                            return requireItem.GetItemBase() == item.GetItemBase();
                        });

                        if (items.Count > 0)
                        {
                            hasItems.AddRange(items);
                        }
                    }

                    OnInteract?.Invoke(this, EventArgs.Empty);
                }
            }

            return true;
        }

        public List<InventoryItem> GetNoItems()
        {
            return requiresInventoryItem.Except(hasItems).ToList();
        }
        public List<InventoryItem> GetHasItems()
        {
            return hasItems;
        }
        public List<InventoryItem> GetRequireItems()
        {
            return requiresInventoryItem;
        }

    }
}