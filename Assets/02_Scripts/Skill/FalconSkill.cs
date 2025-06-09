using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconSkill : MonoBehaviour
{
    [Header("Skill Prefabs")]
    public GameObject falconSkillPf;      // 스킬 프리팹 적용 ( 원하는 범위의 크기로 )
    // 스킬 프리팹에 FalconSkillP Script 적용


    // 타워 스크립트에 추가 / OnTriggerEnter2D 사용하여 Falcon Tag에 닿을 시 10초 동안 공격 속도 10% 증가
    // Falcon Tag에 닿으면 변수 추가 / 변수가 있을 시 10초 후에는 원래 공격 속도로 복구 및 변수 제거


    [SerializeField] private float cooldown;
    private float dCooldown = 60f;

    // Update
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            SkillF();
        }
    }

    void SkillF()
    {
        cooldown += dCooldown;

        Transform towerTransform = transform;

        Instantiate(falconSkillPf, towerTransform.position, towerTransform.rotation);
    }
}
