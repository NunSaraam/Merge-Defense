using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconSkill : MonoBehaviour
{
    [Header("Skill Prefabs")]
    public GameObject falconSkillPf;      // ��ų ������ ���� ( ���ϴ� ������ ũ��� )
    // ��ų �����տ� FalconSkillP Script ����


    // Ÿ�� ��ũ��Ʈ�� �߰� / OnTriggerEnter2D ����Ͽ� Falcon Tag�� ���� �� 10�� ���� ���� �ӵ� 10% ����
    // Falcon Tag�� ������ ���� �߰� / ������ ���� �� 10�� �Ŀ��� ���� ���� �ӵ��� ���� �� ���� ����


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
