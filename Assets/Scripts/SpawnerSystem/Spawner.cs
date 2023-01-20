using UnityEngine;
using System.Collections;
using TheSwordOfSpring.TimeSystem;
using System;
using System.Collections.Generic;
using UnityRandom = UnityEngine.Random;

namespace TheSwordOfSpring.SpawnerSystem
{
    public class Spawner : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField] int startCoin;
        [SerializeField] AnimationCurve extraCoinPerWaveCurve;

        [SerializeField] Vector2 startSpawnPosition, endSpawnPosition;

        [Header("Enemies")]
        [SerializeField] EnemyData[] enemyDatas;


        [Header("Internal")]
        private int coin;

        public static event EventHandler<List<GameObject>> OnSpawnEnemies;
        public static event EventHandler<GameObject> OnSpawnEnemy;
        public static event EventHandler OnNextWave;



        private void Start()
        {
            TimeManager.OnNight += TimeManager_OnNight;
            TimeManager.OnDay += TimeManager_OnDay;

            coin = startCoin;

        }
        private void TimeManager_OnNight(object sender, EventArgs args)
        {
            // New wave
            var enemies = ChooseEnemies();
            if (enemies.Count <= 0)
            {
                // No enemies to spawn
                Debug.LogError($"There were no enemy left, be free");
                return;
            }
            // Calculate time btw spawn 
            float timeBtwSpawn = (TimeManager.GetNightLength() * 0.5f) / (enemies.Count);

            OnSpawnEnemies?.Invoke(this, enemies);
            StartCoroutine(SpawnEnemies(enemies, timeBtwSpawn));

        }

        private void TimeManager_OnDay(object sender, EventArgs args)
        {
            // End wave
            OnNextWave?.Invoke(this, EventArgs.Empty);
            coin += GetExtraCoinViaCurve();
        }


        private List<GameObject> ChooseEnemies()
        {
            int coinForEnemy = coin;
            List<GameObject> enemies = new List<GameObject>();

            while (coinForEnemy > 0)
            {
                int index = UnityRandom.Range(0, enemyDatas.Length);
                EnemyData enemyData = enemyDatas[index];

                enemies.Add(enemyData.enemy);
                coinForEnemy -= enemyData.cost;
            }

            return enemies;
        }

        private IEnumerator SpawnEnemies(List<GameObject> enemies, float timeBtwSpawn)
        {
            foreach (GameObject spawnEnemy in enemies)
            {
                Vector2 spawnPosition = RandomPositionBtw2Vector(startSpawnPosition, endSpawnPosition);

                OnSpawnEnemy?.Invoke(this, spawnEnemy);
                Instantiate(spawnEnemy, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(timeBtwSpawn);
            }
        }

        private Vector2 RandomPositionBtw2Vector(Vector2 start, Vector2 end)
        {
            float randomX = UnityRandom.Range(start.x, end.x);
            float randomY = UnityRandom.Range(start.y, end.y);

            return new Vector2(randomX, randomY);
        }

        private bool IsOccupied(Vector2 position, float radius = 1f)
        {
            return Physics2D.OverlapCircle(position, radius) != null;
        }

        private int GetExtraCoinViaCurve()
        {
            int currentDay = TimeManager.Day;
            int time = currentDay;

            if (currentDay >= extraCoinPerWaveCurve.keys.Length)
            {
                time = extraCoinPerWaveCurve.keys.Length - 1;
            }

            return Mathf.RoundToInt(extraCoinPerWaveCurve.Evaluate(time));
        }

        private void OnDestroy()
        {
            TimeManager.OnNight -= TimeManager_OnNight;
            TimeManager.OnDay -= TimeManager_OnDay;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(startSpawnPosition, 1f);
            Gizmos.DrawWireSphere(endSpawnPosition, 1f);
            Gizmos.DrawLine(startSpawnPosition, endSpawnPosition);

        }
    }
    [Serializable]
    public struct EnemyData
    {
        public GameObject enemy;
        public int cost;
    }
}