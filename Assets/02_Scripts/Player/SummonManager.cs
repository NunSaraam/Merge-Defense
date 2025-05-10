using UnityEngine;
using TowerDefense.Tower;

namespace TowerDefense.Player
{
    public class SummonManager : MonoBehaviour
    {
        [SerializeField] private GoldManager goldManager;
        [SerializeField] private Transform summonParent;
        [SerializeField] private GameObject[] commonTowerPrefabs;
        [SerializeField] private int summonStartCost = 20;
        [SerializeField] private int summonCostIncrease = 2;

        private int currentSummonCost;

        private void Awake()
        {
            currentSummonCost = summonStartCost;
        }

        public void TrySummonTower()
        {
            if (!goldManager.CanAfford(currentSummonCost)) return;

            goldManager.SpendGold(currentSummonCost);

            var randomPrefab = commonTowerPrefabs[Random.Range(0, commonTowerPrefabs.Length)];
            var newTower = Instantiate(randomPrefab, summonParent); // summonParent Modified to Random Location
            
            newTower.transform.localPosition = Vector3.zero;

            currentSummonCost += summonCostIncrease;
        }

        public int CurrentSummonCost => currentSummonCost;
    }
}