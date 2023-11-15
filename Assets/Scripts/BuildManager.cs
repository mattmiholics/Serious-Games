using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    public TowerBlueprint[] towers; 

    public int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public GameObject GetSelectedTower()
    {
        return towers[selectedTower].prefab;
    }
    public int GetSelectedTowerCost()
    {
        return towers[selectedTower].cost;
    }
}
