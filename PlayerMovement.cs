using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump_speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool onFloor = true;

    // Start is called before the first frame update
    private void Awake()
    {
        //Grab references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.W) && onFloor)
            Jump();

        //set animator params
        anim.SetBool("CursorRight", (mousePosition.x > body.position.x));
        anim.SetBool("runRight", horizontalInput == 1);
        anim.SetBool("runBackRight", horizontalInput == -1);
        anim.SetBool("runLeft", horizontalInput == -1);
        anim.SetBool("runBackLeft", horizontalInput == 1);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump_speed);
        onFloor = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            onFloor = true;
    }
}
