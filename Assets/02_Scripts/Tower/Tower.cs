using System.Runtime.CompilerServices;
using UnityEngine;

namespace TowerDefense.Tower
{
    public enum TowerType { Common, Rare, Epic, Legendary, Mythical }
    public class Tower : MonoBehaviour
    {
        private int towerID;

        [SerializeField] private TowerDatabase towerDatabase;

        // TODO : 타워 애니메이터, 이미지 각 등급마다 분류하기
        [Header("TowerProperties")]
        public RuntimeAnimatorController[] towerAnimators;
        public Sprite[] towerImages;

        private Animator towerAnimator;
        private SpriteRenderer towerRenderer;

        private TowerData currentTowerData;

        private TowerType currentTowerType = TowerType.Common;

        public string currentTowerName => currentTowerData.TowerName;

        public GameObject projectilePrefab;
        public Transform firePoint;

        private float attackCooldown;
        private void Start()
        {
            towerID = Random.Range(0, towerDatabase.GetTowersByType(TowerType.Common).Count);
            currentTowerData = towerDatabase.GetTowersByType(currentTowerType)[towerID];
            towerAnimator = GetComponent<Animator>();
            towerRenderer = GetComponent<SpriteRenderer>();
            towerAnimator.runtimeAnimatorController = towerAnimators[towerID];
            towerRenderer.sprite = towerImages[towerID];
            attackCooldown = currentTowerData.AttackSpeed;
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
            if (currentTowerType >= TowerType.Mythical) return;

            currentTowerType += 1;
            int newTowerID = Random.Range(0, towerDatabase.GetTowersByType(currentTowerType).Count);
            currentTowerData = towerDatabase.GetTowersByType(currentTowerType)[newTowerID];
            towerID = newTowerID;
        }

        /*
         * 나중구현
        private void UpdateTowerVisuals(int newID)
        {
            if (newID < towerAnimators.Length)
                towerAnimator.runtimeAnimatorController = towerAnimators[newID].runtimeAnimatorController;
            if (newID < towerImages.Length)
                towerRenderer.sprite = towerImages[newID];
        }
        */

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
                else
                {
                    Debug.Log("Not Set AttackRange or Something Wrong");
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

            // Deal direct damage (or instantiate projectile in 확장)
            if (enemy.TryGetComponent(out Enemy.EnemyHealth health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
