using UnityEngine;
using System;
using TheSwordOfSpring.Modules;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class PickupableInventoryItem : MonoBehaviour, IInteractable
    {
        [SerializeField] protected ItemScriptableObject itemBase;
        public static event EventHandler<ItemScriptableObject> OnItemPickUp;

        private void Start()
        {
            // Pingpong loop for reasons
            float defaultY = transform.position.y;
            transform.LeanMoveLocalY(defaultY + .2f, .3f).setLoopPingPong().setEaseInOutSine();
        }
        public bool Interact(object source)
        {

            // Add to inventory 
            if (source is GameObject)
            {
                GameObject srcObject = (GameObject)source;

                if (srcObject.TryGetComponent<CharacterBase>(out var characterBase))
                {
                    var item = new InventoryItem(itemBase);

                    characterBase.GetInventory().AddItem(item, 1);
                    OnItemPickUp?.Invoke(this, itemBase);
                    Destroy(gameObject, .1f);
                    return true;
                }
            }

            return false;
        }

        public ItemScriptableObject GetItemBase() => itemBase;
    }
}