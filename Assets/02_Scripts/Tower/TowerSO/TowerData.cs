using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower/TowerData")]

public class TowerData : ScriptableObject
{
    [SerializeField]
    private string towerName;
    public string TowerName { get { return towerName; } }

    [SerializeField]
    private string towerGrade;
    public string TowerGrade { get { return towerGrade; } }

    [SerializeField]
    private float attackRange;
    public float AttackRange { get { return attackRange; } }

    [SerializeField]
    private bool isLongDistance;
    public bool IsLongDistance { get { return isLongDistance; } }

    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField]
    private float criticalChance;
    public float CriticalChance { get { return criticalChance *= 0.01f; } }

    [SerializeField]
    private float criticalMultiplier;
    public float CriticalMultiplier { get { return criticalMultiplier *= 0.01f; } }
}
