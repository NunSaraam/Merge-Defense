using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int maxSpawnCount = 10;
        [SerializeField] private float spawnInterval = 1f;

        private Queue<GameObject> enemyPool = new();
        private float lastSpawnTime;

        private void Start()
        {
            InitializePool();
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
                GameObject enemy = Instantiate(enemyPrefab);
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
    }
}