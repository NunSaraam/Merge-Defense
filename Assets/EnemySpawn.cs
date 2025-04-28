using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private int m_SpawnCount;

    private float m_SpawnTime = 1.0f; // 적 스폰시간 조절
    private float m_CountSpawnTime;

    private void Update()
    {
        if (m_Enemy != null)
        {
            if(Time.time >= m_CountSpawnTime + m_SpawnTime)
            {
                Instantiate(m_Enemy, m_SpawnPoint);
                m_CountSpawnTime = Time.time;
            }
        }
    }
}
