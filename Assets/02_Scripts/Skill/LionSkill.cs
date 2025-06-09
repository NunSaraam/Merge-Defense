using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSkill : MonoBehaviour
{
    [Header("Skill Prefabs")]
    public GameObject lionSkillPf;      // ��ų ������ ���� ( ���ϴ� ������ ũ��� )
                                        // ��ų �����տ� LionSkillP Script ����


    // ���� ��ũ��Ʈ�� �߰� / OnTriggerEnter2D ����Ͽ� Lion Tag�� ���� �� 15�� ���� �̵� �ӵ� /2
    // Lion Tag�� ������ ���� �߰� / ������ ���� �� 15�� �Ŀ��� ���� �̵� �ӵ��� ���� �� ���� ����


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
