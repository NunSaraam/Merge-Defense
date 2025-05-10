using UnityEngine;
using TowerDefense.Player;

namespace TowerDefense.Game
{
    public class BossSystem : MonoBehaviour
    {
        [SerializeField] private GoldManager goldManager;

        public void HandleBossDefeat(int waveNumber)
        {
            int reward = waveNumber switch
            {
                5 => 50,
                10 => 150,
                15 => 200,
                20 => 300,
                25 => 400,
                _ => 100,
            };

            goldManager.AddGold(reward);
            // augmentation UI CallBack
        }
    }
}