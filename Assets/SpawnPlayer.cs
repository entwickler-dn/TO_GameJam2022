using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
