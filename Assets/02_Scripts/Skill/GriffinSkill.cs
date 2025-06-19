using System.Collections;
using System.Collections.Generic;
using TowerDefense.Tower;
using UnityEngine;

public class GriffinSkill : MonoBehaviour
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
            tower.ApplyBonusCritChance(0.2f);   // +20%
        }
    }

    private void RemoveBuffFromAll()
    {
        Tower[] towers = FindObjectsOfType<Tower>();
        foreach (Tower tower in towers)
        {
            tower.ApplyBonusCritChance(-0.2f);  // -20%
        }
    }
}