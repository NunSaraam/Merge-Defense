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
            Debug.LogError("DeckBuilderManager�� MenuManager�� ������� �ʾҽ��ϴ�!");
            return;
        }

        if (!deckBuilderManager.IsDeckSaved())
        {
            Debug.Log("���� ������� �ʾҽ��ϴ�.");
            deckWariningTextUI.SetActive(true);
            StartCoroutine(HideDeck(1f));
            return;
        }

        Debug.Log("���� ����Ǿ����Ƿ� ������ �����մϴ�.");
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
