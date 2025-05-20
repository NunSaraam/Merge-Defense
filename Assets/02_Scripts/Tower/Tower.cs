using UnityEngine;

namespace TowerDefense.Tower
{
    public class Tower : MonoBehaviour
    {
        [Header("Tower Stats")]
        public int level = 1;
        public float attackDamage = 10f;
        public float attackSpeed = 1f; // per second
        public float criticalChance = 0.1f; // 10%
        public float criticalMultiplier = 2f;

        [Header("Attack Settings")]
        public float attackRange = 2.5f;
        public GameObject projectilePrefab;
        public Transform firePoint;

        private float attackCooldown;

        private void Update()
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                var target = FindNearestEnemy();
                if (target != null)
                {
                    Attack(target);
                    attackCooldown = 1f / attackSpeed;
                }
            }
        }

        private GameObject FindNearestEnemy()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearest = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < attackRange && dist < minDistance)
                {
                    nearest = enemy;
                    minDistance = dist;
                }
            }

            return nearest;
        }

        private void Attack(GameObject enemy)
        {
            float damage = attackDamage;
            if (Random.value < criticalChance)
            {
                damage *= criticalMultiplier;
            }

            // Deal direct damage (or instantiate projectile in È®Àå)
            if (enemy.TryGetComponent(out Enemy.EnemyHealth health))
            {
                health.TakeDamage(damage);
            }
        }

        public void Upgrade()
        {
            level++;
            attackDamage *= 1.5f;
            attackSpeed += 0.2f;
            criticalChance = Mathf.Min(criticalChance + 0.05f, 1f);
            criticalMultiplier += 0.5f;
        }
    }
}
