using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battle : MonoBehaviour
{

    public enum BattleState { Start, PlayerTurn, EnemyTurn, Win, Lose, None };

    StateMachine stateMachine;

    public GameObject battleUI;

    [SerializeField] GameObject arena;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    BattleMovment playerMovement;
    BattleMovment enemyMovement;

    Vector3 playerBase;
    Vector3 enemyBase;
    Vector3 midpoint;

    bool optionsActive = false;


    Animator playerAnim;

    BattleState currentState;
    // Start is called before the first frame update

    private void OnEnable()
    {
        DisableBattleGUI();
        stateMachine = GetComponent<StateMachine>();
        playerAnim = player.GetComponent<Animator>();
        midpoint = (player.transform.position + enemy.transform.position) / 2;
        Instantiate(arena, midpoint, Quaternion.identity);
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase").transform.position;
        enemyBase = GameObject.FindGameObjectWithTag("EnemyBase").transform.position;

        playerMovement = player.GetComponent<BattleMovment>();
        enemyMovement = enemy.GetComponent<BattleMovment>();

        playerMovement.enabled = true;

        currentState = BattleState.Start;


        //Debug.Log("BattleScript enabled");

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (BattleState.Start):
                StartCoroutine(SetupBattle());
                break;
            case (BattleState.PlayerTurn):
                PlayerTurn();
                break;
            case (BattleState.EnemyTurn):

                break;
            case (BattleState.Win):

                break;
            case (BattleState.Lose):

                break;
            case (BattleState.None):

                break;
        }
    }

    IEnumerator SetupBattle()
    {


        //player.transform.position = playerBase.transform.position;
        enemy.transform.position = enemyBase;
        playerMovement.MoveTo(playerBase);
        //enemyMovement.MoveTo(enemyBase);

        yield return new WaitForSeconds(3f);

        currentState = BattleState.PlayerTurn;
        

    }

    void PlayerTurn()
    {
        if (!optionsActive)
        {
            Debug.Log("Players turn");
            EnableBattleGUI();
        }
    }

    void EnableBattleGUI()
    {
        battleUI.SetActive(true);
        optionsActive = true;
    }
    void DisableBattleGUI()
    {
        battleUI.SetActive(false);
        optionsActive = false;
    }

    public IEnumerator PlayerAttack()
    {
        
        playerMovement.MoveTo(enemyBase);

        yield return new WaitForSeconds(3.5f);
        playerAnim.Play("Attack");
        yield return new WaitForSeconds(1.5f);
        playerMovement.MoveTo(playerBase);
        
        

    }
}
