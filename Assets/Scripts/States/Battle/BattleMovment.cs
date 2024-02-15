using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleMovment : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    

    private void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        


    }

    private void Start()
    {
    }

    private void Update()
    {
        

        UpdateAnimator();
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    //public void StartMoveAction(Vector3 destination)
    //{
    //    MoveTo(destination);
    //}



    //public void MoveTo(Vector3 destination)
    //{
    //    navMeshAgent.destination = destination;
    //    //navMeshAgent.isStopped = false;
    //}
    //public void Cancel()
    //{
    //navMeshAgent.isStopped = true;
    // }


    void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("moveSpeed", speed);
    }



    }
