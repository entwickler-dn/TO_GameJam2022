using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();
    GameObject enemy;
    public int totalSpawnPercentage = 0;

    void Start()
    {
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            totalSpawnPercentage += enemyTypes[i].GetComponent<EnemyHealth>().weight;
        }

        Invoke("SpawnEnemyMethod", 2f);
    }

    void SpawnEnemyMethod()
    {
        GameObject enemy;
        int randomNumber = Random.Range(0, totalSpawnPercentage);
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if (randomNumber < enemyTypes[i].GetComponent<EnemyHealth>().weight)
            {
                enemy = Instantiate(enemyTypes[i], transform.position, Quaternion.identity);
                break;
            }
            randomNumber -= enemyTypes[i].GetComponent<EnemyHealth>().weight;
        }
        SpawnManager.instance.itHasStarted = true;
        Destroy(gameObject);

        //int randomNumber = Random.Range(0, enemyTypes.Count);
        //GameObject enemy = Instantiate(enemyTypes[randomNumber], transform.position, Quaternion.identity);
        //SpawnManager.instance.AddEnemyToCounter();
        //SpawnManager.instance.itHasStarted = true;
        //Destroy(gameObject);
    }
}
