using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSimpleEnemy : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Ranged attack")]
    [SerializeField] private Transform ballPoint;
    [SerializeField] private GameObject[] balls;

    [Header("Player")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;
    private Animator animator;
    private EnemyPatrol enemyPatrol;
    
    [Header("Sound")]
    [SerializeField] private AudioClip enemyBallSound;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer > attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("IsRangedAttacking");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        //подкорректировать код
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(enemyBallSound);
        cooldownTimer = 0;
        balls[FindBall()].transform.position = ballPoint.position;
        balls[FindBall()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindBall()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (!balls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
