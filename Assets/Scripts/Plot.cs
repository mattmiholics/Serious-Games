using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color startColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color notEnoughPointsColor;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        startColor = sr.color; 
    }

    // Update is called once per frame
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }
    private void OnMouseDown()
    {
        if (tower != null) return;

        int towerCost = BuildManager.main.GetSelectedTowerCost();

        if (gameManager.SpendPoints(towerCost))
        {
            GameObject towerPrefab = BuildManager.main.GetSelectedTower();
            tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
            gameManager.towersList.Add(tower);
        }
        else
        {
            // Change color to indicate not enough points
            sr.color = notEnoughPointsColor;
            // Optionally, reset the color after a short delay
            StartCoroutine(ResetColorAfterDelay(1.0f)); // 1 second delay
        }

    }
    private IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (tower == null) // Check if a tower has not been built in the meantime
        {
            sr.color = startColor;
        }
    }
}
