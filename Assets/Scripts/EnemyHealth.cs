using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bodyCol;
    public CapsuleCollider2D capsuleCol;
    CircleCollider2D circleColEffector;
    PointEffector2D pointEffector;

    public float health;
    public float maxHealth;

    public int points;
    public int damage;
    public int weight;

    public float knockedPush;
    public float knockedPushTime;

    public float playerPush;
    public float playerPushTime;

    public float knockedOutTime;
    public float knockedOutTimeCount;
    public bool isGrabbed = false;

    bool canBeDamaged = true;
    public float invulTime;
    float invulTimeCounter;

    [Header("Goat")]
    public bool isGoat = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCol = GetComponent<BoxCollider2D>();
        capsuleCol = GetComponent<CapsuleCollider2D>();
        circleColEffector = GetComponent<CircleCollider2D>();
        pointEffector = GetComponent<PointEffector2D>();
        health = maxHealth;
        knockedOutTimeCount = knockedOutTime;

        if (!isGoat)
            GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (knockedOutTimeCount <= 0 && GetComponent<EnemyAnimationManager>().knockedOut)
        {
            GetUp();
            knockedOutTimeCount = knockedOutTime;
        }

        if (GetComponent<EnemyAnimationManager>().knockedOut && !isGrabbed && !PlayerGrabEnemy.instance.enemyLaunched)
        {
            knockedOutTimeCount -= Time.deltaTime;
        }        

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

    void LoseHealth(float amount, Vector3 bulletDir)
    {
        canBeDamaged = false;
        health -= amount;

        if(health <= 0 && !isRunningCoroutineDeath)
        {
            StartCoroutine(KnockedOut(bulletDir));
            Debug.Log("MURIO");
        }
    }

    bool isRunningCoroutineDeath = false;
    IEnumerator KnockedOut(Vector2 bulletDir)
    {
        GetComponent<EnemyAnimationManager>().knockedOut = true;
        isRunningCoroutineDeath = true;
        bulletDir.Normalize();
        GetComponent<AIPath>().enabled = false;
        bodyCol.isTrigger = false;
        circleColEffector.enabled = false;
        pointEffector.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        rb.AddForce(bulletDir * knockedPush, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockedPushTime);
        rb.velocity = Vector3.zero;
        bodyCol.isTrigger = true;
        capsuleCol.enabled = false;
        gameObject.transform.tag = "EnemyKnockedOut";
        

        //if (FindObjectOfType<SpawnManager>() != null)
        //    SpawnManager.instance.RemoveEnemyFromCounter();

        //Destroy(rb);
        //Destroy(gameObject, 1f);
        //Destroy(GetComponent<EnemyRotation>());
        ////Destroy(GetComponent<EnemyShoot>());
        //Destroy(this);
        GetComponent<EnemyRotation>().enabled = false;
        isRunningCoroutineDeath = false;
    }

    public void GetUp()
    {
        GetComponent<EnemyAnimationManager>().knockedOut = false;

        capsuleCol.enabled = true;
        circleColEffector.enabled = true;
        pointEffector.enabled = true;
        health = (int)maxHealth/2;
        GetComponent<AIPath>().enabled = true;
        GetComponent<EnemyRotation>().enabled = true;
        gameObject.transform.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemies");
    }

    IEnumerator PushFromPlayer(Vector3 playerPos, Vector3 enemyPos)
    {
        Vector3 pushDir = enemyPos - playerPos;
        pushDir.Normalize();
        GetComponent<AIPath>().enabled = false;
        bodyCol.isTrigger = false;
        rb.AddForce(pushDir * playerPush, ForceMode2D.Impulse);
        yield return new WaitForSeconds(playerPushTime);
        bodyCol.isTrigger = true;
        rb.velocity = Vector3.zero;
        GetComponent<AIPath>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(canBeDamaged)
        {
            if (col.gameObject.CompareTag("PlayerBulletSmall") && health > 0)
            {
                LoseHealth(1f, col.GetComponent<Rigidbody2D>().velocity);
                Destroy(col.gameObject);
            }
            else if (col.gameObject.CompareTag("PlayerBulletBig") && health > 0)
            {
                LoseHealth(3f, col.GetComponent<Rigidbody2D>().velocity);
                Destroy(col.gameObject);
            }
        }

        if (col.gameObject.CompareTag("Player"))
        {
            if(!GetComponent<EnemyAnimationManager>().knockedOut)
                StartCoroutine(PushFromPlayer(col.transform.position, transform.position));
        }
    }
}
