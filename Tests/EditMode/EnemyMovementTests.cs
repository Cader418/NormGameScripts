using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class EnemyMovementTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void FacePlayerRightTest()
    {
        GameObject enemyObject = new GameObject();
        EnemyMovement enemy = enemyObject.AddComponent<EnemyMovement>();
        Animator anim = enemyObject.AddComponent<Animator>();
        enemyObject.transform.position = new Vector3(0, 0, 0);
        enemy.anim = anim;

        enemy.FacePlayer(new Vector3(1, 0, 0));

        Assert.AreEqual(true, enemy.facingRight);
    }
    [Test]
    public void FacePlayerLeftTest()
    {
        GameObject enemyObject = new GameObject();
        EnemyMovement enemy = enemyObject.AddComponent<EnemyMovement>();
        Animator anim = enemyObject.AddComponent<Animator>();
        enemyObject.transform.position = new Vector3(1, 0, 0);
        enemy.anim = anim;

        enemy.FacePlayer(new Vector3(0, 0, 0));

        Assert.AreEqual(false, enemy.facingRight);
    }
    [Test]
    public void MoveTowardPlayerTest()
    {
        GameObject enemyObject = new GameObject();
        EnemyMovement enemy = enemyObject.AddComponent<EnemyMovement>();
        Rigidbody2D body = enemyObject.AddComponent<Rigidbody2D>();
        body.velocity = Vector3.zero;
        enemy.body = body;
        enemy.speed = 3;

        enemy.MoveTowardPlayer(new Vector3(10, 5, 0));

        Vector2 expected = new Vector2(0.30f, 0.15f);
        Assert.AreEqual(expected.ToString(), enemy.body.velocity.ToString());
    }
}
