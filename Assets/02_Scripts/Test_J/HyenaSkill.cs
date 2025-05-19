using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyenaSkill : MonoBehaviour
{
    public float attackDamage = 10f;

    private int attackCount = 1;

    // Start
    void Start()
    {
        attackCount = 1;
    }

    // Update
    void Update()
    {
        //���� �� �ߵ��� ���ǹ�
        if (attackCount == 3)
        {
            Critical();
            attackCount = 1;
        }
        else
        {
            //�Ϲ� ����
            attackCount++;
        }
    }

    // Critical
    void Critical()
    {
        float criticalDamage = attackDamage * 2;
        //���� (������� attackDamage -> criticalDamage �� ����)
    }
}
