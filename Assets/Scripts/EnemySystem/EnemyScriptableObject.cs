using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.EnemySystem
{
    [CreateAssetMenu(menuName = "TheSwordOfSpring/Enemy", fileName = "Enemy")]
    public class EnemyScriptableObject : ScriptableObject
    {
        [Header("Desc")]
        public new string name;
        [TextArea(3, 5)]
        public string description;
        public Sprite sprite;

        [Header("General"), Space()]
        public float health;

        [Header("Combat"), Space()]
        public float damage;
        public float atkRange;
        public float atkSpeed;
        public float moveSpeed;
    }
}