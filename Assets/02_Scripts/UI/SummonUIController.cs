using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerDefense.Player;

namespace TowerDefense.UI
{
    public class SummonUIController : MonoBehaviour
    {
        [SerializeField] private Button summonButton;
        [SerializeField] private TMP_Text summonCostText;
        [SerializeField] private SummonManager summonManager;

        private void Start()
        {
            summonButton.onClick.AddListener(OnSummonClicked);
            UpdateButtonCostUI();
        }

        private void OnSummonClicked()
        {
            summonManager.TrySummonTower();
            UpdateButtonCostUI();
        }

        private void UpdateButtonCostUI()
        {
            summonCostText.text = $" : {summonManager.CurrentSummonCost}";
        }
    }
}