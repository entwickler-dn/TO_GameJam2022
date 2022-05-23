using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPortalScript : MonoBehaviour
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
        if (!FindObjectOfType<TutorialManager>().canAddPoints)
        {
            littleTimerCount -= Time.deltaTime;
            if (littleTimerCount <= 0)
            {
                FindObjectOfType<TutorialManager>().canAddPoints = true;
                littleTimerCount = littleTimer;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyKnockedOut") && FindObjectOfType<TutorialManager>().canAddPoints)
        {
            enemyPortal = col.gameObject;
            FindObjectOfType<TutorialManager>().AddPoints(col.gameObject.GetComponent<EnemyHealth>().points);
            SpawnManager.instance.RemoveEnemyFromCounter();
            Destroy(col.gameObject);
            PlayerShoot.instance.launchedEnemy = null;
        }
    }
}
