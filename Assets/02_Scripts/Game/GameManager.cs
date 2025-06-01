using UnityEngine;
using System.IO;
using TowerDefense.Tower;

namespace TowerDefense.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TowerDatabase towerDatabase;
        [SerializeField] private string deckFileName = "deck.json";

        public TowerDeckData DeckData { get; private set; }

        private void Awake()
        {
            LoadDeck();
        }

        private void LoadDeck()
        {
            string path = Path.Combine(Application.persistentDataPath, deckFileName);

            if (!File.Exists(path))
            {
                Debug.LogWarning("Deck file not found. Using default.");
                DeckData = new TowerDeckData { selectedTowerIDs = new() };
            }
            else
            {
                string json = File.ReadAllText(path);
                DeckData = JsonUtility.FromJson<TowerDeckData>(json);
            }

            towerDatabase.SetDeckFilter(DeckData.selectedTowerIDs);
        }
    }
}