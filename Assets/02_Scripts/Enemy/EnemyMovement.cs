using TowerDefense.Game;
using UnityEngine;
using System.Collections;

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

        public void ModifySpeed(float multiplier, float duration)
        {
            StartCoroutine(SlowDownCoroutine(multiplier, duration));
        }

        private IEnumerator SlowDownCoroutine(float multiplier, float duration)
        {
            float originalSpeed = moveSpeed;
            moveSpeed *= multiplier;
            yield return new WaitForSeconds(duration);
            moveSpeed = originalSpeed;
        }

        // Crocodile
        private bool isStun = false;

        public void Stun(float duration)
        {
            if (!isStun)
                StartCoroutine(StunCoroutine(duration));
        }

        private IEnumerator StunCoroutine(float duration)
        {
            isStun = true;
            float originalSpeed = moveSpeed;
            moveSpeed = 0f;
            yield return new WaitForSeconds(duration);
            moveSpeed = originalSpeed;
            isStun = false;
        }

        // Rino
        public void ApplyStun(float duration)
        {
            StartCoroutine(StunCoroutineR(duration));
        }

        private IEnumerator StunCoroutineR(float duration)
        {
            float originalSpeed = moveSpeed;
            moveSpeed = 0f;  // 스턴 ( 이속 0 )
            yield return new WaitForSeconds(duration);
            moveSpeed = originalSpeed;  // 스턴 종료 ( 이속 복구 )
        }
    }
}