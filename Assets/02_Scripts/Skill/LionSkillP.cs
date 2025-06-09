using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSkillP : MonoBehaviour
{
    // 사자 스킬 프리팹에 적용

    private float rTime = 1f;

    // Start
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        if (GetComponent<Collider2D>() == null)                 // Collider 없을 경우 대비
        {
            gameObject.AddComponent<CircleCollider2D>();        // Circle Collider 생성
        }

        if (!CompareTag("Lion"))                                // 태그가 Lion 이 아닐 시
        {
            gameObject.tag = "Lion";                            // 태그를 Lion 로 변경
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
