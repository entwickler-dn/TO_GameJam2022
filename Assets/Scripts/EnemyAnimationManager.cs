using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    Animator anim;
    //public bool isWalking;
    public bool knockedOut;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //anim.SetBool("isWalking", isWalking);
        anim.SetBool("knockedOut", knockedOut);
    }

    public void DeathAnim()
    {
        anim.SetTrigger("Death");
    }
}
