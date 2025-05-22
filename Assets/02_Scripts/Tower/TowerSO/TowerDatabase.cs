using UnityEngine;
using System.Collections.Generic;
using TowerDefense.Tower;

[CreateAssetMenu(fileName = "TowerDatabase", menuName = "Tower/Tower Database")]
public class TowerDatabase : ScriptableObject
{
    public List<TowerData> commonTowers;
    public List<TowerData> rareTowers;
    public List<TowerData> epicTowers;
    public List<TowerData> legendaryTowers;
    public List<TowerData> mythicalTowers;

    public List<TowerData> GetTowersByType(TowerType rarity)
    {
        switch (rarity)
        {
            case TowerType.Common:
                return commonTowers;
            case TowerType.Rare:
                return rareTowers;
            case TowerType.Epic:
                return epicTowers;
            case TowerType.Legendary:
                return legendaryTowers;
            case TowerType.Mythical:
                return mythicalTowers;
            default:
                return null;
        }
    }
}