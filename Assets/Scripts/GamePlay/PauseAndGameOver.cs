using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseAndGameOver : MonoBehaviour
{
    private bool isPaused = false;

    public Sprite playSprite;
    public Sprite pauseSprite;


    public GameObject pauseMenuUI;

    public Image buttonImage;

    public Button resumeButton;

    public Button restartButton;

    public Button playButton;


    private void Start()
    {
        pauseMenuUI.SetActive(false);

        //restartButton.onClick.AddListener();
    }


    public void togglePause()
    {
        isPaused = !isPaused;

        pauseMenuUI.SetActive(true);
         

        if (isPaused)
        {
            PauseGame();
        }

        else
        {
            ResumeGame();
        }

        
    }


    void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0;

        //buttonImage.sprite = pauseSprite;

        pauseMenuUI.SetActive(true);
    }


    void ResumeGame()
    {
        Time.timeScale = 1;

        //buttonImage.sprite = pauseSprite;

        pauseMenuUI.SetActive(false);

        isPaused = false;
    }


}
