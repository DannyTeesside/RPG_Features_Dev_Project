using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{

    [SerializeField] ChasePath chasePath;
    [SerializeField] float waypointTolerance = 3f;
    [SerializeField] ChaseHealth chaseHealth;

    ChaseMover mover;

    Vector3 chaseStart;

    bool fallen = false;
    public bool finished = false;
    bool standing = true;

    int currentWaypointIndex = 0;

    private void OnEnable()
    {
        chaseHealth.gameObject.SetActive(true);
        mover = GetComponent<ChaseMover>();
        currentWaypointIndex = 0;
        chasePath = GameObject.FindGameObjectWithTag("ChasePath").GetComponent<ChasePath>();


        chaseStart = transform.position;
        fallen = false;
        finished = false;
        standing = true;
    }

    private void Update()
    {
        FollowWayPoints();


    }


    [SerializeField] private LayerMask unitLayer;



    //void AttackClosest()
    //{
    //    var closestEnemy = ClosestEnemy();
    //    if (closestEnemy != null && autoAttack.CanAttack(closestEnemy)) autoAttack.Attack(closestEnemy);
    //    else FollowWayPoints();
    //}

    void FollowWayPoints()
    {

        if (chasePath != null && standing)
        {
            if (AtWaypoint())
            {
                CycleWaypoint();
            }
            mover.StartMoveAction(GetCurrentWaypoint());
        }
        if (fallen || finished)
        {
            mover.Cancel();
        }

    }

    void Fall()
    {
        standing = false;
        fallen = true;
        chaseHealth.ReducePizzaHealth();
    }

    void GetUp()
    {
        standing = true;
        fallen = false;
    }

    bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    void CycleWaypoint()
    {
        currentWaypointIndex = chasePath.GetNextWaypointIndex(currentWaypointIndex);
    }

    Vector3 GetCurrentWaypoint()
    {
        return chasePath.GetWaypoint(currentWaypointIndex);
    }
}
