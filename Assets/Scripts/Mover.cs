using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public bool IsMoving { get; private set; }
    public GameObject MoveMark;
    Coroutine moveCoroutine;
    Coroutine moveQueueCoroutine;
    Queue<Vector2> movementQueue = new Queue<Vector2>();
    public int PendingMove
    { get { return movementQueue.Count; } }

    void TranslateObject(Vector2 movement)
    {
        moveCoroutine = StartCoroutine(translateObject(movement));
    }
    public void OverrideTranslateObject(Vector2 movement)
    {
        StopMovement();
        TranslateObject(movement);
    }
    IEnumerator translateObject(Vector2 movement)
    {
        IsMoving = true;
        GameObject mark = Instantiate(MoveMark, movement, Quaternion.identity);
        while (Vector2.Distance((Vector2)transform.position, movement) > 0.1)
        {
            transform.position = Vector2.MoveTowards(transform.position, movement, Time.deltaTime * speed);
            yield return null;
        }
        IsMoving = false;
    }
    public void StopMovement()
    {
        if (PendingMove > 0)
            StopCoroutine(moveQueueCoroutine);
        if (IsMoving)
            StopCoroutine(moveCoroutine);
        movementQueue.Clear();
        IsMoving = false;
    }
    public void addMove(Vector2 mvmt)
    {
        movementQueue.Enqueue(mvmt);
        if (PendingMove == 1)
            moveQueueCoroutine = StartCoroutine(runMovementQueue());
    }
    IEnumerator runMovementQueue()
    {
        while (movementQueue.Count > 0)
        {
            if (!IsMoving)
                moveCoroutine = (StartCoroutine(translateObject(movementQueue.Dequeue())));
            yield return moveCoroutine;
        }
    }
}