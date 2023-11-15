using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyType[] enemyTypes;

    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.8f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalar = 0.75f;

    [SerializeField] public int currantWave = 1;
    [SerializeField] private float timeSinceLastSpawn;
    [SerializeField] private int enemiesAlive;
    [SerializeField] private int enemiesLeftToSpawn;
    [SerializeField] private bool isSpawning = false;

    public static EnemySpawner main;
    public Transform uiParent;

    private HashSet<int> spawnedEnemyTypes;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        spawnedEnemyTypes = new HashSet<int>();
        main = this;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
        main = this;
    }
    void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemies();

            enemiesLeftToSpawn--;
            enemiesAlive++;

            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }
    void EnemyDestroyed() 
    {
        enemiesAlive--;
    }
    void SpawnEnemies()
    {
        EnemyType typeToSpawn = SelectEnemyType();
        GameManager.main.enemiesList.Add(Instantiate(typeToSpawn.prefab, GameManager.main.startPoint.position, Quaternion.identity));

        // Check if this enemy type has been spawned before
        if (!spawnedEnemyTypes.Contains(Array.IndexOf(enemyTypes, typeToSpawn)))
        {
            // Spawn UI prefab for the first time
            SpawnUI(typeToSpawn);
            spawnedEnemyTypes.Add(Array.IndexOf(enemyTypes, typeToSpawn));
        }
    }

    public EnemyType SelectEnemyType()
    {
        // Logic to select enemy type based on current wave
        for (int i = enemyTypes.Length - 1; i >= 0; i--)
        {
            if (currantWave >= enemyTypes[i].startWave)
                return enemyTypes[i];
        }
        return enemyTypes[0]; // Default to the first type if none match
    }
    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currantWave++;
        StartCoroutine(StartWave());
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies*Mathf.Pow(currantWave,difficultyScalar));
    }
    void SpawnUI(EnemyType typeToSpawn)
    {
        Instantiate(typeToSpawn.uiPrefab, uiParent);
        Time.timeScale = 0; // This pauses the game
    }
}
