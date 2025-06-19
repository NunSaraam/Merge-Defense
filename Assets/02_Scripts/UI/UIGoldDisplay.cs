using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerDefense.Player;

namespace TowerDefense.UI
{
    public class UIGoldDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private GoldManager goldManager;

        private void OnEnable()
        {
            goldManager.OnGoldChanged += UpdateGoldText;
            UpdateGoldText(goldManager.CurrentGold);
        }

        private void OnDisable()
        {
            goldManager.OnGoldChanged -= UpdateGoldText;
        }

        private void UpdateGoldText(int gold)
        {
            goldText.text = $" : {gold}";
        }
    }
}