using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerMerge : MonoBehaviour
    {
        public bool TryMerge(Tower a, Tower b)
        {
            if (a.currentTowerName == b.currentTowerName)
            {
                a.UpgradeTowerData();
                Destroy(b.gameObject);
                return true;
            }
            return false;
        }
    }
}