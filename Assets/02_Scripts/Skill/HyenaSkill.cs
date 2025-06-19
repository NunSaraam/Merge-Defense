using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyenaSkill : MonoBehaviour
{
    private int attackCount = 0;
    private bool nextAttackCritical = false;

    public void OnAttack()
    {
        attackCount++;
        if (attackCount >= 3)
        {
            nextAttackCritical = true;
            attackCount = 0;
        }
    }

    public bool TryConsumeCritical()
    {
        if (nextAttackCritical)
        {
            nextAttackCritical = false;
            return true;
        }
        return false;
    }
}
