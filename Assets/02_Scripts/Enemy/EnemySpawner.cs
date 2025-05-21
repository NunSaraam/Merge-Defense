using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Game;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int maxSpawnCount = 10;
        [SerializeField] private float spawnInterval = 1f;

        private Queue<GameObject> enemyPool = new();
        private float lastSpawnTime;

        private void Start()
        {
            InitializePool();

            WaveManager waveManager = FindObjectOfType<WaveManager>();
            if (waveManager != null)
                waveManager.OnWaveStart += PrepareWave;
        }

        private void Update()
        {
            if (Time.time >= lastSpawnTime + spawnInterval && enemyPool.Count > 0)
            {
                SpawnEnemy();
            }
        }

        private void InitializePool()
        {
            for (int i = 0; i < maxSpawnCount; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)]);
                enemy.SetActive(false);
                enemyPool.Enqueue(enemy);
            }
        }

        private void SpawnEnemy()
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.transform.position = spawnPoint.position;
            enemy.SetActive(true);
            lastSpawnTime = Time.time;
        }

        private void PrepareWave(int wave)
        {
            maxSpawnCount = 10 + (wave - 1) * 2;
            spawnInterval = Mathf.Max(0.3f, 1.0f - (wave - 1) * 0.05f);
            InitializePool();
        }
    }
}