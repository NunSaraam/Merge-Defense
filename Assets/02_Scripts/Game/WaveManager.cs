using UnityEngine;
using TMPro;
using TowerDefense.Tower;

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
        [SerializeField] private int playerHealth = 3;
        public int CurrentWave => currentWave;

        public WaveData CurrentWaveData { get; private set; }

        public delegate void WaveEvent(int wave);
        public event WaveEvent OnWaveStart;


        private void Start() => StartNextWave();

        private void StartNextWave()
        {
            currentWave++;
            CurrentWaveData = waveDatabase.GetWaveData(currentWave);
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
                if (CurrentWaveData.IsBossWave)
                {
                    FindObjectOfType<AugmentManager>().OpenAugmentSelection(OnAugmentComplete);
                }
                else
                {
                    Invoke(nameof(StartNextWave), delayBetweenWaves);
                }
            }
        }

        private void OnAugmentComplete()
        {
            Invoke(nameof(StartNextWave), delayBetweenWaves);
        }

        public void RegisterEscape()
        {
            playerHealth = CurrentWaveData.IsBossWave ? 0 : --playerHealth;
            // 점수저장부분
            if (playerHealth <= 0)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }
    }
}
