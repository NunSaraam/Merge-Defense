using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class TowerDeckData
{
    public List<string> selectedTowerIDs;
}

public class DeckBuilderManager : MonoBehaviour
{
    [SerializeField] private Transform deckFieldRoot;
    [SerializeField] private Button saveButton;
    [SerializeField] private int deckCount = 15;

    private const string SaveFilePath = "deck.json";

    public void CheckDeckValid()
    {
        int validCount = 0;

        foreach (Transform group in deckFieldRoot)
        {
            foreach (Transform slot in group)
            {
                var deckSlot = slot.GetComponent<DeckSlot>();
                if (deckSlot != null && deckSlot.HasTower)
                    validCount++;
            }
        }

        saveButton.interactable = validCount >= deckCount;
    }

    public void SaveDeck()
    {
        TowerDeckData deckData = new() { selectedTowerIDs = new() };

        foreach (Transform group in deckFieldRoot)
        {
            foreach (Transform slot in group)
            {
                var deckSlot = slot.GetComponent<DeckSlot>();
                if (deckSlot != null && deckSlot.HasTower)
                    deckData.selectedTowerIDs.Add(deckSlot.AssignedID);
            }
        }

        string json = JsonUtility.ToJson(deckData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, SaveFilePath), json);
        Debug.Log("Deck saved: " + Path.Combine(Application.persistentDataPath, SaveFilePath));
    }
}