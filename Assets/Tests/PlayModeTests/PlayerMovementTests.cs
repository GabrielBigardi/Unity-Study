using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator Move_ChangePosition_Up()
    {
        var playerObject = new GameObject();
        var playerMovement = playerObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(playerObject.transform, playerObject.transform.position + Vector3.up, 0.01f);
        yield return new WaitForSeconds(0.01f);
        Assert.AreEqual(playerObject.transform.position, Vector3.up);
    }
    
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator Move_ChangePosition_Left()
    {
        var playerObject = new GameObject();
        var playerMovement = playerObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(playerObject.transform, playerObject.transform.position + Vector3.left, 0.01f);
        yield return new WaitForSeconds(0.01f);
        Assert.AreEqual(playerObject.transform.position, Vector3.left);
    }
    
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator Move_ChangePosition_Right()
    {
        var playerObject = new GameObject();
        var playerMovement = playerObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(playerObject.transform, playerObject.transform.position + Vector3.right, 0.01f);
        yield return new WaitForSeconds(0.01f);
        Assert.AreEqual(playerObject.transform.position, Vector3.right);
    }
    
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator Move_ChangePosition_Down()
    {
        var playerObject = new GameObject();
        var playerMovement = playerObject.AddComponent<PlayerMovement>();
        playerMovement.IsTestObject = true;
        playerMovement.Move(playerObject.transform, playerObject.transform.position + Vector3.down, 0.01f);
        yield return new WaitForSeconds(0.01f);
        Assert.AreEqual(playerObject.transform.position, Vector3.down);
    }
}