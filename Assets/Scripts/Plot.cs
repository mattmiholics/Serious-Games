using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color startColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
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

        GameObject towerToBuild = BuildManager.main.GetSelectedTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
        gameManager.towersList.Add(tower);

    }
}
