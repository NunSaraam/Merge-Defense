using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSkillP : MonoBehaviour
{
    // ���� ��ų �����տ� ����

    private float rTime = 1f;

    // Start
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        if (GetComponent<Collider2D>() == null)                 // Collider ���� ��� ���
        {
            gameObject.AddComponent<CircleCollider2D>();        // Circle Collider ����
        }

        if (!CompareTag("Lion"))                                // �±װ� Lion �� �ƴ� ��
        {
            gameObject.tag = "Lion";                            // �±׸� Lion �� ����
        }
    }

    // Update
    void Update()
    {
        rTime -= Time.deltaTime;

        if (rTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
