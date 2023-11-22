using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public Button waveButton;
    public Image pauseImg;
    public Image playImg;
    private enum GameState { Playing, Paused, BetweenWaves }
    private GameState currentState = GameState.BetweenWaves;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        spawnedEnemyTypes = new HashSet<int>();
        main = this;
    }

    private void Start()
    {
        waveButton.onClick.AddListener(OnWaveButtonClicked); 
        StartCoroutine(StartWave());
        main = this;
    }
    void Update()
    {
        if (currentState == GameState.Playing)
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

            if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
            {
                EndWave();
            }
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

        if (!spawnedEnemyTypes.Contains(Array.IndexOf(enemyTypes, typeToSpawn)))
        {
            SpawnUI(typeToSpawn);
            spawnedEnemyTypes.Add(Array.IndexOf(enemyTypes, typeToSpawn));
        }
    }

    void OnWaveButtonClicked()
    {
        if (currentState == GameState.BetweenWaves)
        {
            StartCoroutine(StartWave());
        }
        else if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
        else if (currentState == GameState.Playing)
        {
            
            PauseGame();
        }
    }

    public EnemyType SelectEnemyType()
    {
        for (int i = enemyTypes.Length - 1; i >= 0; i--)
        {
            if (currantWave >= enemyTypes[i].startWave)
                return enemyTypes[i];
        }
        return enemyTypes[0]; 
    }
    IEnumerator StartWave()
    {

        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        currentState = GameState.Playing;
        Time.timeScale = 1; // Ensure game is unpaused
    }

    void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currantWave++;
        currentState = GameState.BetweenWaves;
        pauseImg.gameObject.SetActive(false);
        playImg.gameObject.SetActive(true);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        currentState = GameState.Paused;

        pauseImg.gameObject.SetActive(true);
        playImg.gameObject.SetActive(false);

    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        currentState = GameState.Playing;

        pauseImg.gameObject.SetActive(false);
        playImg.gameObject.SetActive(true);
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies*Mathf.Pow(currantWave,difficultyScalar));
    }
    void SpawnUI(EnemyType typeToSpawn)
    {
        Instantiate(typeToSpawn.uiPrefab, uiParent);
        Time.timeScale = 0; 
    }
}
