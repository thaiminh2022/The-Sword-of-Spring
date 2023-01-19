using TheSwordOfSpring.EnemySystem.BossAbility;
using UnityEngine;
using TheSwordOfSpring.Misc;
using TheSwordOfSpring.IndicatorSystem;
using System.Collections;
using TheSwordOfSpring.HealthSystemTM;

namespace TheSwordOfSpring.EnemySystem
{

    public class BossEnemyP1AttacksHandler : MonoBehaviour
    {
        public void HandleProjectileBoom(int amount, float damage, float indicatorTime)
        {
            Vector2[] dirs = RandomDirs.GetRandomDirs(amount);
            float indicatorLength = 100f;
            print(dirs.Length);

            Vector3[] startPositions = new Vector3[2]{
                transform.position,
                dirs[0] * indicatorLength,

            };
            IndicatorManager
            .CreateSquareIndicator(startPositions, .1f, indicatorTime, () =>
            {
                var Init = ProjectilesBoom.Create(transform.position, dirs, damage);
                Init();
            });

            for (int i = 1; i < amount; i++)
            {
                Vector3[] positions = new Vector3[2]{
                    transform.position,
                    dirs[i] * indicatorLength,

                };
                IndicatorManager.CreateSquareIndicator(positions, .3f, indicatorTime);
            }
        }

        public void HandleFlashAttack(Transform target, float timeBeforeHit, float damage, float radius)
        {
            var Init = FlashAttack.Create(transform, target, timeBeforeHit, damage, radius, () =>
            {
                // Create indicator for the attack
                IndicatorManager.CreateCircleIndicator(transform.position, radius, timeBeforeHit);
            });
            Init?.Invoke();
        }

        public void HandleShockwaveHitAttack(
            Transform target,
            float retreatDistance,
            int waveAmount,
            float timeBeforeDamage,
            float radius,
            float damage,
            Vector3 origin
        )
        {
            transform.LeanMoveLocalX(transform.position.x - retreatDistance, .5f).setEaseInCubic().setOnComplete(() =>
            {
                Vector3[] positions = new Vector3[waveAmount];

                for (int i = 0; i < positions.Length; i++)
                {
                    Vector3 position = transform.position + new Vector3(radius * i * 2, 0, 0);
                    IndicatorManager.CreateCircleIndicator(position, radius, timeBeforeDamage);

                    positions[i] = position;
                }

                var Init = ShockwaveHit.Create(transform, positions, damage, timeBeforeDamage, radius, () =>
                {
                    // Move boss to origin
                    transform.LeanMove(origin, 1f).setEaseInOutSine();
                });
                Init?.Invoke();
            });

        }


        public void HandleNormalAttack(Transform player, float damage, float moveSpeed, float waitTimeBeforeAttack, float radius)
        {
            // Move towards player 
            Vector3[] positions = new Vector3[2] {
                transform.position,
                player.position,
            };
            IndicatorManager.CreateSquareIndicator(positions, .2f, moveSpeed);
            transform.LeanMoveLocal(player.position, moveSpeed).setOnComplete(() =>
            {
                // Wait
                IndicatorManager.CreateCircleIndicator(transform.position, radius, waitTimeBeforeAttack);
                StartCoroutine(DamagePlayer(waitTimeBeforeAttack, radius, damage));

            });

        }
        IEnumerator DamagePlayer(float waitTimeBeforeAttack, float radius, float damage)
        {
            yield return new WaitForSeconds(waitTimeBeforeAttack);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var col in colliders)
            {
                if (col.transform == transform)
                    continue;

                if (col.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.Damage(damage);
                }
            }
        }



    }
}