using UnityEngine;
using TMPro;

namespace TowerDefense.Game
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private WaveDatabase waveDatabase;
        [SerializeField] private float delayBetweenWaves = 5f;

        [SerializeField] private TMP_Text currentWavePrint;
        [SerializeField] private TMP_Text currentAliveEnemies;

        private int currentWave = 0;
        [SerializeField] private int aliveEnemies = 0;

        public WaveData CurrentWaveData => waveDatabase.GetWaveData(currentWave);

        public delegate void WaveEvent(int wave);
        public event WaveEvent OnWaveStart;

        private void Start() => StartNextWave();

        private void StartNextWave()
        {
            currentWave++;
            aliveEnemies = CurrentWaveData.EnemyCount;
            currentWavePrint.text = $"현재 웨이브 : {CurrentWaveData.WaveNumber}";
            currentAliveEnemies.text = $"남은 적 수 : {aliveEnemies}";
            OnWaveStart?.Invoke(currentWave);
        }

        public void NotifyEnemyKilled()
        {
            aliveEnemies--;
            currentAliveEnemies.text = $"남은 적 수 : {aliveEnemies}";
            if (aliveEnemies <= 0)
            {
                Invoke(nameof(StartNextWave), delayBetweenWaves);
            }
        }
    }
}