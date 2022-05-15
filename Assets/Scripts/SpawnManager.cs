using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int maxEnemyCount;
    public int enemyCount;
    public float enemySpawnRadius;

    Vector2 playerPos;
    public static SpawnManager instance;
    //List<GameObject> enemies = new List<GameObject>();

    //bool chestSpawned => (GameObject.FindObjectOfType<ChestController>() != null);
    //[SerializeField] GameObject chestPrefab;
    [SerializeField] GameObject enemySpawnerPrefab;

    public float timeBetweenRounds;
    float timeBetweenRoundsCount;

    public bool itHasStarted = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Invoke("SpawnFirstWave", 0.5f);
    }

    void SpawnFirstWave()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (GameManager.instance.skipTutorial)
        {
            StartCoroutine(SpawnEnemies2(maxEnemyCount));
        }
        else
        {
            StartCoroutine(SpawnEnemies2(1));
        }
        ResetTimer();
    }

    void Update()
    {
        TimerDown();
        SpawnNextRound();
    }

    void SpawnNextRound()
    {
        if ((timeBetweenRoundsCount <= 0 || enemyCount <= 0) && itHasStarted && !isRunningSpawnEnemies2)
        {
            StartCoroutine(SpawnEnemies2(maxEnemyCount));
            ResetTimer();
        }
    }

    public void AddEnemyToCounter()
    {
        //enemies.Add(enemy);
        //enemyCount = enemies.Count;
        enemyCount++;
    }

    public void TimerDown()
    {
        timeBetweenRoundsCount -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timeBetweenRoundsCount = timeBetweenRounds;
    }

    //public void FillEnemyList()
    //{
    //    foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    //    {
    //        //enemies.Add(enemy);
    //    }
    //    //enemyCount = enemies.Count;
    //}

    public void RemoveEnemyFromCounter()
    {
        //enemies.Remove(enemy);
        //enemyCount = enemies.Count;

        enemyCount--;
        if (enemyCount <= 0)
        {
            timeBetweenRoundsCount = timeBetweenRounds;
        }
    }

    bool isRunningSpawnEnemies2 = false;
    public IEnumerator SpawnEnemies2(int amount)
    {
        isRunningSpawnEnemies2 = true;
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPos = playerPos + Random.insideUnitCircle * enemySpawnRadius;
            if (Vector2.Distance(spawnPos, playerPos) > 5f && !Physics2D.OverlapCircle(spawnPos, enemySpawnerPrefab.GetComponent<CircleCollider2D>().radius, LayerMask.GetMask("Spawner")) &&
                Physics2D.OverlapCircle(spawnPos, enemySpawnerPrefab.GetComponent<CircleCollider2D>().radius, LayerMask.GetMask("Floor")))
            {
                yield return new WaitForSeconds(0.5f);
                GameObject enemySpawner = Instantiate(enemySpawnerPrefab, spawnPos, Quaternion.identity);
                AddEnemyToCounter();
            }
            else
            {
                i--;
            }
        }
        isRunningSpawnEnemies2 = false;
    }

    public void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPos = playerPos + Random.insideUnitCircle * enemySpawnRadius;
            if (Vector2.Distance(spawnPos, playerPos) > 10f && !Physics2D.OverlapCircle(spawnPos, enemySpawnerPrefab.GetComponent<CircleCollider2D>().radius))
            {
                GameObject enemySpawner = Instantiate(enemySpawnerPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }
}
