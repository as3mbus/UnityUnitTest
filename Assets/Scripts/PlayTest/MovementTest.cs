using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovementTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        [UnityTest]
        public IEnumerator translateObject()
        {
            GameObject testObj = new GameObject();
            Mover testMov = testObj.AddComponent<Mover>();
            Vector2 pos = new Vector2(3, 5);
            testMov.TranslateObject(pos);
            yield return new WaitUntil(() => !testMov.IsMoving);
            Assert.That(Vector2.Distance((Vector2)testObj.transform.position, pos), Is.LessThan(0.1));
        }
        [UnityTest]
        public IEnumerator stopMovement()
        {
            GameObject testObj = new GameObject();
            Mover testMov = testObj.AddComponent<Mover>();
            Vector2 pos = new Vector2(99, 99);
            testMov.TranslateObject(pos);
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(3.5f);
            testMov.StopMovement();
            Vector2 afterStopPos = testObj.transform.position;
            yield return null;
            Assert.That(afterStopPos, Is.Not.EqualTo(Vector2.zero));
            Assert.That(afterStopPos, Is.EqualTo((Vector2)testObj.transform.position));
        }
    }
}
