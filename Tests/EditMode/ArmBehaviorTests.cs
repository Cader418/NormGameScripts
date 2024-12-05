using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class ArmBehaviorTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void FaceArmRightTest()
    {
        GameObject armObject = new GameObject();
        NormArmBehavior arm = armObject.AddComponent<NormArmBehavior>();
        SpriteRenderer spriteRenderer = armObject.AddComponent<SpriteRenderer>();
        PolygonCollider2D collider1 = armObject.AddComponent<PolygonCollider2D>();
        PolygonCollider2D collider2 = armObject.AddComponent<PolygonCollider2D>();
        PolygonCollider2D currentCollider = armObject.AddComponent<PolygonCollider2D>();
        PolygonCollider2D[] colliders = new PolygonCollider2D[] { collider1, collider2 };
        Sprite s1Right = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite s2Right = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite[] rightArm = new Sprite[] { s1Right, s2Right };
        Sprite s1Left = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite s2Left = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite[] leftArm = new Sprite[] { s1Left, s2Left };
        Rigidbody2D parent = armObject.AddComponent<Rigidbody2D>();
        arm.spriteRenderer = spriteRenderer;
        arm.colliders = colliders;
        arm.currentCollider = currentCollider;
        arm.rightArm = rightArm;
        arm.leftArm = leftArm;
        arm.parent = parent;
        arm.parent.position = Vector3.zero;

        arm.HandleArmAnimation(new Vector3(1, 0, 0));

        Assert.AreEqual(3, arm.spriteRenderer.sortingOrder);
    }
    [Test]
    public void FaceArmLeftTest()
    {
        GameObject armObject = new GameObject();
        NormArmBehavior arm = armObject.AddComponent<NormArmBehavior>();
        SpriteRenderer spriteRenderer = armObject.AddComponent<SpriteRenderer>();
        PolygonCollider2D collider1 = armObject.AddComponent<PolygonCollider2D>();
        PolygonCollider2D collider2 = armObject.AddComponent<PolygonCollider2D>();
        PolygonCollider2D currentCollider = armObject.AddComponent<PolygonCollider2D>();
        PolygonCollider2D[] colliders = new PolygonCollider2D[] { collider1, collider2 };
        Sprite s1Right = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite s2Right = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite[] rightArm = new Sprite[] { s1Right, s2Right };
        Sprite s1Left = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite s2Left = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Norm/NormArm.png");
        Sprite[] leftArm = new Sprite[] { s1Left, s2Left };
        Rigidbody2D parent = armObject.AddComponent<Rigidbody2D>();
        arm.spriteRenderer = spriteRenderer;
        arm.colliders = colliders;
        arm.currentCollider = currentCollider;
        arm.rightArm = rightArm;
        arm.leftArm = leftArm;
        arm.parent = parent;
        arm.parent.position = Vector3.zero;

        arm.HandleArmAnimation(new Vector3(-1, 0, 0));

        Assert.AreEqual(1, arm.spriteRenderer.sortingOrder);
    }
    [Test]
    public void PointTowardMouseRightTest()
    {
        GameObject armObject = new GameObject();
        NormArmBehavior arm = armObject.AddComponent<NormArmBehavior>();
        Rigidbody2D parent = armObject.AddComponent<Rigidbody2D>();
        armObject.transform.position = new Vector3(5,5,5);
        arm.parent = parent;
        arm.parent.position = Vector3.zero;
        arm.angle = 0;
        arm.PointTowardMouse(new Vector3(10, 5, 1));

        Assert.AreEqual(-45f, arm.angle);
    }
    [Test]
    public void PointTowardMouseLeftTest()
    {
        GameObject armObject = new GameObject();
        NormArmBehavior arm = armObject.AddComponent<NormArmBehavior>();
        Rigidbody2D parent = armObject.AddComponent<Rigidbody2D>();
        armObject.transform.position = new Vector3(10, 5, 1);
        arm.parent = parent;
        arm.parent.position = Vector3.zero;
        arm.angle = 0;
        arm.PointTowardMouse(new Vector3(5, 5, 5));

        Assert.AreEqual(135f, arm.angle);
    }
}
