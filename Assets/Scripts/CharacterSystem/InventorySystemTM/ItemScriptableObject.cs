using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.StatSystem;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    [CreateAssetMenu(menuName = "TheSwordOfSpring/Item", fileName = "Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        [Header("Desc")]
        public new string name;

        [TextArea(3, 5)]
        public string desc;
        public Sprite sprite;

        [Header("Stat buff")]
        public StatBuff healthBuff;
        public StatBuff viewRangeBuff;
        public StatBuff damageBuff;
        public StatBuff atkRangeBuff;
        public StatBuff atkSpeedBuff;
        public StatBuff moveSpeedBuff;

        public List<BuffData> GetBuffDatas()
        {
            List<BuffData> buffDatas = new List<BuffData>();

            if (atkRangeBuff.Amount != 0)
                buffDatas.Add(new BuffData("AtkRange", atkRangeBuff.Amount));
            if (healthBuff.Amount != 0)
                buffDatas.Add(new BuffData("Health", healthBuff.Amount));
            if (damageBuff.Amount != 0)
                buffDatas.Add(new BuffData("Damage", damageBuff.Amount));
            if (viewRangeBuff.Amount != 0)
                buffDatas.Add(new BuffData("ViewRange", viewRangeBuff.Amount));
            if (atkSpeedBuff.Amount != 0)
                buffDatas.Add(new BuffData("AtkSpeed", atkSpeedBuff.Amount));
            if (moveSpeedBuff.Amount != 0)
                buffDatas.Add(new BuffData("MoveSpeed", moveSpeedBuff.Amount));

            return buffDatas;
        }
    }

    [System.Serializable]
    public struct StatBuff
    {
        public StatModType ModType;
        public float Amount;
    }

    public class BuffData
    {
        public string buffName;
        public float buffAmount;

        public BuffData(string buffName, float buffAmount)
        {
            this.buffName = buffName;
            this.buffAmount = buffAmount;
        }
    }
}