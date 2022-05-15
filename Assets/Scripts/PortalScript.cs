using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    float littleTimer = 0.2f;
    float littleTimerCount;
    bool canGainPoints = false;

    GameObject enemyPortal;
    void Start()
    {
        
    }

    void Update()
    {
        if (!GameManager.instance.canAddPoints)
        {
            littleTimerCount -= Time.deltaTime;
            if (littleTimerCount <= 0)
            {
                GameManager.instance.canAddPoints = true;
                littleTimerCount = littleTimer;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyKnockedOut") && GameManager.instance.canAddPoints)
        {
            enemyPortal = col.gameObject;
            GameManager.instance.AddPoints(col.gameObject.GetComponent<EnemyHealth>().points);
            SpawnManager.instance.RemoveEnemyFromCounter();
            Destroy(col.gameObject);
            PlayerShoot.instance.launchedEnemy = null;
        }
    }
}
