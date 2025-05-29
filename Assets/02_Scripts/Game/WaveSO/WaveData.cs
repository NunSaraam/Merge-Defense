using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData")]

public class WaveData : ScriptableObject
{
    [SerializeField] private string waveNumber;
    public string WaveNumber => waveNumber;

    [SerializeField] private int enemyCount;
    public int EnemyCount => enemyCount;

    [SerializeField] private float enemyHealth;
    public float EnemyHealth => enemyHealth;

    [SerializeField] private float enemyMovementSpeed;
    public float EnemyMovementSpeed => enemyMovementSpeed * 0.5f;

    [SerializeField] private float enemyGoldDrop;
    public float EnemyGoldDrop => enemyGoldDrop;

    [SerializeField] private bool isBossWave;
    public bool IsBossWave => isBossWave;
}
