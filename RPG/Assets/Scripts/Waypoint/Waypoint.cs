using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;

    public Vector3 currentPosition { get; set; }
    private bool gameStarted;

    private void Start()
    {
        gameStarted = true;
        currentPosition = transform.position;   
    }

    public Vector3 GetMovementPosition(int index)
    {
        return currentPosition + points[index];
    }

    private void OnDrawGizmos()
    {


        if(gameStarted == false && transform.hasChanged)
        {
            currentPosition = transform.position;
        }

        if(points == null || points.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(points[i] + currentPosition, 0.5f);

            if (i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + currentPosition, points[i + 1] + currentPosition);
            }
        }
    }
}
