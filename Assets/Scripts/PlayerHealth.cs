using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool canBeDamaged = true;
    bool isDeath => (health <= 0);
    public float invulTime;
    float invulTimeCounter;
    //public GameObject[] healthSprites;

    void Start()
    {
        //health = healthSprites.Length;
        invulTimeCounter = invulTime;
    }

    void Update()
    {
        if(GetComponent<PlayerMovement>() != null)
        {
            if (!canBeDamaged)
            {
                invulTimeCounter -= Time.deltaTime;
                if (invulTimeCounter <= 0)
                {
                    canBeDamaged = true;
                    invulTimeCounter = invulTime;
                }
            }
        }
    }

    public void LoseHealth(int amount)
    {
        canBeDamaged = false;
        Debug.Log("aoñjsndasd");
        //GetComponent<PlayerAnimator>().TriggerHurtAnim();
        health -= amount;
        //healthSprites[health].SetActive(false);
        if (health <= 0)
        {
            //GetComponent<PlayerAnimator>().TriggerDeathAnim();
            Death();
        }
    }

    public void Death()
    {
        //GetComponent<PlayerMovement>().canMove = false;
        //GetComponent<PlayerMovement>().canAim = false;
        Destroy(GetComponent<PlayerMovement>());
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(GetComponent<PlayerShoot>());
        //Destroy(GetComponent<PlayerRunAndDodgeStamina>());
        Destroy(GetComponent<PlayerAnimator>());
        Debug.Log("Moriste, puto");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!isDeath)
        {
            if (col.gameObject.CompareTag("Enemy") && canBeDamaged)
            {
                LoseHealth(col.gameObject.GetComponent<EnemyHealth>().damage);
            }
        }
    }
}
