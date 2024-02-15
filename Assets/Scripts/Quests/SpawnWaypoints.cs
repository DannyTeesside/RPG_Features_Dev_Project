using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaypoints : MonoBehaviour
{

    [SerializeField] GameObject waypoints;

    public void SpawnPath()
    {
        Instantiate(waypoints);
    }
}
