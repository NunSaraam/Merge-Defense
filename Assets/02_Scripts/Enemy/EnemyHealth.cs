using UnityEngine;
using TowerDefense.Game;
using TowerDefense.Player;

namespace TowerDefense.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        private float baseHealth = 50;
        [SerializeField] private float currentHealth;
        private WaveManager waveManager;

        private void Start()
        {
            var waveData = FindObjectOfType<WaveManager>()?.CurrentWaveData;
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
            var gold = FindObjectOfType<WaveManager>()?.CurrentWaveData.EnemyGoldDrop ?? 0;
            FindObjectOfType<GoldManager>()?.AddGold((int)gold);
            FindObjectOfType<WaveManager>()?.NotifyEnemyKilled();
            Destroy(gameObject);
        }
    }
}