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
    public int health = 5;
    public bool OnFloor { get; set; }
    bool dDown = false, aDown = false;
    bool notHit;

    // Start is called before the first frame update
    private void Awake()
    {
        //Grab references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        OnFloor = true;
        notHit = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (notHit)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (!dDown) aDown = Input.GetKey(KeyCode.A);
            if (!aDown) dDown = Input.GetKey(KeyCode.D);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (Input.GetKey(KeyCode.W) && OnFloor)
                Jump();

            //set animator params
            anim.SetBool("CursorRight", (mousePosition.x > body.position.x));
            anim.SetBool("runRight", dDown);
            anim.SetBool("runBackRight", aDown);
            anim.SetBool("runLeft", aDown);
            anim.SetBool("runBackLeft", dDown);
        }

    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump_speed);
        OnFloor = false;
    }
    private IEnumerator JumpBack(float x)
    {
        body.velocity = new Vector2(x, 2f);
        yield return new WaitForSeconds(.6f);
        notHit = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            OnFloor = true;
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            notHit = false;
            health--;
            if (collision.transform.position.x > body.position.x)
                StartCoroutine(JumpBack(-1.5f));
            else StartCoroutine(JumpBack(1.5f));
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            OnFloor = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            OnFloor = false;
    }
}
