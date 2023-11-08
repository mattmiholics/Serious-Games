using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour 
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI wavesText;
    public TextMeshProUGUI healthText;

    public GameObject[] towerButtons;

    void Update()
    {
        pointsText.text = "Points: " + GameManager.main.points.ToString();
        healthText.text = "Health: " + GameManager.main.lives.ToString();
        wavesText.text = "Wave: " + EnemySpawner.main.currantWave.ToString();
    }


}
