using System.Collections;
using System.Collections.Generic;
using TowerDefense.Enemy;
using UnityEngine;

public class TigerSkill : MonoBehaviour
{
    private float bleedDuration = 5f;
    private float totalBleedDamage = 50f;
    private int tickCount = 5; // 1√ ∏∂¥Ÿ 10 √— 50

    public void ApplyBleed(GameObject target)
    {
        if (target.TryGetComponent(out EnemyHealth health))
        {
            target.GetComponent<MonoBehaviour>().StartCoroutine(BleedCoroutine(health));
        }
    }

    private IEnumerator BleedCoroutine(EnemyHealth enemy)
    {
        float tickDamage = totalBleedDamage / tickCount;
        float tickInterval = bleedDuration / tickCount;

        int ticked = 0;
        while (ticked < tickCount && enemy != null)
        {
            enemy.TakeDamage(tickDamage);
            ticked++;
            yield return new WaitForSeconds(tickInterval);
        }
    }
}