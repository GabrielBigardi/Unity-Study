using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerHealthTests
{
    [UnityTest]
    [RequiresPlayMode]
    public IEnumerator SetLifes_ChangeCurrentLifes()
    {
        var playerObject = new GameObject();
        var playerHealth = playerObject.AddComponent<PlayerHealth>();
        playerHealth.SetLifes(3);
        yield return null;
        Assert.AreEqual(playerHealth.CurrentLifes, 3);
    }
}
