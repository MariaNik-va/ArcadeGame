using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] balls;
    private float cooldownTimer;

    [Header("Sound")]
    [SerializeField] private AudioClip ballSound;
    private void Attack()
    {
        cooldownTimer = 0;

        SoundManager.instance.PlaySound(ballSound);
        balls[FindBall()].transform.position = firePoint.position;
        balls[FindBall()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindBall()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (!balls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
