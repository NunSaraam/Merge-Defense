using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject m_Enemy;
    [SerializeField] private Transform m_SpawnPoint;

    [SerializeField] private int m_SpawnCount;
    [SerializeField] private float m_SpawnTime = 1.0f; // 적 스폰시간 조절
    private Queue<GameObject> enemySpawnQueue = new Queue<GameObject>();
    private int MAX_SPAWN_COUNT { get; set; }
    private float m_CountSpawnTime;

    private void Start()
    {
        enemySpawnQueue.Clear();
        for (int i = 0; i < MAX_SPAWN_COUNT; i++)
        {
            EnQueue();
        }
    }
    private void Update()
    {
        if (m_Enemy != null)
        {
            if(Time.time >= m_CountSpawnTime + m_SpawnTime)
            {
                DeQueue().transform.position = m_SpawnPoint.position;
                m_CountSpawnTime = Time.time;
            }
        }
    }
    public void EnQueue()
    {
        if (m_Enemy != null)
        {
            enemySpawnQueue.Enqueue(m_Enemy);
        }
    }
    public GameObject DeQueue()
    {
        if (m_Enemy != null && enemySpawnQueue.Count >= 1)
        {
            return enemySpawnQueue.Dequeue();
        }
        return null;
    }
}
