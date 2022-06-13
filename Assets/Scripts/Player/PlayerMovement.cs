using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D playerBody;
    private Animator animator;
    private BoxCollider2D boxCollider;
    //private float onWallDirection;
    private float horizontalInput;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpSound;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //поворот влево и вправо
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //установка параметров анимации
        animator.SetBool("isWalking", horizontalInput != 0);
        animator.SetBool("isGrounded", isGrounded());

        //изменение скорости игрока + проскальзывание по стене
        if (isOnWall())
            playerBody.velocity = new Vector2(0, -jumpSpeed / 3);
        else
            playerBody.velocity = new Vector2(horizontalInput * speed, playerBody.velocity.y);

        //вернуть логику прыжка по стенам + доавить анимацию висени€ на стене
        /*if (isOnWall() && !isGrounded())
        {
            playerBody.gravityScale = 0;
            playerBody.velocity = Vector2.zero;

        }
        else
        {
            playerBody.gravityScale = 2.5f;
            
        }
        */

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
            SoundManager.instance.PlaySound(jumpSound);
        }

        if (Input.GetMouseButton(0))
        {
            Recoil();
        }
        
    }

    private void Jump()
    {
        //вернуть цепл€нее и прыжок со стены 
        /*if  (isGrounded())
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpSpeed);
            animator.SetTrigger("isJumping");
        } 
        else if (isOnWall() && onWallDirection != 0)
        {
            if (horizontalInput == 0)
            {
                playerBody.velocity = new Vector2(-transform.localScale.x * 3, 7);
            } else
            playerBody.velocity = new Vector2(-onWallDirection * 5, 7);
        }*/

        playerBody.velocity = new Vector2(playerBody.velocity.x, jumpSpeed);
        animator.SetTrigger("isJumping");
    }

    public void Recoil()
    {
        playerBody.velocity = new Vector2(jumpSpeed*(-0.25f) * transform.localScale.x, playerBody.velocity.y);
    }


    //помен€ть названи€
    private bool isGrounded()
    {
        RaycastHit2D raycasHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0, Vector2.down, 0.1f, groundLayer);
        return raycasHit.collider != null;
    }

    private bool isOnWall()
    {
        RaycastHit2D raycasHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.2f, wallLayer);
        return raycasHit.collider != null;
    }

    public bool canAttack()
    {
        return !isOnWall();
    }
}
