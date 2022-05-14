using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    //[SerializeField] GameObject headObject, armObject, weaponObject;
    [SerializeField] GameObject bodyObject, armObject, weaponObject, weaponParentObject, dodgeIcon;

    // Movement
    [SerializeField] float moveX, moveY;
    float speed;
    [SerializeField] float speedBase;
    [SerializeField] float linearDrag;
    Vector2 movementDirection;

    public bool canMove = true;
    public bool canAim = true;
    public bool isMoving => (Input.GetButton("Horizontal") || Input.GetButton("Vertical") && canMove);

    // Rotation
    [SerializeField] Transform rotationPoint;
    Vector3 mousePosition;
    [SerializeField] Transform targetPlayer;
    Vector3 objectPosition;
    bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MovementUpdate();
        Movement();
        LootAtMouse();
        FlipCharacter();

        anim.SetBool("isWalking", isMoving);
    }

    void MovementUpdate()
    {
        if(canMove)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }
        else
        {
            moveX = 0;
            moveY = 0;
        }
    }

    void Movement()
    {
        if (moveX != 0 && moveY != 0)
        {
            speed = speedBase / Mathf.Sqrt(2);
        }
        else
        {
            speed = speedBase;
        }

        movementDirection = new Vector2(moveX, moveY);
        rb.velocity = movementDirection * speed;

    }

    void LootAtMouse()
    {
        if(canAim)
        {
            mousePosition = Input.mousePosition;
            objectPosition = Camera.main.WorldToScreenPoint(targetPlayer.position);
            mousePosition.x -= objectPosition.x;
            mousePosition.y -= objectPosition.y;

            float rotationAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            rotationPoint.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }

    void FlipCharacter()
    {
        if (mousePosition.x < 0 && facingRight) // Está mirando a la izquierda...
        {
            facingRight = !facingRight;
            weaponParentObject.transform.Rotate(180, 0, 0);
            bodyObject.GetComponent<SpriteRenderer>().flipX = true;
            //weaponObject.GetComponent<SpriteRenderer>().flipY = true;
            //armObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        if (mousePosition.x > 0 && !facingRight) // Está mirando a la derecha...
        {
            facingRight = !facingRight;
            weaponParentObject.transform.Rotate(180, 0, 0);
            bodyObject.GetComponent<SpriteRenderer>().flipX = false;
            //weaponObject.GetComponent<SpriteRenderer>().flipY = false;
            //armObject.GetComponent<SpriteRenderer>().flipY = false;
        }
    }
}
