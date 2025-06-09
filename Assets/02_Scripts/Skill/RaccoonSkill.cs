using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonSkill : MonoBehaviour
{
    public int attackCount = 0;

    private float randomChance = 20f;

    // Start
    void Start()
    {
        attackCount = 0;
    }

    // Update
    void Update()
    {
        // 공격 스크립트에 라쿤 스킬 오브젝트 받는 코드 추가   <- 예정
        // if 타워가 라쿤일 경우  밑에 코드 실행               <- 추가 예정
        // 공격 스크립트에서 공격 시 attackCount 1 증가        <- 추가 예정

        if (attackCount > 0)
        {
            RandomGold();
        }
    }

    //RandomGold
    void RandomGold()
    {
        if(attackCount >= 1)
        {
            attackCount--;
            float randomG = Random.Range(1, 101);
            if(randomG <= randomChance)
            {
                //Gold++;       <- 추가 예정
            }
        }
    }
}
