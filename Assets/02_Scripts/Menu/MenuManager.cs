using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private DeckBuilderManager deckBuilderManager;
    [SerializeField] private GameObject deckWariningTextUI;
    [SerializeField] private Button startGameButton;

    public string gameStart;
    private float deckWarningTimer = 1f;

    private void Start()
    {
        deckWariningTextUI.SetActive(false);
        startGameButton.onClick.AddListener(OnClickGameStartButton);
    }

    public void OnClickGameStartButton()
    {
        if (deckBuilderManager == null)
        {
            Debug.LogError("DeckBuilderManager가 MenuManager에 연결되지 않았습니다!");
            return;
        }

        if (!deckBuilderManager.IsDeckSaved())
        {
            Debug.Log("덱이 저장되지 않았습니다.");
            deckWariningTextUI.SetActive(true);
            StartCoroutine(HideDeck(1f));
            return;
        }

        Debug.Log("덱이 저장되었으므로 게임을 시작합니다.");
        deckWariningTextUI.SetActive(false);
        GameStart();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(gameStart);
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    private IEnumerator HideDeck(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        deckWariningTextUI.SetActive(false);
    }
}
