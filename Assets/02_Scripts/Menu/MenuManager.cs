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
    
    public void Maingo()
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
