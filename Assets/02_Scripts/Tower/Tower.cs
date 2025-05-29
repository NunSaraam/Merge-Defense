using UnityEngine;

namespace TowerDefense.Tower
{
    public enum TowerType { Common, Rare, Epic, Legendary, Mythical }

    public class Tower : MonoBehaviour
    {
        private int towerID;

        [SerializeField] private TowerDatabase towerDatabase;

        // Only Test
        [SerializeField] private bool useDebugEffects = true;
        [SerializeField] private GameObject debugMeleeEffectPrefab;
        [SerializeField] private GameObject debugRangedEffectPrefab;


        [SerializeField] private Transform firePoint;

        private TowerData currentTowerData;
        private TowerType currentTowerType = TowerType.Common;

        private Animator towerAnimator;
        private SpriteRenderer towerRenderer;

        private float attackCooldown;

        private float critBonus = 0f;

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
            if (TryGetComponent(out TowerDrag drag) && drag.IsDragging) return;

            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                var target = FindNearestEnemyInRange();
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

        private GameObject FindNearestEnemyInRange()
        {
            float pixelPerUnit = 100f;
            float pixelDistance = currentTowerData.AttackRange * 256f;
            float worldRange = pixelDistance / pixelPerUnit;

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearest = null;
            float closest = float.MaxValue;

            foreach (var enemy in enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < worldRange && dist < closest)
                {
                    closest = dist;
                    nearest = enemy;
                }
            }

            return nearest;
        }

        private void Attack(GameObject enemy)
        {
            if (currentTowerData.IsLongDistance)
            {
                LaunchProjectile(enemy);
            }
            else
            {
                MeleeAttack(enemy);
            }
        }
        private void MeleeAttack(GameObject target)
        {
            GameObject prefab = useDebugEffects ? debugMeleeEffectPrefab : currentTowerData.MeleeEffectPrefab;
            if (prefab == null || target == null) return;

            Instantiate(prefab, target.transform.position, Quaternion.identity);

            if (target.TryGetComponent(out Enemy.EnemyHealth health))
            {
                float damage = GetDamage();
                health.TakeDamage(damage);
            }
        }

        private void LaunchProjectile(GameObject target)
        {
            GameObject prefab = useDebugEffects ? debugRangedEffectPrefab : currentTowerData.RangedEffectPrefab;
            if (prefab == null) return;

            GameObject projectile = Instantiate(prefab, firePoint.position, Quaternion.identity);
            if (projectile.TryGetComponent(out Projectile proj))
            {
                proj.Init(target.transform, GetDamage());
            }
        }

        private float GetDamage()
        {
            float baseCrit = currentTowerData.CriticalChance;
            float totalCritChance = Mathf.Clamp01(baseCrit + critBonus);
            bool isCrit = Random.value < totalCritChance;

            if (isCrit)
            {
                critBonus = 0f;
                float multiplier = 1f + currentTowerData.CriticalMultiplier;
                return currentTowerData.Damage * multiplier;
            }
            else
            {
                float gain = baseCrit * 0.1f;
                critBonus += gain;
                return currentTowerData.Damage;
            }
        }
    }
}