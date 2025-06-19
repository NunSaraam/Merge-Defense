using System.Collections;
using System.Collections.Generic;
using TowerDefense.Player;
using UnityEngine;

public class RaccoonSkill : MonoBehaviour
{
    [SerializeField] private float randomChance = 20f;  // 골드 획득 확률
    [SerializeField] private int goldAmount = 1;        // 골드 획득량

    private GoldManager goldManager;

    private void Start()
    {
        goldManager = FindObjectOfType<GoldManager>();
        if (goldManager == null)
            Debug.LogError("GoldManager를 찾을 수 없음.");
    }

    public void RandomGold()
    {
        float roll = Random.Range(0f, 100f);
        if (roll <= randomChance)
        {
            goldManager.AddGold(goldAmount);
            Debug.Log("골드 +" + goldAmount);
        }
    }
}
