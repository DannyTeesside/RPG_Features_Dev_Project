using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Quests;

public class GoBackToQuestGiver : MonoBehaviour
{
    StateMachine stateMachineScript;
    GameObject returnPoint;
    ChaseHealth chaseHealth;

    GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            chaseHealth = GameObject.FindGameObjectWithTag("ChaseHealth").GetComponent<ChaseHealth>();
            player = other.gameObject;
            StartCoroutine(chaseHealth.EndChase());
            GetComponent<QuestCompletion>().CompleteTask();
        }
    }

    //IEnumerator EndChase()
    //{
    //    player.GetComponent<ChaseController>().finished = true;
    //    stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
    //    player.GetComponent<CharacterController>().enabled = false;
    //    returnPoint = GameObject.FindGameObjectWithTag("ChaseReturn");
    //    stateMachineScript.StartBlackFade();
    //    yield return new WaitForSeconds(1);
    //    Destroy(GameObject.FindGameObjectWithTag("ChasePath"));
    //    player.transform.position = returnPoint.transform.position;
    //    Destroy(returnPoint.gameObject);
    //    player.GetComponent<CharacterController>().enabled = true;
    //    stateMachineScript.EnterRoaming();
    //}
}
