using UnityEngine;
using System;
using System.Linq;
using TheSwordOfSpring.TimeSystem;
using TheSwordOfSpring.EffectsSystem;
using System.Collections;
using TheSwordOfSpring.WeaponSystem;
using Redcode.Extensions;

namespace TheSwordOfSpring.EnemySystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RangerEnemy : EnemyBase
    {
        [SerializeField] GameObject projectileObject;

        private Rigidbody2D rb;
        private EnemyAnimation enemyAnimation;

        private float startTimeBtwAttack = 0;
        private float timeBtwAttack = 0;
        private Transform player;
        protected override void Start()
        {
            base.Start();

            SetEnemyState(EnemyState.WARM_UP);

            startTimeBtwAttack = CalculateTimeBtwAtk();
            player = GetPlayer().transform;

            rb = GetComponent<Rigidbody2D>();
            enemyAnimation = GetComponent<EnemyAnimation>();

            healthSystem.OnDead += HealthSystem_OnDead;
            healthSystem.OnDamaged += HealthSystem_OnDamage;
            TimeManager.OnDay += TimeManager_OnDay;
        }

        private void HealthSystem_OnDead(object sender, EventArgs args)
        {
            SetEnemyState(EnemyState.DEAD);
        }
        private void HealthSystem_OnDamage(object sender, EventArgs args)
        {
            if (enemyState != EnemyState.DEAD)
            {
                enemyAnimation.SetHitAnimation();
            }
        }
        private void TimeManager_OnDay(object sender, EventArgs args)
        {
            if (enemyState != EnemyState.DEAD)
            {
                EffectsManager
                .ApplyEffectToGameObject(EffectsType.BURNING, gameObject, TimeManager.GetDayLength());
            }
        }
        private void Update()
        {

            switch (enemyState)
            {
                case EnemyState.WARM_UP:
                    // play warm up animations
                    WarmUp();
                    break;
                case EnemyState.IDLE:
                    FindPlayer();
                    break;
                case EnemyState.ATTACK:
                    HandleAttack();
                    break;
                case EnemyState.RETREAT:
                    Retreat();
                    break;
                case EnemyState.DEAD:
                    Dead();
                    break;
                default:
                    break;


            }
            if (enemyState != EnemyState.DEAD)
            {
                FlipObject();
            }

            if (timeBtwAttack > 0)
            {
                timeBtwAttack -= Time.deltaTime;

            }
        }
        private void WarmUp()
        {
            SetEnemyState(EnemyState.IDLE);
        }
        private void FindPlayer()
        {
            enemyAnimation.SetRunAnimation();

            // Found player!
            SetEnemyState(EnemyState.ATTACK);
        }
        private void HandleAttack()
        {
            if (player == null)
            {
                return;
            }

            if (timeBtwAttack > 0)
            {
                SetEnemyState(EnemyState.RETREAT);
            }

            Vector2 playerPosition = (Vector2)player.transform.position;
            float atkRange = this.baseEnemy.AtkRange.Value;


            if (Vector2.Distance(transform.position, playerPosition) >= atkRange + 1)
            {
                MoveTowardsPlayer(playerPosition);
            }
            else
            {
                print("Called attack");
                StartCoroutine(Attack(player));
                timeBtwAttack = startTimeBtwAttack;
            }
        }

        private void MoveTowardsPlayer(Vector2 position)
        {
            float moveSpeed = this.baseEnemy.MoveSpeed.Value;
            Vector2 playerDir = (player.position - transform.position).normalized;
            enemyAnimation.SetRunAnimation();


            rb.AddForce(playerDir * moveSpeed * 1.25f, ForceMode2D.Impulse);
        }
        private IEnumerator Attack(Transform player)
        {
            if (timeBtwAttack <= 0)
            {
                float damageAmount = baseEnemy.Damage.Value;

                yield return new WaitForSeconds(1.5f);
                Transform target = this.GetPlayer().transform;

                // Shoot
                Transform go = Instantiate(projectileObject.transform, transform.position, Quaternion.identity);
                Projectile projectile = go.GetComponent<Projectile>();
                projectile.SetValue(target, damageAmount);
            }
        }


        private void Retreat()
        {
            Vector2 playerDir = (player.position - transform.position).normalized;
            Vector2 retreatDir = playerDir * -1;

            float moveSpeed = this.baseEnemy.MoveSpeed.Value;
            float atkRange = this.baseEnemy.AtkRange.Value;


            // If we retreat enough distance, stop and wait
            if (Vector2.Distance(player.position, transform.position) < atkRange + 1f)
            {
                enemyAnimation.SetRunAnimation();
                rb.AddForce(retreatDir * moveSpeed * .5f, ForceMode2D.Impulse);
            }
            else
            {
                enemyAnimation.StopAllAnimation();
            }

            if (timeBtwAttack <= 0)
            {
                SetEnemyState(EnemyState.IDLE);
            }
        }

        private void Dead()
        {
            enemyAnimation.SetDieAnimation();
            Destroy(gameObject, 3f);

        }


        private float CalculateTimeBtwAtk()
        {
            return 1 / this.baseEnemy.AtkSpeed.Value;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, baseEnemy.AtkRange.Value);

        }

        private void FlipObject()
        {
            float localScaleX = Mathf.Abs(transform.localScale.x);
            if (rb.velocity.x < 0)
            {
                transform.SetLocalScaleX(localScaleX * -1);
            }
            else if (rb.velocity.x > 0)
            {
                transform.SetLocalScaleX(localScaleX * 1);
            }
        }
        private void OnDestroy()
        {
            healthSystem.OnDead -= HealthSystem_OnDead;
            healthSystem.OnDamaged -= HealthSystem_OnDamage;
            TimeManager.OnDay -= TimeManager_OnDay;
        }
    }
}