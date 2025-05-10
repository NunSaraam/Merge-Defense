using UnityEngine;

namespace TowerDefense.Tower
{
    public class Tower : MonoBehaviour
    {
        public int level = 1;

        public void Upgrade()
        {
            level++;
            // augment upgrade reflect
        }
    }
}