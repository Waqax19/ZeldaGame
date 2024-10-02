using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseAndGameOver : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);

        //resumeButton.onClick.AddListener(ResumeGame);
    }

    public void togglePause()
    {
        if(isPaused)
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1;
            isPaused = false;
        }

        else
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0;
            isPaused = true;

        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1;

        isPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;
    }
    public void GoToMainMenu()
    {
        Debug.Log("Going to main menu..");
    }


}
