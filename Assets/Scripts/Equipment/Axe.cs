using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public bool canRotate = false;
    public float RotSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        canRotate = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.freezeRotation = true;
    }
}
