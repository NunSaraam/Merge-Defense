using UnityEngine;

namespace TowerDefense.Game
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private float waveDuration = 20f;
        [SerializeField] private float delayBetweenWaves = 5f;

        private float currentWaveTime;
        private bool waveInProgress;
        private int currentWave = 1;

        public int CurrentWave => currentWave;

        public delegate void WaveEvent(int wave);
        public event WaveEvent OnWaveStart;
        public event WaveEvent OnBossWave;

        private void Update()
        {
            if (waveInProgress)
            {
                currentWaveTime -= Time.deltaTime;
                if (currentWaveTime <= 0f)
                {
                    waveInProgress = false;
                    Invoke(nameof(StartNextWave), delayBetweenWaves);
                }
            }
        }

        private void StartNextWave()
        {
            currentWave++;
            currentWaveTime = waveDuration;
            waveInProgress = true;
            OnWaveStart?.Invoke(currentWave);

            if (currentWave % 5 == 0)
            {
                OnBossWave?.Invoke(currentWave);
            }
        }
    }
}