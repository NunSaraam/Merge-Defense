using UnityEngine;
using TowerDefense.Game;
using TowerDefense.Player;
using System.Collections;

namespace TowerDefense.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        private float baseHealth = 50f;
        [SerializeField] private float currentHealth;
        private WaveManager waveManager;
        private bool dead = false;

        private void Start()
        {
            waveManager = FindObjectOfType<WaveManager>();
            var waveData = waveManager?.CurrentWaveData;
            currentHealth = waveData != null ? waveData.EnemyHealth : baseHealth;
        }   

        public void TakeDamage(float amount)
        {
            if (dead) return;
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Die(isEscaped: false);
                dead = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Destroy"))
            {
                Die(isEscaped: true);
            }
        }

        private void Die(bool isEscaped)
        {
            waveManager?.NotifyEnemyKilled();

            if (!isEscaped)
            {
                var gold = waveManager?.CurrentWaveData.EnemyGoldDrop ?? 0;
                FindObjectOfType<GoldManager>()?.AddGold((int)gold);
            }
            else
            {
                waveManager?.RegisterEscape();
            }
            Destroy(gameObject);
        }
    }
}