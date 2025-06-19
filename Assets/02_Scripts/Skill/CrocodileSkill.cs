using System.Collections;
using System.Collections.Generic;
using TowerDefense.Enemy;
using UnityEngine;

public class CrocodileSkill : MonoBehaviour
{
    private int attackCount = 0;
    private float stunDuration = 1.5f;
    private float effectRange = 3f;

    public void OnAttack(Vector3 centerPosition)
    {
        attackCount++;
        if (attackCount >= 5)
        {
            attackCount = 0;
            ApplyStun(centerPosition);
        }
    }

    private void ApplyStun(Vector3 center)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(center, effectRange, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemyCol in enemies)
        {
            EnemyMovement enemy = enemyCol.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration);
            }
        }
    }
}