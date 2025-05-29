using TowerDefense.Game;
using UnityEngine;

namespace TowerDefense.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private float moveSpeed = 2f;
        private Transform[] pathPoints;

        private int currentIndex = 0;

        private void Start()
        {
            var waveData = FindObjectOfType<WaveManager>()?.CurrentWaveData;
            moveSpeed = waveData != null ? waveData.EnemyMovementSpeed : moveSpeed;
        }

        public void SetPath(Transform[] path)
        {
            pathPoints = path;
            currentIndex = 0;
        }

        private void Update()
        {
            if (pathPoints == null || currentIndex >= pathPoints.Length) return;

            Transform target = pathPoints[currentIndex];
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentIndex++;
                if (currentIndex >= pathPoints.Length)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}