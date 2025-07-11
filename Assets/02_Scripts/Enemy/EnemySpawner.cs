using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Game;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private GameObject bossPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnInterval = 1f;
        [SerializeField] private Transform[] pathPoints;
        [SerializeField] private int currentSpawnCount;

        private Queue<GameObject> enemyPool = new();
        private float lastSpawnTime;

        private void Start()
        {
            WaveManager waveManager = FindObjectOfType<WaveManager>();
            if (waveManager != null)
            {
                waveManager.OnWaveStart += PrepareWave;
            }
        }

        private void Update()
        {
            if (Time.time >= lastSpawnTime + spawnInterval && enemyPool.Count > 0)
                SpawnEnemy();
        }

        private void InitializePool(int count)
        {
            enemyPool.Clear();
            currentSpawnCount = count;
            if (WaveManager.IsBossWave)
            {
                GameObject bossEnemy = Instantiate(bossPrefab);
                bossEnemy.SetActive(false);
                enemyPool.Enqueue(bossEnemy);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    GameObject enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)]);
                    enemy.SetActive(false);
                    enemyPool.Enqueue(enemy);
                }
            }
        }

        private void SpawnEnemy()
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.transform.position = spawnPoint.position;
            enemy.SetActive(true);
            enemy.GetComponent<EnemyMovement>().SetPath(pathPoints);
            lastSpawnTime = Time.time;
        }

        private void PrepareWave(int wave)
        {
            var waveData = FindObjectOfType<WaveManager>()?.CurrentWaveData;
            if (waveData == null) return;

            spawnInterval = Mathf.Max(0.2f, 2.0f - ((wave - 1) * 0.05f));
            currentSpawnCount = waveData.EnemyCount;
            InitializePool(currentSpawnCount);
        }
    }
}