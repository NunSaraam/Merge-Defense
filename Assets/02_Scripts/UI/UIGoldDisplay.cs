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
            UpdateGoldText(goldManager.CurrentGold); // 초기값 표시
        }

        private void OnDisable()
        {
            goldManager.OnGoldChanged -= UpdateGoldText;
        }

        private void UpdateGoldText(int gold)
        {
            goldText.text = $"현재 골드 : {gold}";
        }
    }
}