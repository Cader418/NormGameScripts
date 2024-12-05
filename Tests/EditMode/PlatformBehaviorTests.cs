using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class PlatformBehaviorTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void AbovePlatformTest()
    {
        GameObject platformObject = new GameObject();
        PlatformBehavior platform = platformObject.AddComponent<PlatformBehavior>();
        Rigidbody2D player = platformObject.AddComponent<Rigidbody2D>();
        TilemapCollider2D platformCollider = platformObject.AddComponent<TilemapCollider2D>();
        platform.player_half_height = 0.235;
        platform.platform_pos = 1;
        platform.player = player;
        platform.platformCollider = platformCollider;
        player.transform.position = new Vector3(0, 1.5f, 0);

        platform.HandlePlatform();

        Assert.AreEqual("Floor", platformObject.tag);
    }
    [Test]
    public void BelowPlatformTest()
    {
        GameObject platformObject = new GameObject();
        PlatformBehavior platform = platformObject.AddComponent<PlatformBehavior>();
        Rigidbody2D player = platformObject.AddComponent<Rigidbody2D>();
        TilemapCollider2D platformCollider = platformObject.AddComponent<TilemapCollider2D>();
        platform.player_half_height = 0.235;
        platform.platform_pos = 1;
        platform.player = player;
        platform.platformCollider = platformCollider;
        player.transform.position = new Vector3(0, 0.5f, 0);

        platform.HandlePlatform();

        Assert.AreEqual("Untagged", platformObject.tag);
    }
}
