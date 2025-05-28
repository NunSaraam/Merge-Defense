using System.Collections;
using System.Collections.Generic;
using TowerDefense.Tower;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] private string towerName;
    public string TowerName => towerName;

    [SerializeField] private TowerType towerGrade;
    public TowerType TowerGrade => towerGrade;

    [SerializeField] private Sprite towerSprite;
    public Sprite TowerSprite => towerSprite;

    [SerializeField] private RuntimeAnimatorController towerAnimator;
    public RuntimeAnimatorController TowerAnimator => towerAnimator;

    [SerializeField] private float attackRange;
    public float AttackRange => attackRange;

    [SerializeField] private bool isLongDistance;
    public bool IsLongDistance => isLongDistance;

    [SerializeField] private int damage;
    public int Damage => damage;

    [SerializeField] private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    [SerializeField] private float criticalChance;
    public float CriticalChance => criticalChance * 0.01f;

    [SerializeField] private float criticalMultiplier;
    public float CriticalMultiplier => criticalMultiplier * 0.01f;

    [SerializeField] private GameObject meleeEffectPrefab;
    public GameObject MeleeEffectPrefab => meleeEffectPrefab;

    [SerializeField] private GameObject rangedEffectPrefab;
    public GameObject RangedEffectPrefab => rangedEffectPrefab;
}
