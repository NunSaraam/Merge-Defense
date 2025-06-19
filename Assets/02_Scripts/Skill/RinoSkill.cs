using System.Collections;
using System.Collections.Generic;
using TowerDefense.Enemy;
using UnityEngine;

public class RinoSkill : MonoBehaviour
{
    [SerializeField] private float stunDuration = 3f;   // ���� ���ӽð�
    [SerializeField] private float cooldown = 15f;      // ��Ÿ��
    [SerializeField] private float stunRange = 5f;      // ���� ���� �ݰ�

    private bool isCooldown = false;

    private void Start()
    {
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        while (true)
        {
            if (!isCooldown)
            {
                TriggerStun();
                isCooldown = true;
                yield return new WaitForSeconds(cooldown);
                isCooldown = false;
            }
            yield return null;
        }
    }

    private void TriggerStun()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float closestDist = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy == null)
            return;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(nearestEnemy.transform.position, enemy.transform.position);
            if (dist <= stunRange)
            {
                EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
                if (movement != null)
                {
                    movement.ApplyStun(stunDuration);
                }
            }
        }
    }
}