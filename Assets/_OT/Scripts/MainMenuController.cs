using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button quitButton;
    private void Start()
    {
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

}
