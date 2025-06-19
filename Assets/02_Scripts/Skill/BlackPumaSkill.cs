using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPumaSkill : MonoBehaviour
{
    private HashSet<GameObject> damagedE = new HashSet<GameObject>();

    public bool IsFirstAttack(GameObject enemy)
    {
        if (damagedE.Contains(enemy))
            return false;

        damagedE.Add(enemy);
        return true;
    }
}
