using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : WaypointMovement
{
    [SerializeField] private MovementDirection direction;

    private readonly int walkDown = Animator.StringToHash("WalkDown");

    protected override void RotateCharacter()
    {
        if (direction != MovementDirection.Horizontal)
        {
            return;
        }

        if (PointToMove.x > lastPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void RotateVertical()
    {
        if (direction != MovementDirection.Vertical)
        {
            return;
        }

        if(PointToMove.y > lastPosition.y)
        {
            _animator.SetBool(walkDown, false);
        }
        else
        {
            _animator.SetBool(walkDown, true);
        }
    }
}
