using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isWalking", GetComponent<PlayerMovement>().isMoving);
    }

    public void TriggerShootAnim()
    {
        anim.SetTrigger("Shoot");
    }

    public void TriggerHurtAnim()
    {
        anim.SetTrigger("Hurt");
    }

    public void TriggerDeathAnim()
    {
        anim.SetTrigger("Death");
    }

    public void TriggerDodgeAnim()
    {
        anim.SetTrigger("Dodge");
    }
}
