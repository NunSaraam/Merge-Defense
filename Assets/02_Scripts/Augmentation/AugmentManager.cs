using UnityEngine;
using System.Collections.Generic;
using TowerDefense.UI;

namespace TowerDefense.Tower
{
    public class AugmentManager : MonoBehaviour
    {
        [SerializeField] private List<AugmentData> allAugments;
        [SerializeField] private GameObject augmentUIPanel;
        [SerializeField] private AugmentUIOption[] uiOptions;

        private System.Action onAugmentComplete;

        public void OpenAugmentSelection(System.Action onComplete)
        {
            onAugmentComplete = onComplete;

            List<AugmentData> randomOptions = GetRandomAugments(3);
            for (int i = 0; i < uiOptions.Length; i++)
            {
                uiOptions[i].Initialize(randomOptions[i], ApplyAugment, RefreshSingleOption);
            }

            augmentUIPanel.SetActive(true);
        }

        private void ApplyAugment(AugmentData selected)
        {
            TowerUpgradeSystem.ApplyGlobalAugment(selected);
            augmentUIPanel.SetActive(false);
            onAugmentComplete?.Invoke();
        }

        private void RefreshSingleOption(AugmentUIOption option)
        {
            var old = option.GetData();
            var pool = new List<AugmentData>(allAugments);
            pool.Remove(old);

            if (pool.Count == 0) return;

            var replacement = pool[Random.Range(0, pool.Count)];
            option.ReplaceAugment(replacement);
        }

        private List<AugmentData> GetRandomAugments(int count)
        {
            List<AugmentData> copy = new(allAugments);
            List<AugmentData> result = new();

            for (int i = 0; i < count && copy.Count > 0; i++)
            {
                int index = Random.Range(0, copy.Count);
                result.Add(copy[index]);
                copy.RemoveAt(index);
            }

            return result;
        }
    }
}