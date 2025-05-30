using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Tower
{
    public static class TowerUpgradeSystem
    {
        private static Dictionary<AugmentType, float> buffs = new();

        public static Dictionary<AugmentType, float> GetAllBonuses()
        {
            return new Dictionary<AugmentType, float>(buffs);
        }

        public static void ApplyGlobalAugment(AugmentData augment)
        {
            if (!buffs.ContainsKey(augment.Type))
                buffs[augment.Type] = 0;

            buffs[augment.Type] += augment.actualValue;

            foreach (var tower in GameObject.FindObjectsOfType<Tower>())
                tower.ApplyAugment(buffs);
        }
    }
}