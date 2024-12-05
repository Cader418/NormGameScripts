using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerMovementRightTest()
    {
        GameObject playerObject = new GameObject();
        PlayerMovement player = playerObject.AddComponent<PlayerMovement>();
        Rigidbody2D body = playerObject.AddComponent<Rigidbody2D>();
        player.body = body;
        player.speed = 5;
        body.velocity = Vector3.zero;
        float horizontalMovement = 1;

        player.HandleMovement(horizontalMovement);

        Assert.AreEqual(5, player.body.velocity.x);
    }
    [Test]
    public void PlayerMovementLeftTest()
    {
        GameObject playerObject = new GameObject();
        PlayerMovement player = playerObject.AddComponent<PlayerMovement>();
        Rigidbody2D body = playerObject.AddComponent<Rigidbody2D>();
        player.body = body;
        player.speed = 5;
        body.velocity = Vector3.zero;
        float horizontalMovement = -1;

        player.HandleMovement(horizontalMovement);

        Assert.AreEqual(-5, player.body.velocity.x);
    }
}
