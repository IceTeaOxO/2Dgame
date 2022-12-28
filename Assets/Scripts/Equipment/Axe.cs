using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public bool canRotate = false;
    public float RotSpeed;
    public float Speed;
    Rigidbody2D rb;
    PlayerCharacter Player;

    Vector3 dir;
    void Start()
    {
        RotSpeed = -500;
        Speed = 10;

        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player").GetComponent<PlayerCharacter>();

        dir = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        if (canRotate)
        {
            transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        if (Player.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-dir.x, dir.y, dir.z);
            transform.position += Speed * -transform.right * Time.deltaTime;
        }
        else if (Player.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(dir.x, dir.y, dir.z);
            transform.position += Speed * transform.right * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.GetComponent<GoblinKing>().BeHit(Player.LightAttackDamge);
        }
    }
}
