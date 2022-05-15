using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    bool goSlower = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerMovement>() != null)
            anim.SetBool("isWalking", GetComponent<PlayerMovement>().isMoving);

        Debug.Log(Time.timeScale);
    }

    IEnumerator ScaleTime(float start, float end, float time)     //not in Start or Update
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
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
        anim.SetTrigger("isDeath");
        StartCoroutine(ScaleTime(1.0f, 0.0f, 1.5f));
        //goSlower = true;
        
    }

    public void TriggerDodgeAnim()
    {
        anim.SetTrigger("Dodge");
    }
}
