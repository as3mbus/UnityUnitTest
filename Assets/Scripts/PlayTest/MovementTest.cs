using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovementTest
    {
        GameObject testObj;
        Mover testMov;
        
        [SetUp]
        public void setup()
        {
            testObj = new GameObject();
            testMov = testObj.AddComponent<Mover>();
        }
        [UnityTest]
        public IEnumerator translateObject()
        {
            Vector2 pos = new Vector2(3, 5);
            testMov.TranslateObject(pos);
            yield return new WaitUntil(() => !testMov.IsMoving);
            Assert.That(Vector2.Distance((Vector2)testObj.transform.position, pos), Is.LessThan(0.1));
        }
        [UnityTest]
        public IEnumerator stopMovement()
        {   
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
        [UnityTest]
        public IEnumerator MovementQueue()
        {
            Vector2 firstMove = new Vector2(5,5);
            Vector2 firstMoveGoal = (Vector2) testObj.transform.position + firstMove;
            testMov.TranslateObject(firstMove);
            Vector2 seconMove = new Vector2(-9,-10);
            Vector2 seconMoveGoal = firstMoveGoal + seconMove;
            testMov.addMove(seconMove);
            yield return new WaitForFixedUpdate();
            yield return new WaitUntil(()=>testMov.PendingMove == 0);
            Assert.That(Vector2.Distance(testObj.transform.position, firstMoveGoal), Is.LessThanOrEqualTo(0.3));
            yield return new WaitUntil(()=> !testMov.IsMoving);
            Assert.That(Vector2.Distance(testObj.transform.position, seconMoveGoal), Is.LessThanOrEqualTo(0.1));
        }
    }
}
