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
                Debug.LogWarning("덱 파일을 찾을 수 없음. 기본값 사용");
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