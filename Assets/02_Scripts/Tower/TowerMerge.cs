using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerMerge : MonoBehaviour
    {
        [SerializeField] private TowerDatabase towerDatabase;
        [SerializeField] private GameObject towerPrefab;

        public bool TryMerge(Tower a, Tower b)
        {
            if (a == null || b == null || a == b) return false;

            var dataA = a.GetCurrentData();
            var dataB = b.GetCurrentData();

            if (a.CurrentType != b.CurrentType) return false;
            if (dataA.TowerName != dataB.TowerName) return false;
            if (a.CurrentType == TowerType.Mythical) return false;

            TowerType nextType = a.CurrentType + 1;
            var candidates = towerDatabase.GetTowersByType(nextType);
            var newData = candidates[Random.Range(0, candidates.Count)];

            a.SetTowerData(nextType, newData); // Tower Merge

            Destroy(b.gameObject);

            return true;
        }
    }
}