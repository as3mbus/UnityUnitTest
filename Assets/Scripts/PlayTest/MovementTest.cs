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
        const string clonePostFix = "(Clone)";
        [SetUp]
        public void setup()
        {
            testObj = new GameObject();
            testMov = testObj.AddComponent<Mover>();
            testMov.speed = 5;
            testMov.MoveMark = Resources.Load("Prefabs/MoveMark") as GameObject;
        }
        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(testObj);
        }
        [UnityTest]
        public IEnumerator _01_translateObject()
        {
            Vector2 pos = new Vector2(3, 5);
            testMov.OverrideTranslateObject(pos);
            yield return new WaitUntil(() => !testMov.IsMoving);
            Debug.Log(testObj.transform.position);
            Assert.That(Vector2.Distance((Vector2)testObj.transform.position, pos), Is.LessThan(0.1));
        }
        [UnityTest]
        public IEnumerator _02_stopMovement()
        {   
            Vector2 pos = new Vector2(99, 99);
            testMov.OverrideTranslateObject(pos);
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(1.5f);
            testMov.StopMovement();
            Vector2 afterStopPos = testObj.transform.position;
            yield return null;
            Assert.That(afterStopPos, Is.EqualTo((Vector2)testObj.transform.position));
        }
        [UnityTest]
        public IEnumerator _03_01_MovementQueue()
        {
            Vector2 firstMove = new Vector2(5,5);
            testMov.OverrideTranslateObject(firstMove);
            Vector2 seconMove = new Vector2(-9,-10);
            testMov.addMove(seconMove);
            yield return new WaitForFixedUpdate();
            yield return new WaitUntil(()=>testMov.PendingMove == 0);
            Assert.That(Vector2.Distance(testObj.transform.position, firstMove), Is.LessThanOrEqualTo(0.2));
            yield return new WaitUntil(()=> !testMov.IsMoving);
            Assert.That(Vector2.Distance(testObj.transform.position, seconMove), Is.LessThanOrEqualTo(0.1));
        }
        [UnityTest]
        public IEnumerator _03_02_MovementQueueRunningKeepMovingTillQueueEmpty()
        {
            Vector2 firstMove = new Vector2(5,5);
            testMov.OverrideTranslateObject(firstMove);
            Vector2 seconMove = new Vector2(-9,-10);
            Vector2 thirdMove = new Vector2(10,0);
            testMov.addMove(seconMove);
            testMov.addMove(thirdMove);
            int currentPendingMove = testMov.PendingMove;
            yield return new WaitForFixedUpdate();
            yield return new WaitUntil(()=>testMov.PendingMove != currentPendingMove);
            currentPendingMove = testMov.PendingMove;
            Assert.That(Vector2.Distance(testObj.transform.position, firstMove), Is.LessThanOrEqualTo(0.2));
            yield return new WaitUntil(()=>testMov.PendingMove != currentPendingMove);
            Assert.That(Vector2.Distance(testObj.transform.position, seconMove), Is.LessThanOrEqualTo(0.2));
            yield return new WaitUntil(()=> !testMov.IsMoving);
            Assert.That(Vector2.Distance(testObj.transform.position, thirdMove), Is.LessThanOrEqualTo(0.1));
        }
        [UnityTest]
        public IEnumerator _04_stopMovementAlsoStopQueueMovement()
        {   
            Vector2 pos = new Vector2(99, 99);
            Vector2 seconMove = new Vector2(-9,-10);
            testMov.OverrideTranslateObject(pos);
            testMov.addMove(seconMove);
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(3.5f);
            testMov.StopMovement();
            Vector2 afterStopPos = testObj.transform.position;
            yield return null;
            Assert.That(afterStopPos, Is.Not.EqualTo(Vector2.zero));
            Assert.That(afterStopPos, Is.EqualTo((Vector2)testObj.transform.position));
            Assert.That(testMov.PendingMove, Is.EqualTo(0));
        }
        [UnityTest]
        public IEnumerator _05_NewMovementResetMovementAndEmptyMovementQueue()
        {
            Vector2 pos = new Vector2(100, 100);
            Vector2 seconMove = new Vector2(-9,-10);
            Vector2 thirdMove = new Vector2(-3,-7);

            testMov.OverrideTranslateObject(pos);
            testMov.addMove(seconMove);
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(1.5f);
            testMov.OverrideTranslateObject(thirdMove);
            Assert.That(testMov.PendingMove, Is.EqualTo(0));
            yield return new WaitUntil(()=>!testMov.IsMoving);
            Assert.That(Vector2.Distance(testObj.transform.position, thirdMove), Is.LessThan(0.1));
        }
        [Test]
        public void _06_MovementInstantiateMarkPrefab()
        {
            Vector2 pos = new Vector2(10, 10);
            testMov.OverrideTranslateObject(pos);
            Assert.That(GameObject.Find(testMov.MoveMark.name+clonePostFix), Is.Not.Null);
        }
        [UnityTest]
        public IEnumerator _07_MovementCleanUpMarkPrefabAfterDone()
        {
            Vector2 pos = new Vector2(10, 10);
            testMov.OverrideTranslateObject(pos);
            yield return new WaitUntil(() => !testMov.IsMoving);
            yield return new WaitForEndOfFrame();
            Assert.That(GameObject.Find(testMov.MoveMark.name+clonePostFix), Is.Null);
        }
        [UnityTest]
        public IEnumerator _08_StoppingMovementClearAllMarks()
        {
            Vector2 pos = new Vector2(10, 10);
            testMov.OverrideTranslateObject(pos);
            yield return new WaitForEndOfFrame();
            testMov.StopMovement();
            Assert.That(GameObject.Find(testMov.MoveMark.name+clonePostFix), Is.Null);
        }
    }
}
