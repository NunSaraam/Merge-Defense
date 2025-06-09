using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSkill : MonoBehaviour
{
    [Header("Skill Prefabs")]
    public GameObject lionSkillPf;      // 스킬 프리팹 적용 ( 원하는 범위의 크기로 )
                                        // 스킬 프리팹에 LionSkillP Script 적용


    // 몬스터 스크립트에 추가 / OnTriggerEnter2D 사용하여 Lion Tag에 닿을 시 15초 동안 이동 속도 /2
    // Lion Tag에 닿으면 변수 추가 / 변수가 있을 시 15초 후에는 원래 이동 속도로 복구 및 변수 제거


    [SerializeField] private float cooldown;
    private float dCooldown = 45f;

    // Update
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            SkillL();
        }
    }

    void SkillL()
    {
        cooldown += dCooldown;

        Transform towerTransform = transform;

        Instantiate(lionSkillPf, towerTransform.position, towerTransform.rotation);
    }
}
