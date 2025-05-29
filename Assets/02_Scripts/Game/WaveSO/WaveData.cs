using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData")]

public class WaveData : ScriptableObject
{
    [SerializeField] private int currentWave;
    public int CurrentWave => currentWave;

    [SerializeField] private bool isBossWave;
    public bool IsBossWave => isBossWave;

    [SerializeField] private int enemyPerWave;
    public int EnemyPerWave => enemyPerWave;

    [SerializeField] private float enemyHealth;
    public float EnemyHealth => enemyHealth;

    [SerializeField] private float enemyMovementSpeed;
    public float EnemyMovementSpeed => enemyMovementSpeed;
}
