using System.Collections;
using System.Collections.Generic;
using TowerDefense.Tower;
using UnityEngine;

public class PegasusSkill : MonoBehaviour
{
    private void OnEnable()
    {
        ApplyBuffToAll();
    }

    private void OnDisable()
    {
        RemoveBuffFromAll();
    }

    private void ApplyBuffToAll()
    {
        Tower[] towers = FindObjectsOfType<Tower>();
        foreach (Tower tower in towers)
        {
            tower.ApplyBonusAttackSpeed(2f);    // 1 + 2 = 3
        }
    }

    private void RemoveBuffFromAll()
    {
        Tower[] towers = FindObjectsOfType<Tower>();
        foreach (Tower tower in towers)
        {
            tower.ApplyBonusAttackSpeed(-2f);   // 3 - 2 = 1
        }
    }
}