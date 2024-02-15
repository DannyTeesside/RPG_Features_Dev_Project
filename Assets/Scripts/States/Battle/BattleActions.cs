using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActions : MonoBehaviour
{
    GameObject stateMachineGO;
    Battle battle;

    private void Start()
    {
        stateMachineGO = GameObject.Find("StateMachine");
        battle = stateMachineGO.GetComponent<Battle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("X"))
        {
            StartCoroutine(battle.PlayerAttack());
        }
    }
}
