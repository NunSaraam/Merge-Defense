using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Tower
{
    [CreateAssetMenu(fileName = "TowerDatabase", menuName = "Tower/TowerDatabase")]
    public class TowerDatabase : ScriptableObject
    {
        public List<TowerData> commonTowers;
        public List<TowerData> rareTowers;
        public List<TowerData> epicTowers;
        public List<TowerData> legendaryTowers;
        public List<TowerData> mythicalTowers;

        private HashSet<string> deckTowerNames = null;

        public void SetDeckFilter(List<string> towerNames)
        {
            deckTowerNames = new HashSet<string>(towerNames);
        }

        public List<TowerData> GetAllTowersByType(TowerType rarity)
        {
            return rarity switch
            {
                TowerType.Common => commonTowers,
                TowerType.Rare => rareTowers,
                TowerType.Epic => epicTowers,
                TowerType.Legendary => legendaryTowers,
                TowerType.Mythical => mythicalTowers,
                _ => new List<TowerData>()
            };
        }

        public List<TowerData> GetTowersByType(TowerType rarity)
        {
            var pool = GetAllTowersByType(rarity);

            if (deckTowerNames == null || deckTowerNames.Count == 0)
                return pool;

            List<TowerData> filtered = new();
            foreach (var tower in pool)
            {
                if (deckTowerNames.Contains(tower.TowerName))
                    filtered.Add(tower);
            }
            return filtered;
        }

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetDeckFilter()
        {
            var dbs = Resources.LoadAll<TowerDatabase>("");
            foreach (var db in dbs)
                db.SetDeckFilter(null);
        }
#endif
    }
}