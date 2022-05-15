using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();

    void Start()
    {
        Invoke("SpawnEnemyMethod", 2f);
    }

    void SpawnEnemyMethod()
    {
        int randomNumber = Random.Range(0, enemyTypes.Count);
        GameObject enemy = Instantiate(enemyTypes[randomNumber], transform.position, Quaternion.identity);
        SpawnManager.instance.AddEnemyToCounter();
        SpawnManager.instance.itHasStarted = true;
        Destroy(gameObject);
    }
}
