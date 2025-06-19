using System.Collections;
using System.Collections.Generic;
using TowerDefense.Tower;
using UnityEngine;

public class FalconSkill : MonoBehaviour
{
    [SerializeField] private float attackSpeedIncreasePercent = 10f; // ���� �ӵ� ���� %
    [SerializeField] private float buffDuration = 10f;  // ���� ���� �ð�
    [SerializeField] private float cooldown = 60f;      // ��ų ��Ÿ��

    private bool isCooldown = false;

    private void Start()
    {
        StartCoroutine(BuffRoutine());
    }

    private IEnumerator BuffRoutine()
    {
        while (true)
        {
            if (!isCooldown)
            {
                ApplyBuffToAll();
                isCooldown = true;
                yield return new WaitForSeconds(cooldown);
                isCooldown = false;
            }
            yield return null;
        }
    }

    private void ApplyBuffToAll()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Team");

        foreach (GameObject ally in allies)
        {
            Tower tower = ally.GetComponent<Tower>();
            if (tower != null)
            {
                tower.StartCoroutine(AttackSpeedBuff(tower));
            }
        }
    }

    private IEnumerator AttackSpeedBuff(Tower tower)
    {
        tower.ApplyAugment(new System.Collections.Generic.Dictionary<AugmentType, float>
        {
            { AugmentType.AttackSpeed, attackSpeedIncreasePercent / 100f }
        });

        yield return new WaitForSeconds(buffDuration);

        tower.ApplyAugment(new System.Collections.Generic.Dictionary<AugmentType, float>
        {
            { AugmentType.AttackSpeed, 0f }
        });
    }
}
