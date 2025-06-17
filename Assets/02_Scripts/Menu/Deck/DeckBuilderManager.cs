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
        Debug.Log("덱 저장됨: " + Path.Combine(Application.persistentDataPath, SaveFilePath));
    }

    public bool IsDeckSaved()
    {
        string path = Path.Combine(Application.persistentDataPath,SaveFilePath);
        if (!File.Exists(path)) return false;
        
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json)) return false;

        TowerDeckData data = JsonUtility.FromJson<TowerDeckData>(json);
        return data != null && data.selectedTowerIDs != null && data.selectedTowerIDs.Count > 0;
    }
}