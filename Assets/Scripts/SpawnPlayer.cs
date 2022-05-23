using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void Start()
    {
        //Instantiate(player, transform.position, Quaternion.identity);
        //Destroy(gameObject);
    }
}
