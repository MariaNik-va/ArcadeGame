using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileWall : MonoBehaviour
{
    private Animator animator;

    [Header("Sound")]
    [SerializeField] private AudioClip breakSound;

    private bool invulnerability;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void breakDown()
    {
            animator.SetTrigger("isBreakingDown");
            SoundManager.instance.PlaySound(breakSound);
            GetComponent<FragileWall>().enabled = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
