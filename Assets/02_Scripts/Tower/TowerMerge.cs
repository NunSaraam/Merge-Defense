using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerMerge : MonoBehaviour
    {
        public bool TryMerge(Tower a, Tower b)
        {
            if (a.level == b.level)
            {
                a.Upgrade();
                Destroy(b.gameObject);
                return true;
            }
            return false;
        }
    }
}