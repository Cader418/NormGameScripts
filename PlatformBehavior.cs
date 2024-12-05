using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformBehavior : MonoBehaviour
{
    public TilemapCollider2D platformCollider;
    private CompositeCollider2D platform;
    private PlayerMovement playerScript;
    public Rigidbody2D player;
    public float platform_pos;
    public double player_half_height;
    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<TilemapCollider2D>();
        platform = GetComponent<CompositeCollider2D>();
        platform_pos = platform.bounds.center.y;
        platformCollider.enabled = false;
        player_half_height = 0.235;
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlatform(); 
    }
    public void HandlePlatform()
    {
        // Player is above the platform
        if (player.transform.position.y - player_half_height > platform_pos)
        {
            platformCollider.enabled = true;
            tag = "Floor";
        }
        //Player is below the platform or hit the down button
        if (player.transform.position.y < platform_pos || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.S)) playerScript.OnFloor = false;
            tag = "Untagged";
            platformCollider.enabled = false;
        }
    }
}
