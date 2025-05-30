using UnityEngine;
using TowerDefense.Game;
using TowerDefense.Player;

namespace TowerDefense.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        private float baseHealth = 50f;
        [SerializeField] private float currentHealth;
        private WaveManager waveManager;

        private void Start()
        {
            waveManager = FindObjectOfType<WaveManager>();
            var waveData = waveManager?.CurrentWaveData;
            currentHealth = waveData != null ? waveData.EnemyHealth : baseHealth;
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
            var gold = waveManager?.CurrentWaveData.EnemyGoldDrop ?? 0;
            waveManager?.NotifyEnemyKilled();
            FindObjectOfType<GoldManager>()?.AddGold((int)gold);
            Destroy(gameObject);
        }
    }
}