using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public bool IsMoving { get; private set; }
    Coroutine moveCoroutine;
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
        StopCoroutine(moveCoroutine);
        IsMoving = false;
    }
}