using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public bool IsMoving { get; private set; }
    Coroutine moveCoroutine;
    Coroutine moveQueueCoroutine;
    Queue<Vector2> movementQueue = new Queue<Vector2>();
    public int PendingMove 
    {get {return movementQueue.Count;}}
    public void TranslateObject (Vector2 movement)
    {
        moveCoroutine = StartCoroutine(translateObject(movement));
    }
    IEnumerator translateObject(Vector2 movement)
    {
        Vector2 TargetPos = (Vector2)transform.position + movement;
        IsMoving = true;
        while (Vector2.Distance((Vector2)transform.position, TargetPos) > 0.1)
        {
            transform.Translate(movement * Time.deltaTime);
            yield return null;
        }
        IsMoving = false;
    }
    public void StopMovement ()
    {
        StopCoroutine(moveQueueCoroutine);
        StopCoroutine(moveCoroutine);
        movementQueue.Clear();
        IsMoving = false;
    }
    public void addMove(Vector2 mvmt)
    {
        movementQueue.Enqueue(mvmt);
        moveQueueCoroutine = StartCoroutine(runMovementQueue());
    }
    IEnumerator runMovementQueue()
    {
        while (movementQueue.Count > 0)
        {
            if(!IsMoving)
                moveCoroutine = (StartCoroutine(translateObject(movementQueue.Dequeue())));
            yield return moveCoroutine;
        }
    }
}