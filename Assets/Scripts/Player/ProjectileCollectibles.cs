using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollectibles : MonoBehaviour
{
    [SerializeField] private int projectileValue;

    [Header("Sound")]
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(collectSound);
            collision.GetComponent<PlayerAttack>().AddProjectile(projectileValue);
            gameObject.SetActive(false);
        }
    }
}
