using System.Collections.Generic;
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

        // Augmentation
        private float bonusAttack = 0f;
        private float bonusSpeed = 0f;
        private float bonusCritChance = 0f;
        private float bonusCritDamage = 0f;

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

            ApplyAugment(TowerUpgradeSystem.GetAllBonuses());
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
                    float totalSpeed = currentTowerData.AttackSpeed + bonusSpeed;
                    attackCooldown = 1f / totalSpeed;
                }
            }
        }

        public void SetTowerData(TowerType newType, TowerData newData)
        {
            currentTowerType = newType;
            currentTowerData = newData;
            ApplyVisuals();

            if (currentTowerData.TowerName == "Raccoon" && GetComponent<RaccoonSkill>() == null)
            {
                gameObject.AddComponent<RaccoonSkill>();
            }
            if (currentTowerData.TowerName == "Hyena" && GetComponent<HyenaSkill>() == null)
            {
                gameObject.AddComponent<HyenaSkill>();
            }
            if (currentTowerData.TowerName == "Farcon" && GetComponent<FalconSkill>() == null)
            {
                gameObject.AddComponent<FalconSkill>();
            }
            if (currentTowerData.TowerName == "Lion" && GetComponent<LionSkill>() == null)
            {
                gameObject.AddComponent<LionSkill>();
            }
            if (currentTowerData.TowerName == "BlackPuma" && GetComponent<BlackPumaSkill>() == null)
            {
                gameObject.AddComponent<BlackPumaSkill>();
            }
            if (currentTowerData.TowerName == "Crocodile" && GetComponent<CrocodileSkill>() == null)
            {
                gameObject.AddComponent<CrocodileSkill>();
            }
            if (currentTowerData.TowerName == "Tiger" && GetComponent<TigerSkill>() == null)
            {
                gameObject.AddComponent<TigerSkill>();
            }
            if (currentTowerData.TowerName == "Rino" && GetComponent<RinoSkill>() == null)
            {
                gameObject.AddComponent<RinoSkill>();
            }
            if (currentTowerData.TowerName == "Griffin" && GetComponent<GriffinSkill>() == null)
            {
                gameObject.AddComponent<GriffinSkill>();
            }
            if (currentTowerData.TowerName == "Pegasus" && GetComponent<PegasusSkill>() == null)
            {
                gameObject.AddComponent<PegasusSkill>();
            }
            if (currentTowerData.TowerName == "Cerberus" && GetComponent<CerberusSkill>() == null)
            {
                gameObject.AddComponent<CerberusSkill>();
            }
        }

        private void ApplyVisuals()
        {
            towerRenderer.sprite = currentTowerData.TowerSprite;
            towerAnimator.runtimeAnimatorController = currentTowerData.TowerAnimator;
        }

        private GameObject FindNearestEnemyInRange()
        {
            float pixelPerUnit = 100f;
            float pixelDistance = currentTowerData.AttackRange * 128f;
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
            // 매개변수 Enemy가 target 되어 스킬 사용되는 방식

            // 스킬 메서드 구현부

            // 타워가 Tower 프리팹 하나로 직렬화되었기 때문에, CurrentTowerData 에서 스킬들을 불러온 후,
            // 그 스킬의 원거리, 근거리 공격여부 등 파악한 후 개별 호출 이루어져야함.

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

                RaccoonSkill raccoonSkill = GetComponent<RaccoonSkill>();
                if (raccoonSkill != null)
                {
                    raccoonSkill.RandomGold();
                }

                HyenaSkill hyenaSkill = GetComponent<HyenaSkill>();
                if (hyenaSkill != null)
                {
                    hyenaSkill.OnAttack();
                }

                BlackPumaSkill blackPumaSkill = GetComponent<BlackPumaSkill>();
                if (blackPumaSkill != null && blackPumaSkill.IsFirstAttack(target))
                {
                    damage *= 2f;
                }

                TigerSkill tigerSkill = GetComponent<TigerSkill>();
                if (tigerSkill != null)
                {
                    tigerSkill.ApplyBleed(target);
                }

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

                // Crocodile
                CrocodileSkill crocSkill = GetComponent<CrocodileSkill>();
                if (crocSkill != null)
                {
                    crocSkill.OnAttack(target.transform.position);
                }
            }
        }


        private float GetDamage()
        {
            float baseCrit = currentTowerData.CriticalChance + bonusCritChance;
            float totalCritChance = Mathf.Clamp01(baseCrit + critBonus);
            bool isCrit = Random.value < totalCritChance;

            if (isCrit)
            {
                critBonus = 0f;
                float multiplier = 1.5f + currentTowerData.CriticalMultiplier + bonusCritDamage;
                float damage = (currentTowerData.Damage * (1f + bonusAttack)) * multiplier;

                HyenaSkill hyenaSkill = GetComponent<HyenaSkill>();
                if (hyenaSkill != null && hyenaSkill.TryConsumeCritical())
                {
                    damage *= 2f;
                }
                return damage;
            }
            else
            {
                float gain = baseCrit * 0.1f;
                critBonus += gain;
                float damage = currentTowerData.Damage * (1f + bonusAttack);

                HyenaSkill hyenaSkill = GetComponent<HyenaSkill>();
                if (hyenaSkill != null && hyenaSkill.TryConsumeCritical())
                {
                    damage *= 2f;
                }
                return damage;
            }
        }

        public void ApplyAugment(Dictionary<AugmentType, float> buffs)
        {
            if (buffs.TryGetValue(AugmentType.AttackPower, out float atk))
                bonusAttack = atk;
            if (buffs.TryGetValue(AugmentType.AttackSpeed, out float spd))
                bonusSpeed = spd;
            if (buffs.TryGetValue(AugmentType.CritChance, out float crit))
                bonusCritChance = crit;
            if (buffs.TryGetValue(AugmentType.CritDamage, out float critDmg))
                bonusCritDamage = critDmg;
        }

        // Cerberus
        public void ApplyBonusAttackPower(float value)
        {
            bonusAttack += value;
        }

        // Pegasus
        public void ApplyBonusAttackSpeed(float value)
        {
            bonusSpeed += value;
        }

        // Griffin
        public void ApplyBonusCritChance(float value)
        {
            bonusCritChance += value;
        }
    }
}