using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public string gameStart;
    

    public void GameStartButton()
    {
        SceneManager.LoadScene(gameStart);
    }
    
    public void Maingo() // 메인 고 이지랄
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
