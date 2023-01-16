using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class MatchSpriteToItem : MonoBehaviour
    {
        [SerializeField] PickupableInventoryItem inventoryItem;
        [SerializeField] SpriteRenderer spriteRenderer;


        private void Start()
        {
            spriteRenderer.sprite = inventoryItem.GetItemBase().sprite;
        }

        private void OnValidate()
        {
            if (inventoryItem == null)
            {
                inventoryItem = GetComponent<PickupableInventoryItem>();
            }
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();

            }

            if (spriteRenderer != null && inventoryItem != null)
            {
                spriteRenderer.sprite = inventoryItem.GetItemBase().sprite;

            }
        }
    }
}