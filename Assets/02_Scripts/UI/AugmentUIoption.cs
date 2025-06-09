using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace TowerDefense.UI
{
    public class AugmentUIOption : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descText;
        [SerializeField] private TMP_Text refreshCountText;
        [SerializeField] private Button selectButton;
        [SerializeField] private Button refreshButton;

        private AugmentData data;
        private System.Action<AugmentData> onSelect;
        private System.Action<AugmentUIOption> onRefresh;

        private int remainingRefresh = 1;

        public void Initialize(AugmentData augment, System.Action<AugmentData> selectCallback, System.Action<AugmentUIOption> refreshCallback = null)
        {
            data = augment;
            onSelect = selectCallback;
            onRefresh = refreshCallback;

            data.RollValue();
            UpdateDisplay();

            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => onSelect?.Invoke(data));

            refreshButton.onClick.RemoveAllListeners();
            refreshButton.onClick.AddListener(() =>
            {
                if (remainingRefresh > 0)
                {
                    remainingRefresh--;
                    onRefresh?.Invoke(this);
                    UpdateDisplay();
                }
            });
        }

        public void ReplaceAugment(AugmentData newData)
        {
            data = newData;
            data.RollValue();
        }

        private void UpdateDisplay()
        {
            nameText.text = data.AugmentName;
            descText.text = $"{data.Description}\n{data.GetDisplayValue()}";
            refreshCountText.text = $"새로고침 {remainingRefresh}/1";
            refreshButton.interactable = remainingRefresh > 0;
        }

        public AugmentData GetData() => data;
    }
}
