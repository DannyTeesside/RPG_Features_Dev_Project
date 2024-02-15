using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseHealth : MonoBehaviour
{

    Image healthBar;
    float currentHealth;
    float maxHealth = 100f;

    float timeTilTick = 0.01f;
    float fillPerTick = 0.02f;
    float timer;

    bool failed = false;

    StateMachine stateMachineScript;



    // Start is called before the first frame update
    void OnEnable()
    {
        failed = false;
        stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
        healthBar = transform.GetChild(1).GetComponent<Image>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachineScript.currentGameState == StateMachine.GameState.Chase)
        {
            if(currentHealth <= 0)
            {
                if (!failed)
                {
                    StartCoroutine(EndChase());
                }
                
                failed = true;
            }

            timer += Time.deltaTime;

            if (timer >= timeTilTick)
            {
                timer = 0;
                currentHealth -= fillPerTick;
            }
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void ReducePizzaHealth()
    {
        currentHealth -= 10f;
    }


    public IEnumerator EndChase()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ChaseController>().finished = true;
        stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
        player.GetComponent<CharacterController>().enabled = false;
        GameObject returnPoint = GameObject.FindGameObjectWithTag("ChaseReturn");
        stateMachineScript.StartBlackFade();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        Destroy(GameObject.FindGameObjectWithTag("ChasePath"));
        player.transform.position = returnPoint.transform.position;
        Destroy(returnPoint.gameObject);
        player.GetComponent<CharacterController>().enabled = true;
        stateMachineScript.EnterRoaming();
    }
}
