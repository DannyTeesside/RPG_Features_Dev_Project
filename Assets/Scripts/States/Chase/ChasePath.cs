using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePath : MonoBehaviour
{
    const float waypointGizmoRadius = 3f;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextWaypointIndex(i);
            Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
        }
    }
    public int GetNextWaypointIndex(int i)
    {
        if (i >= transform.childCount - 1)
        { return transform.childCount - 1; }
        return i + 1;
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
