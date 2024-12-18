using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NormArmBehavior : MonoBehaviour
{
    private Rigidbody2D body;
    public Rigidbody2D parent;
    private Animator anim;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D[] colliders;
    public PolygonCollider2D currentCollider;

    [SerializeField] public Sprite[] rightArm;
    [SerializeField] public Sprite[] leftArm;
    private bool hitTimedOut = true;
    public float angle;
    int hit = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        //Grab references from object
        body = GetComponent<Rigidbody2D>();
        Transform parentTransform = transform.parent;
        parent = parentTransform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponents<PolygonCollider2D>();
        currentCollider = colliders[0];
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleArmAnimation(mousePosition); 
        PointTowardMouse(mousePosition); 
        HandleAttack();
    }

    public void HandleArmAnimation(Vector3 mousePosition)
    {
        //Determine arm animation
        if (mousePosition.x > parent.position.x) //facing right
        {
            currentCollider = colliders[0];
            spriteRenderer.sprite = rightArm[hit];
            spriteRenderer.sortingOrder = 3; //arm goes on top of body
        }
        else //facing left
        {
            currentCollider = colliders[1];
            spriteRenderer.sprite = leftArm[hit];
            spriteRenderer.sortingOrder = 1; //arm goes behind body
        }
    }

    public void PointTowardMouse(Vector3 mousePosition)
    {
        //Point toward mouse
        Vector3 direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (mousePosition.x > parent.position.x) angle -= 45f;
        else angle -= 135f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void HandleAttack()
    {
        //Determine attack
        if (Input.GetMouseButtonDown(0)) //if mouse clicked
        {
            hit = 1; //extend arm
            currentCollider.enabled = true;
            hitTimedOut = false;
            StartCoroutine(allowHitFor(1f)); //wait for 1 sec before retracting arm
        }
        else if (Input.GetMouseButtonUp(0)) //if mouse released
        {
            hitTimedOut = true; //stop waiting to retract arm
            StopAllCoroutines();
        }
        if (hitTimedOut == true)
        {
            currentCollider.enabled = false;
            hit = 0; //retract arm
        }
    }

    private IEnumerator allowHitFor(float duration)
    {
        hitTimedOut = false;
        Debug.Log("Waitng...");
        yield return new WaitForSeconds(duration);
        hitTimedOut = true;
        Debug.Log("Go back. Arm is tired");
    }
}
