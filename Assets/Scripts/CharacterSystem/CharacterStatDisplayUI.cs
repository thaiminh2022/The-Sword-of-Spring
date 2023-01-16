using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Redcode.Extensions;
using TheSwordOfSpring.StatSystem;
using TheSwordOfSpring.CharacterSystem.InventorySystemTM;

namespace TheSwordOfSpring.CharacterSystem
{

    public class CharacterStatDisplayUI : MonoBehaviour
    {
        [SerializeField] string[] statString;
        [SerializeField] private CharacterStartStats characterStat;
        [SerializeField] private Transform statHolder;
        [SerializeField] private GameObject statDisplayTemplate;

        private void Start()
        {
            PickupableInventoryItem.OnItemPickUp += PickupableInventoryItem_OnItemPickup;
            UpdateStat();
        }

        private void PickupableInventoryItem_OnItemPickup(object sender, ItemScriptableObject item)
        {
            UpdateStat();
        }
        private void UpdateStat()
        {
            var stats = characterStat.GetAllStatAsArray();
            RemoveAllStatTemplate();

            for (int i = 0; i < stats.Length; i++)
            {
                var stat = stats[i];

                GameObject go = Instantiate(statDisplayTemplate, statHolder);
                StatDisplayTemplate template = go.GetComponent<StatDisplayTemplate>();

                template.SetStatName(statString[i]);
                template.SetStatValue(stat.Value);
            }

        }
        private void RemoveAllStatTemplate()
        {
            statHolder.DestroyChilds();
        }
    }
}