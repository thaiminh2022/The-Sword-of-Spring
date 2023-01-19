using UnityEngine;
using System.Collections.Generic;

namespace TheSwordOfSpring.EnemySystem
{

    [CreateAssetMenu(menuName = "TheSwordOfSpring/Boss", fileName = "Boss")]
    public class BossScriptableObject : EnemyScriptableObject
    {
        [Header("Attack")]
        public float waitTimeBtwAttack;

        [Header("Projectiles Boom")]
        public float projectilesIndicateTime;
        public float projectTilesDamage;
        public int projectilesAmount;

        [Header("Flash Attack")]
        public float flashAttackRadius;
        public float flashAttackDamage;
        public float flashAttackIndicatorTime;


        [Header("Shockwave Hit")]
        public float shockWaveRadius;
        public float shockWaveDamage;
        public float shoveWaveIndicatorTime;
        public int shockWaveAmount;
        public float shockWaveRetreatDistance;



    }
}