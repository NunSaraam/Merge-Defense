using UnityEngine;
using System.Collections.Generic;
using TowerDefense.Tower;

[CreateAssetMenu(fileName = "TowerDatabase", menuName = "Tower/TowerDatabase")]
public class TowerDatabase : ScriptableObject
{
    public List<TowerData> commonTowers;
    public List<TowerData> rareTowers;
    public List<TowerData> epicTowers;
    public List<TowerData> legendaryTowers;
    public List<TowerData> mythicalTowers;

    /* TowerDatabase내 덱 설정 구현 메서드
    public void SetTowerDeck(bool[][] towerIndex)
    {
        for(int i = 0; i < towerIndex[0].Length; i++)
        {
            for (int j = 0; j < towerIndex[i].Length; j++)
            {
                if (towerIndex[i][j] == true)
                {
                    switch(i)
                    {
                        case 0 :
                            commonTowers.RemoveAt(j);
                        break;
                        case 1 :
                            rareTowers.RemoveAt(j);
                        break;
                        case 2 :
                            epicTowers.RemoveAt(j);
                        break;
                        case 3 :
                            legendaryTowers.RemoveAt(j);
                        break;
                        case 4 :
                            mythicalTowers.RemoveAt(j);
                        break;
                    }
                }
            }
        }
    }
    */

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