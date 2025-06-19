using System.Collections;
using System.Collections.Generic;
using TowerDefense.Enemy;
using UnityEngine;

public class LionSkill : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f;  // �̵� �ӵ� 50% ����
    [SerializeField] private float slowDuration = 15f;     // 15�ʰ� �̵� �ӵ� ����
    [SerializeField] private float cooldown = 45f;         // ��Ÿ��

    private bool isCooldown = false;

    private void Start()
    {
        StartCoroutine(SlowRoutine());
    }

    private IEnumerator SlowRoutine()
    {
        while (true)
        {
            if (!isCooldown)
            {
                ApplySlowToAll();
                isCooldown = true;
                yield return new WaitForSeconds(cooldown);
                isCooldown = false;
            }
            yield return null;
        }
    }

    private void ApplySlowToAll()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.ModifySpeed(slowMultiplier, slowDuration);
            }
        }
    }
}
