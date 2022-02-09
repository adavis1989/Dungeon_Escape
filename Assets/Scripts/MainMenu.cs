using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MenuButton()
    {
        menuPanel.SetActive(true);
    }

    public void MenuExit()
    {
        menuPanel.SetActive(false);
    }
}
