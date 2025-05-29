using UnityEngine;
using TowerDefense.Game;

namespace TowerDefense.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public float baseHealth = 50f;
        private float currentHealth;
        private WaveManager waveManager;

        private void Start()
        {
            waveManager = FindObjectOfType<WaveManager>();
            int wave = waveManager?.CurrentWave ?? 1;
            currentHealth = baseHealth + (wave - 1) * 25f;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Enemy Dead");
            Destroy(gameObject);
        }
    }
}