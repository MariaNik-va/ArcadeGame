using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform ballPoint;
    [SerializeField] private GameObject[] balls;
    [SerializeField] private AudioClip ballSound;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float attackCooldownTimer = Mathf.Infinity;
    public int currentNumberOfProjectile { get; private set; }
    [SerializeField] private int startingNumberOfProjectile;

    private void Awake()
    {
        currentNumberOfProjectile = startingNumberOfProjectile;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
 
        if (Input.GetMouseButton(0) && playerMovement.canAttack() && attackCooldownTimer > attackCooldown)
        {
            Attack();
        }
            

        attackCooldownTimer += Time.deltaTime;
    }

    

    private void Attack()
    {
        attackCooldownTimer = 0;
                
        if (currentNumberOfProjectile >= 1)
        {
            SoundManager.instance.PlaySound(ballSound);
            animator.SetTrigger("isAttacking");
            currentNumberOfProjectile -= 1;
            balls[FindBall()].transform.position = ballPoint.position;
            balls[FindBall()].GetComponent<BallAttack>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
        
        //pool
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

    public void AddProjectile(int _value)
    {
        if (currentNumberOfProjectile < startingNumberOfProjectile)
            currentNumberOfProjectile += _value;
    }
}
