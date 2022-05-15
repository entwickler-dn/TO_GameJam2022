using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabEnemy : MonoBehaviour
{
    public static PlayerGrabEnemy instance;
    float distanceToNearestKnockedEnemy;
    public float distanceToGrab;
    public GameObject grabbedEnemy;
    public Transform grabbedEnemyParent;
    public bool enemyLaunched;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("EnemyKnockedOut") != null)
            distanceToNearestKnockedEnemy = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);

        if(distanceToNearestKnockedEnemy < distanceToGrab && grabbedEnemy == null)
        {
            if(Input.GetMouseButtonDown(1))
            {
                grabbedEnemy = FindClosestEnemy();
                grabbedEnemy.GetComponent<EnemyHealth>().isGrabbed = true;
                grabbedEnemy.transform.parent = grabbedEnemyParent;
            }
        }

        if (grabbedEnemy != null && grabbedEnemy.GetComponent<EnemyHealth>().isGrabbed)
        {
            grabbedEnemy.transform.localPosition = Vector3.zero;
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("EnemyKnockedOut");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
