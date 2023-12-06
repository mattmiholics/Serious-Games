using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemiesList = new List<GameObject>();
    public List<GameObject> towersList = new List<GameObject>();
    public static GameManager main;

    public GameObject loseUI;

    public Transform startPoint;
    public Transform[] path;

    public int points = 200;
    public int lives = 50;

    private void Awake()
    {
        main = this;
        loseUI.SetActive(false);
        Time.timeScale = 1;

    }
    void Start()
    {
        points = 200;
    }
    public void AddPoints(int amount) 
    {
        points += amount;
    }
    public bool SpendPoints(int amount)
    {
        if (points >= amount)
        {
            points -= amount;
            return true;
        }
        else
        {
            // Not enough points
            return false;
        }
    }
    public void LoseLives(int amount)
    {
        lives -= amount;
        Time.timeScale = 0;
        loseUI.SetActive(true);
    }

}
