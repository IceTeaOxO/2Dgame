using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movePower = 10f;
    public float KickBoardMovePower = 15f;
    public float jumpPower = 20f;

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;
    private bool isKickboard = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Restart();
        if (alive)
        {
            //Hurt();
            //Die();
            //Attack();
            Jump();
            Run();

        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    anim.SetBool("isJump", false);
    //}

    void Run()
    {
        movement = Vector3.zero;
        //anim.SetBool("isRun", false);

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            movement = Vector3.left;

            transform.localScale = new Vector3(direction, 1, 1);
            //if (!anim.GetBool("isJump"))
                //anim.SetBool("isRun", true);

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            movement = Vector3.right;

            transform.localScale = new Vector3(direction, 1, 1);
            //if (!anim.GetBool("isJump"))
                //anim.SetBool("isRun", true);

        }
        transform.position += movement * movePower * Time.deltaTime;


    }
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") )
        )
        {
            isJumping = true;
            //anim.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    public Vector3 GetFacingDirection()
    {
        if (direction == 1)
        {
            Vector2 dir = movement;
            return dir;
        } else
        {
            Vector2 dir = movement;
            return dir;
        }
    }
    //void Attack()
    //{
    //    if (Input.GetKeyDown(KeyCode.J))
    //    {
    //        anim.SetTrigger("attack");
    //    }
    //}
    //void Hurt()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        anim.SetTrigger("hurt");
    //        if (direction == 1)
    //            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
    //        else
    //            rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
    //    }
    //}
    //void Die()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        isKickboard = false;
    //        anim.SetBool("isKickBoard", false);
    //        anim.SetTrigger("die");
    //        alive = false;
    //    }
    //}
    //void Restart()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha0))
    //    {
    //        isKickboard = false;
    //        anim.SetBool("isKickBoard", false);
    //        anim.SetTrigger("idle");
    //        alive = true;
    //    }
    //}
}
