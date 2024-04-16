using System;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class Character: InjectComponentBehaviour
{
    private readonly static float MoveTolerance = 0.01f;


    public int Level = 0;
    public float MoveSpeed = 5.0f;
    public float MaxMoveTime = 2.0f;

    public GameObject Target;

    public bool StoppedMovement
    {
        get
        {
            if (movePosition == null)
            {
                return true;
            }
            if (moveTime < 0)
            {
                return true;
            } 
            return (rigidbodyComponent.position - movePosition).Value.magnitude < MoveTolerance;
        }
    }

    public bool HasMovePostiton
    {
        get
        {
            return movePosition != null;
        }
    }

    [GetComponent]
    private Rigidbody2D rigidbodyComponent;

    private Vector2? movePosition = null;
    private float moveTime = 0;

    public void MoveTo(Vector2 movePosition)
    {
        this.moveTime = MaxMoveTime;
        this.movePosition = movePosition;
    }

    public void StopMoiving()
    {
        this.moveTime = 0;
        this.movePosition = null;
    }

    public float GetDistanceToTarget()
    {
        if (Target == null)
        {
            return 0;
        }
        return ((Vector2)Target.transform.position - rigidbodyComponent.position).magnitude;
    }

    private void Update()
    {
        if (Target.IsDestroyed())
        {
            Target = null;
        }
    }

    private void FixedUpdate()
    {
        if (StoppedMovement)
        {
            return;
        }
        var delta = movePosition - rigidbodyComponent.position;
        var direction = delta.Value.normalized;
        rigidbodyComponent.velocity = direction * MoveSpeed;
        moveTime -= Time.fixedDeltaTime;
    }
}