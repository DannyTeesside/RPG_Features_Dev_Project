using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.PlayerController
{
    public class Player : MonoBehaviour
    {

        ThirdPersonMovement freeMovement;
        BattleMovment battleMovement;
        CharacterController characterController;

        public InventoryObject inventory;

        [SerializeField] StateMachine state;

        Animator anim;
        [SerializeField] RuntimeAnimatorController freeController;
        [SerializeField] RuntimeAnimatorController battleController;
        [SerializeField] RuntimeAnimatorController chaseAnimationController;
        [SerializeField] RuntimeAnimatorController deliveryController;

        [SerializeField] Transform pizzaBoxTransform;
        [SerializeField] GameObject pizzaPrefab = null;

        [SerializeField] GameObject chaseReturnPoint;

        NavMeshAgent navmeshAgent;

        AudioSource audioSource;


        ChaseMover chaseMovement;
        ChaseController chaseController;





        Vector3 moveTarget;

        public float strength = 10f;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            freeMovement = GetComponent<ThirdPersonMovement>();
            battleMovement = GetComponent<BattleMovment>();
            chaseMovement = GetComponent<ChaseMover>();
            chaseController = GetComponent<ChaseController>();
            navmeshAgent = GetComponent<NavMeshAgent>();
            state.onChaseStart += StartChase;
            state.onRoaming += StartRoam;
            state.onBattleStart += StartBattle;


        }

        

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (state.currentGameState == StateMachine.GameState.Battling)
            {
                freeMovement.enabled = false;
                
                anim.runtimeAnimatorController = battleController;
                if (moveTarget != new Vector3 (0f,0f,0f))
                {
                    GetComponent<BattleMovment>().MoveTo(moveTarget);
                    Debug.Log("Player script returned movetarget not null" + moveTarget);
                }
            }
            if (state.currentGameState == StateMachine.GameState.CutScene)
            {
                freeMovement.enabled = false;
                anim.runtimeAnimatorController = battleController;

            }
        }

        private void Update()
        {

        }

        void StartRoam()
        {
            battleMovement.enabled = false;
            anim.runtimeAnimatorController = freeController;
            freeMovement.enabled = true;
            if (!characterController.enabled)
            {
                characterController.enabled = true;
            }
            if (navmeshAgent.enabled || chaseMovement.enabled || chaseController.enabled)
            {
                navmeshAgent.enabled = false;
                chaseMovement.enabled = false;
                chaseController.enabled = false;
                DestroyOldWeapon(pizzaBoxTransform);
            }
        }

        private void StartBattle()
        {
            BattleManager battleManager = GameObject.FindGameObjectWithTag("GlobalStateMachine").GetComponent<BattleManager>();
            battleManager.heroesToManage.Add(this.gameObject);
            characterController.enabled = false;
            freeMovement.enabled = false;
            anim.runtimeAnimatorController = battleController;
            navmeshAgent.enabled = true;
            chaseMovement.enabled = false;
            chaseController.enabled = false;
        }

        void StartChase()
        {
            Instantiate(chaseReturnPoint);
            chaseReturnPoint.transform.position = transform.position;
            freeMovement.enabled = false;
            //characterController.enabled = false;

            anim.runtimeAnimatorController = deliveryController;
            transform.position = GameObject.FindGameObjectWithTag("ChasePath").transform.GetChild(0).transform.position;
            //transform.position = chaseStartPoint.transform.position;

            SpawnPizza(pizzaBoxTransform);

            chaseMovement.enabled = true;
            chaseController.enabled = true;
            navmeshAgent.enabled = true;
            //characterController.enabled = true;


        }

        

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "NPC")

            {
                audioSource.Play();
            }
        }



        const string pizzaName = "Pizza";

        void SpawnPizza(Transform handTransform)
        {
            DestroyOldWeapon(handTransform);
            GameObject pizzaObject = Instantiate(pizzaPrefab, handTransform);
            pizzaObject.name = pizzaName;
        }

        public void DestroyOldWeapon(Transform handTransform)
        {
            Transform oldPizza = handTransform.Find(pizzaName);
            if (oldPizza == null) return;

            oldPizza.name = "DESTROYING";
            Destroy(oldPizza.gameObject);
        }

        public void SaveInventory()
        {
            inventory.Save();
        }

        public void LoadInventory()
        {
            inventory.Load();
        }

        private void OnApplicationQuit()
        {
            inventory.Container.Clear();
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Item")
        //    {
        //        var item = other.GetComponent<Item>();
        //        if (item)
        //        {
        //            inventory.AddItem(item.item, 1);
        //            Destroy(other.gameObject);
        //        }
        //    }
        //}





    }
}
