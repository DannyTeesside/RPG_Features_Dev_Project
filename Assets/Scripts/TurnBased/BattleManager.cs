using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class BattleManager : MonoBehaviour
{

    LayerMask chooseActionLayermask;

    LayerMask selectTargetLayermask;

    bool attackSelected = false;

    bool abilitySelected = false;

    bool confuseSelected = false;

    GameObject selectedTarget;


    enum battleState
    {
        Setup,
        Wait,
        PlayerTurn,
        EnemyTurn,
        DecideAction,
        PerformAction

    }

    enum heroGUI
    {
        Activate,
        Waiting,
        ChooseAction,
        SelectTarget,
        Deactivate
    }

    [SerializeField] heroGUI currentGUIState;
    [SerializeField] GameObject battleActions;
    [SerializeField] GameObject battleChoices;
    [SerializeField] GameObject backScreen;
    [SerializeField] GameObject skillScreen;
    [SerializeField] GameObject itemScreen;

    public List<GameObject> heroesToManage = new List<GameObject>();
    //HandleTurn heroChoice;

    GameObject player;
    NavMeshAgent navMesh;

    [SerializeField] LayerMask m_LayerMask;

    [SerializeField] battleState currentBattleState;
    [SerializeField] GameObject battlefield;
    GameObject battlefieldInstance;

    //[SerializeField] List<HandleTurn> actionQueue = new List<HandleTurn>();
    [SerializeField] List<GameObject> turnOrder = new List<GameObject>();
    [SerializeField] List<GameObject> totalBattlers = new List<GameObject>();
    [SerializeField] List<GameObject> aliveBattlers = new List<GameObject>();
    [SerializeField] List<GameObject> heroesInBattle = new List<GameObject>();
    [SerializeField] List<GameObject> enemiesInBattle = new List<GameObject>();

    
    Vector3 playerBattlePoint;



    string abilityName;
    int manaCost;
    int abilityDamage;

    void OnEnable()
    {
        currentBattleState = battleState.Setup;
        currentGUIState = heroGUI.Deactivate;

        DeactivateBattleGUI();
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<BattleMovment>().enabled = true;
        navMesh = player.GetComponent<NavMeshAgent>();
        FindBattlers();
        battlefieldInstance = Instantiate(battlefield, player.transform.position, Quaternion.identity);
        playerBattlePoint = GameObject.FindGameObjectWithTag("PlayerBattlePoint").transform.position;
        //Camera.main.GetComponent<CameraFollow>().target = battlefieldInstance.transform;
        


        chooseActionLayermask = LayerMask.GetMask("BattleAction");
        selectTargetLayermask = LayerMask.GetMask("Character");

        //playerInput = player.GetComponent<InputReader>();

        //playerInput.SelectEvent += ChooseAction;
        //playerInput.SelectEvent += SelectActionTarget;

    }

    

    // Update is called once per frame
    void Update()
    {
        switch (currentBattleState)
        {

            case (battleState.Setup):
                MoveToPlayerStart();
                break;
            case (battleState.Wait):
                
                
                break;

            case (battleState.PlayerTurn):
                currentGUIState = heroGUI.Activate;
                currentBattleState = battleState.DecideAction;
                break;

            case (battleState.EnemyTurn):
                currentGUIState = heroGUI.Deactivate;
                StartCoroutine(EnemyAttack());
                currentBattleState = battleState.Wait;




                break;

            case (battleState.DecideAction):
                ChooseAction();
                break;

            case (battleState.PerformAction):
                
                break;


        }

        switch (currentGUIState)
        {
            case (heroGUI.Activate):
                if (heroesToManage.Count > 0)
                {
                    //heroChoice = new HandleTurn();
                    ActivateBattleGUI();
                }

                break;

            case (heroGUI.Waiting):

                break;

            case (heroGUI.ChooseAction):

                break;

            case (heroGUI.SelectTarget):
                SelectActionTarget();
                break;

            case (heroGUI.Deactivate):
                DeactivateBattleGUI();
                break;
        }
    }

    IEnumerator EnemyAttack()
    {
        if (!turnOrder[0].GetComponent<CharacterStats>().CheckCanAttack()) 
        {
            print(turnOrder[0].GetComponent<CharacterStats>().GetStatus());
            yield return new WaitForSeconds(2);
            turnOrder.Remove(turnOrder[0]);
            StartTurn();
        }
        else
        {
            print(turnOrder[0].name + " is deciding what to do");
            yield return new WaitForSeconds(2);
            if(turnOrder[0].GetComponent<CharacterStats>().isConfused)
            {
                print(transform.name + " is Confuzzled");
                int randomTarget = Random.Range(0, aliveBattlers.Count);
                selectedTarget = aliveBattlers[randomTarget];
            }
            else
            {
                selectedTarget = heroesInBattle[0];
            }
            print(turnOrder[0].name + " attacks " + selectedTarget.name);
            selectedTarget.GetComponent<CharacterStats>().TakeDamage(turnOrder[0].GetComponent<CharacterStats>().baseAttack.GetValue());
            turnOrder.Remove(turnOrder[0]);
            yield return new WaitForSeconds(2);
            StartTurn();
        }
        
    }

    void FindBattlers()
    {
        Collider[] hitColliders = Physics.OverlapBox(player.transform.position, new Vector3(12,12,12), Quaternion.identity, m_LayerMask);
        int i = 0;
        
        while (i < hitColliders.Length)
        {

            if(hitColliders[i].tag == "Enemy")
            {
                enemiesInBattle.Add(hitColliders[i].gameObject);
                totalBattlers.Add(hitColliders[i].gameObject);
                aliveBattlers.Add(hitColliders[i].gameObject);

            }
            if (hitColliders[i].tag == "Player")
            {
                heroesInBattle.Add(hitColliders[i].gameObject);
                totalBattlers.Add(hitColliders[i].gameObject);
                aliveBattlers.Add(hitColliders[i].gameObject);
            }
            i++;
        }
        CreateTurnOrder();
    }

    //public void CollectAction(HandleTurn input)
    //{
    //    actionQueue.Add(input);
    //}

    void CreateTurnOrder()
    {
        turnOrder.Clear();
        turnOrder.AddRange(aliveBattlers);
        //turnOrder.AddRange(heroesInBattle);
    }

    void SortByAgility()
    {
        turnOrder.Sort((a, b) => a.GetComponent<CharacterStats>().CompareTo(b.GetComponent<CharacterStats>()));

    }

    void MoveToPlayerStart()
    {
        var cc = player.GetComponent<BattleMovment>();
        var offset = playerBattlePoint - player.transform.position;
        
        navMesh.stoppingDistance = 0;
        if (offset.magnitude > 1f)
        {
            cc.MoveTo(playerBattlePoint);

        }
        else
        {
            navMesh.enabled = false;
            player.transform.LookAt(enemiesInBattle[0].transform.position);
            navMesh.enabled = true;
            StartTurn();
        }
      

        
    }

    void StartTurn()
    {
        foreach (var character in aliveBattlers.ToList())
        {
            if(character.GetComponent<CharacterStats>().isDead)
            {
                turnOrder.Remove(character.gameObject);
                heroesInBattle.Remove(character.gameObject);
                enemiesInBattle.Remove(character.gameObject);
                aliveBattlers.Remove(character.gameObject);
            }
        }

        if (enemiesInBattle.Count == 0)
        {
            WinBattle();
            return;
        }

        if (turnOrder.Count == 0)
        {
            CreateTurnOrder();
        }

        SortByAgility();


        if (turnOrder[0].tag == "Player")
        {
            currentBattleState = battleState.PlayerTurn;
        }
        else
        {
            currentBattleState = battleState.EnemyTurn;
        }
    }

    void ActivateBattleGUI()
    {
        Debug.Log("Gui called");
            //Vector3 uiPosition = new Vector3(0f, .5f, 9);
            //uiPosition = Camera.main.ViewportToWorldPoint(uiPosition);
            //battleActions.transform.position = uiPosition;
            //attackSelected = false;
            //abilitySelected = false;
            //confuseSelected = false;
            //backScreen.SetActive(false);
            //skillScreen.SetActive(false);
            //itemScreen.SetActive(false);
            battleActions.SetActive(true);
            battleChoices.SetActive(true);
            currentGUIState = heroGUI.ChooseAction;
    }

    void DeactivateBattleGUI()
    {
        if (battleActions.activeSelf == true)
        {
            battleActions.SetActive(false);
        }
    }

    void DeactivateChoices()
    {
        if (battleChoices.activeSelf == true)
        {
            battleChoices.SetActive(false);
            //backScreen.SetActive(true);
        }
    }

    void ChooseAction()
    {
        if (currentBattleState != battleState.DecideAction) { return; }
        
        
            if (Input.GetButtonDown("X"))
            {
                attackSelected = true;
                DeactivateChoices();
                currentGUIState = heroGUI.SelectTarget;
            }

            //if (Input.GetButtonDown("Triangle"))
            //{
            //    abilitySelected = true;
            //    DeactivateChoices();
            //    skillScreen.SetActive(true);
            //    //currentGUIState = heroGUI.SelectTarget;
            //}

            //if (Input.GetButtonDown("Square"))
            //{
                
            //    DeactivateChoices();
            //    itemScreen.SetActive(true);
            //    //currentGUIState = heroGUI.SelectTarget;
            //}


        

    }

    //public void GetAbility(AbilityButton button)
    //{
    //    button.GetAbility();
    //    print(button.GetAbilityName() + " costs " + button.GetManaCost() + " mana and deals " + button.GetDamage() + " damage");
    //    abilityName = button.GetAbilityName();
    //    manaCost = button.GetManaCost();
    //    abilityDamage = button.GetDamage();
    //    skillScreen.SetActive(false);
    //    currentGUIState = heroGUI.SelectTarget;
    //}

    void SelectActionTarget()
    {
        if (currentGUIState != heroGUI.SelectTarget) { return; }

            if (Input.GetButtonDown("Circle"))
            {
                currentGUIState = heroGUI.Activate;
            }


        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectTargetLayermask))
        //{
        //    if (hit.transform != null)
        //    {
        //        if(attackSelected)
        //        {
        //            Attack(hit);
        //        }

        //        if(abilitySelected)
        //        {
        //            CastAbility(hit);
        //        }
                
        //        if (confuseSelected)
        //        {
        //            Confuse(hit);
        //        }


        //    }
        //}
    }

    void ClearAllLists()
    {
        turnOrder.Clear();
        enemiesInBattle.Clear();
        heroesInBattle.Clear();
        aliveBattlers.Clear();
        totalBattlers.Clear();
    }
    void WinBattle()
    {
        GameObject stateMachine = GameObject.FindGameObjectWithTag("GlobalStateMachine");
        //Camera.main.GetComponent<CameraFollow>().target = player.transform;
        ClearAllLists();
        Destroy(battlefieldInstance);
        DeactivateBattleGUI();
        stateMachine.GetComponent<StateMachine>().EnterRoaming();
    }

    //Player Battle Functions

    void Attack(RaycastHit hit)
    {
        //heroChoice.actionCharacterName = heroesToManage[0].name;
        //heroChoice.actionCharacterObject = heroesToManage[0];
        //heroChoice.actionTargetObject = hit.transform.gameObject;
        currentBattleState = battleState.PerformAction;
        currentGUIState = heroGUI.Waiting;
        Debug.Log("player attacks " + hit.transform.name);
        hit.transform.gameObject.GetComponent<CharacterStats>().TakeDamage(turnOrder[0].GetComponent<CharacterStats>().baseAttack.GetValue());
        turnOrder.Remove(turnOrder[0]);
        attackSelected = false;
        StartTurn();
    }

    void CastAbility(RaycastHit hit)
    {
        currentBattleState = battleState.PerformAction;
        currentGUIState = heroGUI.Waiting;
        print("player uses " + abilityName);
        hit.transform.gameObject.GetComponent<CharacterStats>().TakeDamage(abilityDamage);
        turnOrder.Remove(turnOrder[0]);
        abilitySelected = false;
        StartTurn();



    }

    void Heal(RaycastHit hit)
    {
        currentBattleState = battleState.PerformAction;
        currentGUIState = heroGUI.Waiting;
        Debug.Log("player heals " + hit.transform.name);
        hit.transform.gameObject.GetComponent<CharacterStats>().Heal();
        turnOrder.Remove(turnOrder[0]);
        //healSelected = false;
        StartTurn();
    }

    void Confuse(RaycastHit hit)
    {
        currentBattleState = battleState.PerformAction;
        currentGUIState = heroGUI.Waiting;
        Debug.Log(hit.transform.name + " has been confused");
        hit.transform.gameObject.GetComponent<CharacterStats>().Confuse();
        turnOrder.Remove(turnOrder[0]);
        confuseSelected = false;
        StartTurn();
    }

}
