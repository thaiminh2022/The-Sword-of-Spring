using TheSwordOfSpring.Stats;
using UnityEngine;
using TheSwordOfSpring.Modules;

namespace TheSwordOfSpring.CharacterSystem
{
    [System.Serializable]
    public class CharacterStartStats : MonoBehaviour, IStatComponent
    {
        [SerializeField] CharacterScriptableObject baseStat;

        public Stat Health;
        public Stat Damage;
        public Stat AtkRange;
        public Stat AtkSpeed;
        public Stat MoveSpeed;
        public Stat ViewRange;


        private void Awake()
        {
            Health = new Stat(baseStat.health);
            Damage = new Stat(baseStat.damage);
            AtkRange = new Stat(baseStat.atkRange);
            AtkSpeed = new Stat(baseStat.atkSpeed);
            MoveSpeed = new Stat(baseStat.moveSpeed);
            ViewRange = new Stat(baseStat.viewRange);

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
                ViewRange

            };
        }

        public float GetSpeed()
        {
            return MoveSpeed.Value;
        }

        public float GetViewRange() => ViewRange.Value;

        public float GetAtkDamage() => Damage.Value;


        public float GetAtkRange() => AtkRange.Value;

        public float GetAtkSpeed() => AtkSpeed.Value;
    }
}
