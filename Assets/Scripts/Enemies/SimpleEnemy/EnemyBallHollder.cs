using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallHollder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
