using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [Header("Patrol points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool isMovingLeft;
    [SerializeField] private float waitingDuration;
    private float waitingTimer;

    [Header("Enemy animator")]
    [SerializeField] private Animator animator;



    private void Awake()
    {
        initScale = enemy.localScale;

    }

    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
    }

    private void Update()
    {
        if (isMovingLeft)
        {
            if (enemy.position.x > leftEdge.position.x)
                MoveInDeriction(-1);
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x < rightEdge.position.x)
                MoveInDeriction(1);
            else
            {
                ChangeDirection();
            }
        }

    }

    private void ChangeDirection()
    {
        animator.SetBool("isWalking", false);
        waitingTimer += Time.deltaTime;

        if (waitingTimer > waitingDuration)
            isMovingLeft = !isMovingLeft;

    }
    private void MoveInDeriction(int _direction)
    {
        waitingTimer = 0;
        animator.SetBool("isWalking", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}

