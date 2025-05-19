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
        //공격 시 발동할 조건문
        if (attackCount == 3)
        {
            Critical();
            attackCount = 1;
        }
        else
        {
            //일반 공격
            attackCount++;
        }
    }

    // Critical
    void Critical()
    {
        float criticalDamage = attackDamage * 2;
        //공격 (대미지를 attackDamage -> criticalDamage 로 적용)
    }
}
