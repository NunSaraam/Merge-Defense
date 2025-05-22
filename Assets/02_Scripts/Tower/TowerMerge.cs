using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerMerge : MonoBehaviour
    {
        public bool TryMerge(Tower a, Tower b)
        {
            if (a.currentTowerLevel == b.currentTowerLevel)
            {
                a.UpgradeTowerData();
                Destroy(b.gameObject);
                return true;
            }
            return false;
        }
    }
}