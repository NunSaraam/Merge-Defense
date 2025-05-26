using UnityEngine;

namespace TowerDefense.Tower
{
    public enum TowerType { Common, Rare, Epic, Legendary, Mythical }

    public class Tower : MonoBehaviour
    {
        private int towerID;

        [SerializeField] private TowerDatabase towerDatabase;

        public GameObject projectilePrefab;
        public Transform firePoint;

        private TowerData currentTowerData;
        private TowerType currentTowerType = TowerType.Common;

        private Animator towerAnimator;
        private SpriteRenderer towerRenderer;

        private float attackCooldown;

        public TowerType CurrentType => currentTowerType;
        public TowerData GetCurrentData() => currentTowerData;

        private void Awake()
        {
            towerAnimator = GetComponent<Animator>();
            towerRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            towerID = Random.Range(0, towerDatabase.GetTowersByType(currentTowerType).Count);
            currentTowerData = towerDatabase.GetTowersByType(currentTowerType)[towerID];

            ApplyVisuals();
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

        public void SetTowerData(TowerType newType, TowerData newData)
        {
            currentTowerType = newType;
            currentTowerData = newData;
            ApplyVisuals();
        }

        private void ApplyVisuals()
        {
            towerRenderer.sprite = currentTowerData.TowerSprite;
            towerAnimator.runtimeAnimatorController = currentTowerData.TowerAnimator;
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

            if (enemy.TryGetComponent(out Enemy.EnemyHealth health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}