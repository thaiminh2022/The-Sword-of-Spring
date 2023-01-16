using UnityEngine;
using System;
using TheSwordOfSpring.Modules;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class PickupableInventoryItem : InventoryItem, IInteractable
    {
        public static event EventHandler<ItemScriptableObject> OnItemPickUp;

        public bool Interact(object source)
        {
            print($"{itemBase.name} has been interacted");

            // Add to inventory 
            if (source is GameObject)
            {
                GameObject srcObject = (GameObject)source;

                if (srcObject.TryGetComponent<CharacterBase>(out var characterBase))
                {
                    characterBase.GetInventory().AddItem(this, 1);
                    OnItemPickUp?.Invoke(this, itemBase);
                    Destroy(gameObject, .1f);
                    return true;
                }
            }

            print($"{itemBase.name} is non pickupable");
            return false;
        }
    }
}