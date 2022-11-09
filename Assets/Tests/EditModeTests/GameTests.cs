using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTests
{
    [Test]
    public void BasicTest()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(Vector3.zero, new Vector3(0f,0f,0f));
    }
}
