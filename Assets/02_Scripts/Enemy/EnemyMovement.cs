using UnityEngine;

namespace TowerDefense.Enemy
{
    public class EnemyPathFollower : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2.5f;
        [SerializeField] private Transform[] pathPoints;

        private int currentIndex = 0;

        public void SetPath(Transform[] path)
        {
            pathPoints = path;
            currentIndex = 0;
        }

        private void Update()
        {
            if (pathPoints == null || pathPoints.Length == 0 || currentIndex >= pathPoints.Length) return;

            Transform target = pathPoints[currentIndex];
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentIndex++;
            }
        }
    }
}