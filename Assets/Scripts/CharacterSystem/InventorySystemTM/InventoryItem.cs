using TheSwordOfSpring.StatSystem;
using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{

    [System.Serializable]
    public class InventoryItem
    {
        [SerializeField] protected ItemScriptableObject itemBase;

        public InventoryItem(ItemScriptableObject itemBase)
        {
            this.itemBase = itemBase;
        }

        public void EquipItem(CharacterStartStats character)
        {
            if (itemBase.atkRangeBuff.Amount != 0)
                character.AtkRange.AddModifier(new StatModifier(itemBase.atkRangeBuff.Amount, itemBase.atkRangeBuff.ModType));
            if (itemBase.healthBuff.Amount != 0)
                character.Health.AddModifier(new StatModifier(itemBase.healthBuff.Amount, itemBase.healthBuff.ModType));
            if (itemBase.viewRangeBuff.Amount != 0)
                character.ViewRange.AddModifier(new StatModifier(itemBase.viewRangeBuff.Amount, itemBase.viewRangeBuff.ModType));
            if (itemBase.atkSpeedBuff.Amount != 0)
                character.AtkSpeed.AddModifier(new StatModifier(itemBase.atkSpeedBuff.Amount, itemBase.atkSpeedBuff.ModType));
            if (itemBase.moveSpeedBuff.Amount != 0)
                character.MoveSpeed.AddModifier(new StatModifier(itemBase.moveSpeedBuff.Amount, itemBase.moveSpeedBuff.ModType));
        }
        public void RemoveItem(CharacterStartStats character)
        {
            if (itemBase.atkRangeBuff.Amount != 0)
                character.AtkRange.RemoveAllModifiersFromSource(this);
            if (itemBase.healthBuff.Amount != 0)
                character.Health.RemoveAllModifiersFromSource(this);
            if (itemBase.viewRangeBuff.Amount != 0)
                character.ViewRange.RemoveAllModifiersFromSource(this);
            if (itemBase.atkSpeedBuff.Amount != 0)
                character.AtkSpeed.RemoveAllModifiersFromSource(this);
            if (itemBase.moveSpeedBuff.Amount != 0)
                character.MoveSpeed.RemoveAllModifiersFromSource(this);
        }

        public ItemScriptableObject GetItemBase()
        {
            return itemBase;
        }

        public List<BuffData> GetBuffDatas()
        {
            return itemBase.GetBuffDatas();
        }


    }




}