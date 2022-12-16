using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    GameObject Boss;
    Animator animator;
    Collider2D collider2d;

    Vector3 dir;

    public float Speed;

    public float Damge;

    public float LifeTime;

    void Start()
    {
        Boss = GameObject.Find("GoblinKing");
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();

        dir = transform.localScale;

        Speed = 15;

        Damge = 8f;

        LifeTime = 4f;
    }

    void Update()
    {
        Move();
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0 || Boss.GetComponent<GoblinKing>().isDead)
        {
            Destroy(gameObject);
        }
    }
    public void Move()
    {
        if (Boss.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(dir.x, dir.y, dir.z);
            transform.position += Speed * -transform.right * Time.deltaTime;
        }
        else if (Boss.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-dir.x, dir.y, dir.z);
            transform.position += Speed * transform.right * Time.deltaTime;
        }
    }
    public void CloseCollider()
    {
        collider2d.enabled = false;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.Play("Hit");
            if (transform.position.x < collision.transform.position.x)
            {
                collision.GetComponent<PlayerController>().BeHit(Vector2.right, Damge);
            }
            else if (transform.position.x >= collision.transform.position.x)
            {
                collision.GetComponent<PlayerController>().BeHit(Vector2.left, Damge);
            }

        }
        else if (collision.CompareTag("Ground"))
        {
            animator.Play("Hit");
        }
    }
}
