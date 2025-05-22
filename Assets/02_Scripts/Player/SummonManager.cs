using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefense.Player
{
    public class SummonManager : MonoBehaviour
    {
        [SerializeField] private GoldManager goldManager;
        //[SerializeField] private 
        [SerializeField] private GameObject[] commonTowerPrefabs;
        [SerializeField] private Transform[] summonParent;
        [SerializeField] private int summonStartCost = 20;
        [SerializeField] private int summonCostIncrease = 2;
        [SerializeField] private GameObject summonFailedText;

        private int currentSummonCost;

        private void Awake()
        {
            currentSummonCost = summonStartCost;
            summonFailedText.SetActive(false);
        }

        public void TrySummonTower()
        {
            if (!goldManager.CanAfford(currentSummonCost))
                return;

            List<Transform> emptySlots = new List<Transform>();
            foreach (var slot in summonParent)
            {
                if (slot.childCount == 0)
                {
                    emptySlots.Add(slot);
                }
            }

            if (emptySlots.Count > 0)
            {
                Transform selectedSlot = emptySlots[Random.Range(0, emptySlots.Count)];
                var prefab = commonTowerPrefabs[Random.Range(0, commonTowerPrefabs.Length)];
                Instantiate(prefab, selectedSlot.position, Quaternion.identity, selectedSlot);

                goldManager.SpendGold(currentSummonCost);
                currentSummonCost += summonCostIncrease;
            }
            else
            {
                StartCoroutine(ShowSummonFailText());
            }
        }

        private IEnumerator ShowSummonFailText()
        {
            summonFailedText.SetActive(true);
            yield return new WaitForSeconds(2f);
            summonFailedText.SetActive(false);
        }

        public int CurrentSummonCost => currentSummonCost;
    }
}