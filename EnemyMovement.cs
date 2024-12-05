using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    public Rigidbody2D body;
    public Animator anim;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private GameObject player;
    private int health;
    bool notHit;
    public bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        notHit = true;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (notHit)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = playerPosition - transform.position;

            FacePlayer(playerPosition);
            RotateTowardPlayer(direction);
            MoveTowardPlayer(direction); 
        }
    }

    public void FacePlayer(Vector3 playerPosition)
    {
        //Make enemy to look left or right based on position of player
        facingRight = playerPosition.x > transform.position.x;
        anim.SetBool("faceRight", facingRight);
        anim.SetBool("faceLeft", !facingRight);
    }

    public void RotateTowardPlayer(Vector3 direction)
    {
        //Rotate toward Player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (facingRight)
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 180);
    }

    public void MoveTowardPlayer(Vector3 direction)
    {
        //Normalize direction vector and move toward player.
        direction.Normalize();
        direction.x = direction.x / speed;
        direction.y = direction.y / speed;
        body.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hammer"))
        {
            notHit = false;
            health--;
            if (health == 0)
            {
                //Die
                circleCollider.enabled = false;
                if (facingRight) anim.Play("EnemyRight_Dead");
                else anim.Play("EnemyLeft_Dead");
                StartCoroutine(stun());
                Destroy(gameObject, .3f);
            }
            else
            {
                //Knockback
                Vector2 playerPosition = player.transform.position;
                Vector2 knockbackDirection = (body.position - playerPosition).normalized;
                body.AddForce(knockbackDirection * 10f, ForceMode2D.Impulse);
                StartCoroutine(stun());
            }
        }
    }
    private IEnumerator stun()
    {
        yield return new WaitForSeconds(.6f);
        notHit = true;
    }
}
