using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurePursuit : MonoBehaviour
{
    [SerializeField]
    public List<Vector2> path;

    public float lookAheadDistance = 0.8f;

    public float linearVelocity = 0.8f;

    private Vector2 currentPos;
    private Vector2 goalPoint;
    private int i = 0;

    public float searchRadius;

    private void FixedUpdate()
    {
        currentPos = transform.position;

        for (int k = 0; k < path.Count; k++)
        {
            if (((path[k].x - currentPos.x) * (path[k].x - currentPos.x) + (path[k].y - currentPos.y) * (path[k].y - currentPos.y)) <= searchRadius * searchRadius)
            {
                i = k;
            }
        }

        if (path.Count != 0)
        {
            goalPoint = new Vector2(path[i].x, path[i].y);
            // Calculate the angle to the goal
            float angle = Vector2.SignedAngle(new Vector2(transform.up.x, transform.up.y), goalPoint - currentPos);

            // Rotate the vehicle towards the goal
            transform.Rotate(0, 0, angle);

            // Move the vehicle forward
            transform.position = Vector3.MoveTowards(transform.position, goalPoint, linearVelocity);

            // Check if the vehicle has reached the goal point
            if (Vector2.Distance(currentPos, goalPoint) == 0)
            {
                while (i >= 0)
                {
                    path.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            Gizmos.color = Color.red;
            foreach (Vector2 point in path)
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}