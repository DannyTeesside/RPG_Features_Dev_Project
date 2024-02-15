using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    GameObject stateMachineGO;
    StateMachine stateMachineScript;

    private void Start()
    {
        stateMachineGO = GameObject.Find("StateMachine");
        stateMachineScript = stateMachineGO.GetComponent<StateMachine>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Starting Battle");
            stateMachineScript.EnterBattle();
            Destroy(gameObject);

        }
    }
}
