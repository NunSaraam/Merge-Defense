using System.Collections;
using System.Collections.Generic;
using TowerDefense.Enemy;
using UnityEngine;

public class LionSkill : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f;  // 이동 속도 50% 감소
    [SerializeField] private float slowDuration = 15f;     // 15초간 이동 속도 감소
    [SerializeField] private float cooldown = 45f;         // 쿨타임

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
