using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Callover : MonoBehaviour
{
    [SerializeField] AudioClip[] nPCreactions;


    bool bumped = false;
    bool isInRange = false;
    bool isPlaying = false;
    bool isPlayingBump = false;
    AudioSource callOver;
    StateMachine stateMachineScript;


    private void Start()
    {
        callOver = GetComponent<AudioSource>();
        stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
    }

    private void Update()
    {
        EnvironmentVoice();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")

        {
            isInRange = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")

        {
            bumped = true;
        }
    }



    void EnvironmentVoice()
    {
        if(isInRange == true && isPlaying == false && bumped == false && stateMachineScript.currentGameState != StateMachine.GameState.CutScene)
        {
            callOver.clip = nPCreactions[0];
            callOver.Play();
            StartCoroutine(PlayingCallOver());
        }
        if (stateMachineScript.currentGameState == StateMachine.GameState.CutScene)
        {
            callOver.Stop();
        }
        else if (bumped && !isPlayingBump)
        {
            
            StartCoroutine(PlayingBumped());
        }
    }

    IEnumerator PlayingCallOver()
    {
        isPlaying = true;
        yield return new WaitForSeconds(callOver.clip.length);
        isPlaying = false;
    }

    IEnumerator PlayingBumped()
    {
        isPlayingBump = true;
        callOver.clip = nPCreactions[1];
        callOver.Play();
        yield return new WaitForSeconds(callOver.clip.length);
        bumped = false;
        isPlayingBump = false;
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")

        {
            isInRange = false;
        }
    }
}
