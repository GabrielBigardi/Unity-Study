using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests
{
    public class PlayerMovementTests
    {
        [UnityTest]
        [RequiresPlayMode]
        [TestCase(0f, 1f, ExpectedResult = (IEnumerator)null)]
        [TestCase(0f, -1f, ExpectedResult = (IEnumerator)null)]
        [TestCase(1f, 0f, ExpectedResult = (IEnumerator)null)]
        [TestCase(-1f, 0f, ExpectedResult = (IEnumerator)null)]
        public IEnumerator Move_ChangePosition(float x, float y)
        {
            var playerObject = new GameObject();
            var playerMovement = playerObject.AddComponent<PlayerMovement>();
            playerMovement.IsTestObject = true;
            playerMovement.Move(playerObject.transform, playerObject.transform.position + new Vector3(x,y,0f), 0f);
            yield return null;
            Assert.AreEqual(playerObject.transform.position, new Vector3(x,y,0f));
        }
    }
}