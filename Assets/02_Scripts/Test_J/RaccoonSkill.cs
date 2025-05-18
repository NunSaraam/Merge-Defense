using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonSkill : MonoBehaviour
{
    private int attackCount = 0;

    private float randomChance = 20f;

    // Start
    void Start()
    {
        attackCount = 0;
    }

    // Update
    void Update()
    {
        //공격 시 attackCount 1 증가        <- 추가 예정

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
