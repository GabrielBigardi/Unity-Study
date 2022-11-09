using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator MoveNorth()
    {
        var gameObject = new GameObject();
        var playerMovement = gameObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(gameObject.transform, gameObject.transform.position + Vector3.up, 0.1f);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(gameObject.transform.position, Vector3.up);
    }
    
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator MoveLeft()
    {
        var gameObject = new GameObject();
        var playerMovement = gameObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(gameObject.transform, gameObject.transform.position + Vector3.left, 0.1f);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(gameObject.transform.position, Vector3.left);
    }
    
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator MoveRight()
    {
        var gameObject = new GameObject();
        var playerMovement = gameObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(gameObject.transform, gameObject.transform.position + Vector3.right, 0.1f);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(gameObject.transform.position, Vector3.right);
    }
    
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator MoveSouth()
    {
        var gameObject = new GameObject();
        var playerMovement = gameObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(gameObject.transform, gameObject.transform.position + Vector3.down, 0.1f);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(gameObject.transform.position, Vector3.down);
    }
}