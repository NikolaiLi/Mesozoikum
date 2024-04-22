using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame2()
    {
        SceneManager.LoadScene("Controls");
    }
    public void PlayGame3()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void PlayGame4()
    {
        SceneManager.LoadScene("WinScreen");
    }
    public void PlayGame5()
    {
        SceneManager.LoadScene("Map");
    }
}
