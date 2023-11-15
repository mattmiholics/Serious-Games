using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismiss : MonoBehaviour
{
    public void DismissUI()
    { 
        Time.timeScale = 1; 
        Destroy(this.gameObject);
    }
}
