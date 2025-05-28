using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerMergeManager : MonoBehaviour
    {
        [SerializeField] private TowerDatabase towerDatabase;
        [SerializeField] private GameObject towerPrefab;

        public void Merge(Tower a, Tower b, Transform targetSlot)
        {
            if (a.CurrentType >= TowerType.Mythical) return;

            TowerType nextType = a.CurrentType + 1;
            var pool = towerDatabase.GetTowersByType(nextType);
            var newData = pool[Random.Range(0, pool.Count)];

            Destroy(a.gameObject);
            Destroy(b.gameObject);

            GameObject merged = Instantiate(towerPrefab, targetSlot.position, Quaternion.identity, targetSlot);
            if (merged.TryGetComponent(out Tower tower))
            {
                tower.SetTowerData(nextType, newData);
            }
        }
    }
}