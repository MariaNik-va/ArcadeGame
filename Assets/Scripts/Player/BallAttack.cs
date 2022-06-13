using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 8)
        {
            gameObject.SetActive(false);
        }
    }

    //старнная штука
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "FragileWall")
        {
            hit = true;
            boxCollider.enabled = false;
            animator.SetTrigger("isExploded");
        }
        
        if (collision.tag == "Enemy")
        {
                       //добавитьпеременную силы удара
            collision.GetComponent<Health>().takeDamage(1);
        }
        else
        if (collision.tag == "FragileWall")
        {
            collision.GetComponent<FragileWall>().breakDown(); ;
        }
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        lifeTime = 0;
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
