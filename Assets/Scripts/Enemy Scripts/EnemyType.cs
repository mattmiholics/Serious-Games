using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyType : MonoBehaviour
{
        public GameObject prefab; // Reference to the EnemyBase (or derived class) prefab
        public GameObject uiPrefab; // Prefab for the UI element showing the description
        public int startWave; // The wave number from which this enemy starts appearing
}
