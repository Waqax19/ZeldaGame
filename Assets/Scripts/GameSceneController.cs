using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [Header("Game")]
    public PlayerMovement player;
   

    [Header("UI")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI arrowText;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(player != null)
        {
            healthText.text = "Health : " + player.health;
            bombText.text = "Bombs : " + player.bombAmount;
            arrowText.text = "Arrows : " + player.arrowAmount;
        }

        else
        {
            healthText.text = "Health : 0";
        }
    }
}
