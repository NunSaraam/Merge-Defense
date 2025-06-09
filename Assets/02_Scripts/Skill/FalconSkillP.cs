using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconSkillP : MonoBehaviour
{
    // 매 스킬 프리팹에 적용

    private float rTime = 1f;

    // Start
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        if (GetComponent<Collider2D>() == null)                 // Collider 없을 경우 대비
        {
            gameObject.AddComponent<CircleCollider2D>();        // Circle Collider 생성
        }

        if (!CompareTag("Falcon"))                              // 태그가 Falcon 이 아닐 시
        {
            gameObject.tag = "Falcon";                          // 태그를 Falcon 로 변경
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
