using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSelector : MonoBehaviour
{
    public void setTurretSelected0()
    {
        BuildManager.main.selectedTower = 0;
    }
    public void setTurretSelected1()
    {
        BuildManager.main.selectedTower = 1;
    }
}
