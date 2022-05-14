using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform shootPoint;

    public bool canShootBullet = true;
    public float timerBetweenBullets;
    [SerializeField] float timerBetweenBulletsCount;

    public float strayFactor;
    public float bulletSpeed;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timerBetweenBulletsCount = timerBetweenBullets;
    }

    void Update()
    {
        CanShootBulletAgain();
        CheckIfShoot();

    }

    void CheckIfShoot()
    {
        if (!canShootBullet)
        {
            if (Input.GetMouseButton(0))
                timerBetweenBulletsCount -= Time.deltaTime;
            else
                timerBetweenBulletsCount -= Time.deltaTime * 2f;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        //GetComponent<PlayerAnimator>().TriggerShootAnim();

        // Si solo disparas una bala...
        var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        var randomNumberRotate = Random.Range(-strayFactor, strayFactor);
        bullet.transform.Rotate(0, 0, randomNumberRotate);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        Destroy(bullet, 3f);
       
        canShootBullet = false;
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
