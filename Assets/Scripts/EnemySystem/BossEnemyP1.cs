using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityRandom = UnityEngine.Random;
using TheSwordOfSpring.EffectsSystem;
using Redcode.Extensions;

namespace TheSwordOfSpring.EnemySystem
{
    public class BossEnemyP1 : EnemyBase, IStunable, ITriggerable
    {
        [SerializeField]
        private BossScriptableObject bossScriptable;

        private Rigidbody2D rb;
        private EnemyAnimation enemyAnimation;

        private Transform player;
        private bool isDead = false;
        private bool isStun = false;

        BossEnemyP1AttacksHandler attacksHandler;
        private Vector3 startPosition;

        protected override void Start()
        {
            base.Start();

            player = GetPlayer().transform;

            rb = GetComponent<Rigidbody2D>();
            enemyAnimation = GetComponent<EnemyAnimation>();
            attacksHandler = GetComponent<BossEnemyP1AttacksHandler>();

            healthSystem.OnDead += HealthSystem_OnDead;
            healthSystem.OnDamaged += HealthSystem_OnDamage;

            startPosition = transform.position;

        }

        public void Trigger()
        {
            WarmUp();
        }

        private void HealthSystem_OnDead(object sender, EventArgs args)
        {
            isDead = true;
            Dead();
        }
        private void HealthSystem_OnDamage(object sender, EventArgs args)
        {
            if (!isDead)
            {
                enemyAnimation.SetHitAnimation();
            }

        }
        private void Update()
        {
            FlipObject();
        }
        private void WarmUp()
        {
            HandleAttack();
        }
        private IEnumerator Wait(float extraTime)
        {
            yield return new WaitForSeconds(bossScriptable.waitTimeBtwAttack + extraTime);
            HandleAttack();
        }

        private void HandleAttack()
        {
            if (isDead)
            {
                return;
            }

            BossAttacks chosenAttack = (BossAttacks)ChooseRandomAttack();
            print(chosenAttack.ToString());

            StartCoroutine(Attack(chosenAttack));
        }
        private IEnumerator Attack(BossAttacks attackType)
        {
            yield return new WaitUntil(() => isStun == false);

            float extraTime = 0;
            switch (attackType)
            {
                case BossAttacks.ProjectilesBom:

                    attacksHandler
                    .HandleProjectileBoom(
                        bossScriptable.projectilesAmount,
                        bossScriptable.projectTilesDamage,
                        bossScriptable.projectilesIndicateTime
                    );
                    extraTime = bossScriptable.projectilesIndicateTime;

                    break;
                case BossAttacks.Flash:
                    attacksHandler
                    .HandleFlashAttack(
                        player,
                        bossScriptable.flashAttackIndicatorTime,
                        bossScriptable.flashAttackDamage,
                        bossScriptable.flashAttackRadius
                    );
                    extraTime = bossScriptable.flashAttackIndicatorTime;

                    break;
                case BossAttacks.ShockwaveHit:
                    attacksHandler
                    .HandleShockwaveHitAttack(
                        player,
                        bossScriptable.shockWaveRetreatDistance,
                        bossScriptable.shockWaveAmount,
                        bossScriptable.shoveWaveIndicatorTime,
                        bossScriptable.shockWaveRadius,
                        bossScriptable.shockWaveDamage,
                        startPosition
                    );
                    extraTime = bossScriptable.shoveWaveIndicatorTime;

                    break;
                case BossAttacks.NormalAttack:
                    attacksHandler
                    .HandleNormalAttack(
                        player,
                        bossScriptable.damage,
                        bossScriptable.moveSpeed,
                        bossScriptable.atkSpeed,
                        bossScriptable.atkRange
                    );
                    extraTime = bossScriptable.atkSpeed;

                    break;
                default:
                    break;
            }

            StartCoroutine(Wait(extraTime));
        }
        private int ChooseRandomAttack()
        {
            Array enumsValue = Enum.GetValues(typeof(BossAttacks));
            int index = UnityRandom.Range(0, enumsValue.Length);

            return index;
        }
        private void MoveTowardsPosition(Vector2 position)
        {
            float moveSpeed = this.baseEnemy.MoveSpeed.Value;
            transform.LeanMoveLocal(position, moveSpeed);
        }

        private void Dead()
        {
            enemyAnimation.SetDieAnimation();
        }

        private float CalculateTimeBtwAtk()
        {
            return 1 / this.baseEnemy.AtkSpeed.Value;
        }


        private void OnDestroy()
        {
            healthSystem.OnDead -= HealthSystem_OnDead;
            healthSystem.OnDamaged -= HealthSystem_OnDamage;
        }
        private void FlipObject()
        {
            Vector2 dirToPlayer = (player.position - transform.position).normalized;


            float localScaleX = Mathf.Abs(transform.localScale.x);
            if (dirToPlayer.x < 0)
            {
                transform.SetLocalScaleX(localScaleX * -1);
            }
            else if (dirToPlayer.x > 0)
            {
                transform.SetLocalScaleX(localScaleX * 1);
            }
        }

        private Vector2 RandomPosition()
        {
            return UnityEngine.Random.insideUnitCircle * 4f + (Vector2)startPosition;
        }

        public void StartStun()
        {
            isStun = true;
        }

        public void StopStun()
        {
            isStun = false;
        }


    }
}

public enum BossState
{
    WARM_UP,
    ATTACK,
    WAIT,
    DEAD
}
public enum BossAttacks
{
    ProjectilesBom,
    Flash,
    ShockwaveHit,
    NormalAttack,

}
