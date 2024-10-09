using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToMainMenu : MonoBehaviour
{
    public Button mainMenuButton;

    //mainmenuFile
    public string mainMenuSceneName;

    private void Start()
    {
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OpenMainMenu);
        }
        else
        {
            Debug.LogError("MainMenuButton is not assigned in the Inspector");
        }
    }

    void OpenMainMenu()
    {
        SceneManager.UnloadSceneAsync("TerrainZelda");
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
