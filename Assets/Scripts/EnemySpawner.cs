using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;

    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.8f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalar = 0.75f;

    [SerializeField] private int currantWave = 1;
    [SerializeField] private float timeSinceLastSpawn;
    [SerializeField] private int enemiesAlive;
    [SerializeField] private int enemiesLeftToSpawn;
    [SerializeField] private bool isSpawning = false;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }
    void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond))
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
        GameObject prefabToSpawn = enemyPrefab[0];
        GameManager.main.enemiesList.Add(Instantiate(prefabToSpawn, GameManager.main.startPoint.position, Quaternion.identity));
        
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
}
