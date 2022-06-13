using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileBar : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Image totalProjectileBar;
    [SerializeField] private Image currentProjectileBar;

    private void Start()
    {
        totalProjectileBar.fillAmount = ((float) playerAttack.currentNumberOfProjectile) / 10;
    }

    private void Update()
    {
        currentProjectileBar.fillAmount = ((float) playerAttack.currentNumberOfProjectile) / 10;
    }
}
