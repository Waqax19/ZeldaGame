using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseAndGameOver : MonoBehaviour
{
    private bool isPaused = false;

    public Sprite playSprite;
    public Sprite pauseSprite;

    public Image buttonImage;

    public void togglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;

        buttonImage.sprite = isPaused ? playSprite : pauseSprite;
    }

    
}
