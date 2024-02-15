using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStates : MonoBehaviour
{

    StateMachine stateMachineScript;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
        stateMachineScript.onChaseStart += ChangeToChaseCam;
        stateMachineScript.onRoaming += ChangeToRoamingCam;
        
    }

    private void ChangeToRoamingCam()
    {
        animator.Play("RoamingCam");
    }

    void ChangeToChaseCam()
    {
        animator.Play("ChaseCam");
    }
}
