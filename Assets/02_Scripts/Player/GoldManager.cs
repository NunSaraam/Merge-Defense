using UnityEngine;
using System;

namespace TowerDefense.Player
{
    public class GoldManager : MonoBehaviour
    {
        [SerializeField] private int startingGold = 100;
        [SerializeField] private int currentGold;

        public int CurrentGold => currentGold;
        public event Action<int> OnGoldChanged;

        private void Awake()
        {
            currentGold = startingGold;
            OnGoldChanged?.Invoke(currentGold);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                currentGold += 10;
                OnGoldChanged?.Invoke(currentGold);

            }
        }

        public void AddGold(int amount)
        {
            if (amount > 0)
            {
                currentGold += amount;
                OnGoldChanged?.Invoke(currentGold);
            }
        }

        public bool SpendGold(int amount)
        {
            if (currentGold >= amount)
            {
                currentGold -= amount;
                OnGoldChanged?.Invoke(currentGold);
                return true;
            }
            return false;
        }

        public bool CanAfford(int amount) => currentGold >= amount;
    }
}