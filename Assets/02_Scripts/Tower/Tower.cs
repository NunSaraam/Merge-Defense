using System.Runtime.CompilerServices;
using UnityEngine;

namespace TowerDefense.Tower
{
    public enum TowerType { Common, Rare, Epic, Legendary, Mythical }
    public class Tower : MonoBehaviour
    {
        public TowerDatabase towerDatabase;

        private TowerData currentTowerData;

        private TowerType currentTowerType = TowerType.Common;

        public string currentTowerLevel => currentTowerData.TowerName;

        public GameObject projectilePrefab;
        public Transform firePoint;

        private float attackCooldown;
        private void Start()
        {
            currentTowerData = towerDatabase.GetTowersByType(currentTowerType)[Random.Range(0, towerDatabase.GetTowersByType(TowerType.Common).Count)];
        }

        private void Update()
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                var target = FindNearestEnemy();
                if (target != null)
                {
                    Attack(target);
                    attackCooldown = 1f / currentTowerData.AttackSpeed;
                }
            }
        }

        public void UpgradeTowerData()
        {
            currentTowerType += 1;
            currentTowerData = towerDatabase.GetTowersByType(currentTowerType)[Random.Range(0, towerDatabase.GetTowersByType(currentTowerType).Count)];
        }

        private GameObject FindNearestEnemy()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearest = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < currentTowerData.AttackRange && dist < minDistance)
                {
                    nearest = enemy;
                    minDistance = dist;
                }
            }

            return nearest;
        }

        private void Attack(GameObject enemy)
        {
            float damage = currentTowerData.Damage;
            if (Random.value < currentTowerData.CriticalChance)
            {
                damage *= currentTowerData.CriticalMultiplier;
            }

            // Deal direct damage (or instantiate projectile in È®Àå)
            if (enemy.TryGetComponent(out Enemy.EnemyHealth health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
