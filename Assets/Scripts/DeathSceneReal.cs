using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Primary Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame2()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void PlayGame3()
    {
        SceneManager.LoadScene("LoseScreen");
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
