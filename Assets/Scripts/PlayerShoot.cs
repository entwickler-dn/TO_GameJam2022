using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    [SerializeField] public GameObject bulletPrefabSmall, bulletPrefabBig;
    [SerializeField] public Transform shootPoint;

    public bool canShootBullet = true;
    public float timerBetweenBullets;
    [SerializeField] float timerBetweenBulletsCount;

    public float strayFactor;
    public float bulletSpeed;

    bool chargeAttack = false;
    public float timerToMaxDamage;
    float timerToMaxDamageCount;
    GameObject bullet;

    [Header("Launch Enemy")]
    public GameObject launchedEnemy;
    float launchEnemyForce;
    float holdDownStartTime;

    AudioSource playerAudio;
    AudioClip shootAudio, launchAudio;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timerBetweenBulletsCount = timerBetweenBullets;
        playerAudio = GetComponent<AudioSource>();
        shootAudio = (AudioClip)Resources.Load("shootSound");
        launchAudio = (AudioClip)Resources.Load("throuEnemySound");
    }

    void Update()
    {
        CanShootBulletAgain();
        CheckIfShoot();
        GetComponent<Animator>().SetBool("chargeAttack", chargeAttack);

        if (PlayerGrabEnemy.instance.enemyLaunched && launchedEnemy != null)
        {
            Rigidbody2D enemyRB = launchedEnemy.GetComponent<Rigidbody2D>();
            float step = 5 * Time.deltaTime;
            enemyRB.velocity = Vector2.MoveTowards(enemyRB.velocity, Vector2.zero, step);

            if (enemyRB.velocity == Vector2.zero || !launchedEnemy.GetComponent<EnemyAnimationManager>().knockedOut)
            {
                PlayerGrabEnemy.instance.enemyLaunched = false;
            }
        }
    }

    void CheckIfShoot()
    {
        if (timerToMaxDamageCount >= timerToMaxDamage)
        {
            chargeAttack = true;
        }
        else chargeAttack = false;

        if (!canShootBullet)
        {
            if (Input.GetMouseButton(0))
                timerBetweenBulletsCount -= Time.deltaTime;
            else
                timerBetweenBulletsCount -= Time.deltaTime * 2f;
        }
        else
        {
            if(PlayerGrabEnemy.instance.grabbedEnemy == null)
            {
                if (Input.GetMouseButton(0))
                {
                    timerToMaxDamageCount += Time.deltaTime;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Shoot();
                }
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    holdDownStartTime = Time.time;
                }
                if(Input.GetMouseButtonUp(0))
                {
                    float holdDownTime = Time.time - holdDownStartTime;
                    ShootEnemy(CalculateHoldDownForce(holdDownTime));
                }
            }
        }
    }

    public void Shoot()
    {
        playerAudio.clip = shootAudio;
        playerAudio.Play();
        //GetComponent<PlayerAnimator>().TriggerShootAnim();

        if (PlayerGrabEnemy.instance.grabbedEnemy == null)
        {
            // Si solo disparas una bala...
            if (timerToMaxDamageCount < timerToMaxDamage)
            {
                bullet = Instantiate(bulletPrefabSmall, shootPoint.position, shootPoint.rotation);
            }
            else
            {
                bullet = Instantiate(bulletPrefabBig, shootPoint.position, shootPoint.rotation);
            }
            var randomNumberRotate = Random.Range(-strayFactor, strayFactor);
            bullet.transform.Rotate(0, 0, randomNumberRotate);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
            Destroy(bullet, 3f);

            canShootBullet = false;
            timerToMaxDamageCount = 0;
        }
        //else
        //{
        //    launchedEnemy = PlayerGrabEnemy.instance.grabbedEnemy;
        //    PlayerGrabEnemy.instance.grabbedEnemy = null;
        //    launchedEnemy.transform.parent = null;
        //    launchedEnemy.GetComponent<EnemyHealth>().isGrabbed = false;
        //    launchedEnemy.GetComponent<Rigidbody2D>().velocity = launchedEnemy.transform.right * bulletSpeed;
            
        //}
    }

    float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 1.5f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime/maxForceHoldDownTime);
        float force = holdTimeNormalized * 50;
        return force;
    }

    void ShootEnemy(float force)
    {
        playerAudio.clip = launchAudio;
        playerAudio.Play();

        launchedEnemy = PlayerGrabEnemy.instance.grabbedEnemy;
        PlayerGrabEnemy.instance.grabbedEnemy = null;
        launchedEnemy.transform.parent = null;
        launchedEnemy.GetComponent<EnemyHealth>().isGrabbed = false;
        //launchedEnemy.GetComponent<Rigidbody2D>().velocity = launchedEnemy.transform.right * force;
        launchedEnemy.GetComponent<Rigidbody2D>().AddForce(launchedEnemy.transform.right * force/3, ForceMode2D.Impulse);
        PlayerGrabEnemy.instance.enemyLaunched = true;
        launchedEnemy.GetComponent<EnemyHealth>().capsuleCol.enabled = true;
    }

    void CanShootBulletAgain()
    {
        if (timerBetweenBulletsCount <= 0)
        {
            timerBetweenBulletsCount = timerBetweenBullets;
            canShootBullet = true;
        }
    }
}
