using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.StatSystem;

namespace TheSwordOfSpring.EnemySystem
{
    public class EnemyStartStats : MonoBehaviour
    {
        [SerializeField] EnemyScriptableObject baseStat;

        public Stat Health;
        public Stat Damage;
        public Stat AtkRange;
        public Stat AtkSpeed;
        public Stat MoveSpeed;


        private void Awake()
        {
            Health = new Stat(baseStat.health);
            Damage = new Stat(baseStat.damage);
            AtkRange = new Stat(baseStat.atkRange);
            AtkSpeed = new Stat(baseStat.atkSpeed);
            MoveSpeed = new Stat(baseStat.moveSpeed);

        }

        public Stat[] GetAllStatAsArray()
        {
            return new Stat[]
            {
                Health,
                Damage,
                AtkRange,
                AtkSpeed,
                MoveSpeed,

            };
        }
    }
}