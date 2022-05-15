using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_Script : MonoBehaviour
{
    void Start()
    {
        Invoke("FollowPlayer", 0.2f);
        
    }
    
    void FollowPlayer()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
