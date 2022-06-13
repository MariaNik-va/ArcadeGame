using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    [Header ("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Player ")]
    [SerializeField] private LayerMask playerLayer;
    private Health playerHealth;

    [Header("Sound")]
    [SerializeField] private AudioClip attackSound;

    private float cooldownTimer = Mathf.Infinity;
    private Animator animator;

    [SerializeField] private LayerMask BallLayer;

    private EnemyPatrol enemyPatrol;
    private Rigidbody2D enemyBody;


    private void Awake()
    {
        // 
        animator = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            enemyPatrol.enabled = false;
            if (cooldownTimer > attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                animator.SetTrigger("isAttacking");
                SoundManager.instance.PlaySound(attackSound);
            }
        } 
        else
            enemyPatrol.enabled = true;

        if (BallInSight())
        {
            if (isGrounded())
                Jump();
        }

      /*  if (enemyPatrol != null)
        {
            if (PlayerInSight() || BallInSight())
                enemyPatrol.enabled = false;
            else
                enemyPatrol.enabled = true;
        }
        */

    }

    private bool isGrounded()
    {
        RaycastHit2D raycasHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycasHit.collider != null;
    }

    private bool PlayerInSight()
    {
        //подкорректировать код
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private bool BallInSight()
    {
        //подкорректировать код
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * 3 * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, BallLayer);

        return hit.collider != null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.takeDamage(damage);
        }
    }

    private void Jump()
    {
        enemyBody.velocity = new Vector2(enemyBody.velocity.x, 15);
    }
}
