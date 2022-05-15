using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if(col.gameObject.CompareTag("EnemyKnockedOut"))
        //{
        //    Destroy(col.gameObject);
        //    PlayerShoot.instance.launchedEnemy = null;
        //}
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyKnockedOut"))
        {
            GameManager.instance.AddPoints(col.gameObject.GetComponent<EnemyHealth>().points);
            Destroy(col.gameObject);
            PlayerShoot.instance.launchedEnemy = null;
        }
    }
}
