using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform bodySprite;
    //[SerializeField] Transform weaponPivot;
    Vector2 direction;
    float dotResult;
    bool facingRight => (dotResult > 0);

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //direction = (player.position - transform.position).normalized;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rotationPoint.rotation = Quaternion.Euler(Vector3.forward * (angle));

        FlipCharacter();
    }

    void FlipCharacter()
    {
        //dotResult = Vector3.Dot(transform.right, direction);
        //if (facingRight)
        //{
        //    // El player está a la derecha.
        //    //facingRight = !facingRight;
        //    //GetComponent<EnemyType>().headSprite.flipX = false;
        //    //GetComponent<EnemyType>().weaponSprite.flipY = false;
        //    //GetComponent<EnemyType>().armSprite.flipY = false;
        //    transform.Rotate(180, 0, 0);
        //}
        //if (!facingRight)
        //{
        //    // El player está en la izquierda.
        //    //facingRight = !facingRight;
        //    //GetComponent<EnemyType>().headSprite.flipX = true;
        //    //GetComponent<EnemyType>().weaponSprite.flipY = true;
        //    //GetComponent<EnemyType>().armSprite.flipY = true;
        //    transform.Rotate(180, 0, 0);
        //}

        Vector3 playerDirectionLocal = transform.InverseTransformPoint(player.position);

        if (playerDirectionLocal.x < 0)
        {
            // El player está en la izquierda.
            //facingRight = !facingRight;
            //GetComponent<EnemyType>().headSprite.flipX = true;
            //GetComponent<EnemyType>().weaponSprite.flipY = true;
            bodySprite.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //transform.Rotate(180, 0, 0);
        }
        else if (playerDirectionLocal.x > 0)
        {
            // El player está a la derecha.
            //facingRight = !facingRight;
            //GetComponent<EnemyType>().headSprite.flipX = false;
            //GetComponent<EnemyType>().weaponSprite.flipY = false;
            bodySprite.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //transform.Rotate(180, 0, 0);
        }
    }
}
