using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string NPCName;
        StateMachine stateMachineScript;


        bool isInRange = false;
        GameObject player;
        PlayerConversant playerConversant;

        private void Start()
        {
            stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
        }

        private void Update()
        {
            Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")

            {
                player = other.gameObject;
                isInRange = true;
            }
        }




        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")

            {
                isInRange = false;
            }
        }

        void Interact()
        {
            if (dialogue == null)
            {
                return;
            }

            if (Input.GetButtonDown("X") && isInRange)
            {
                
                if (stateMachineScript.currentGameState == StateMachine.GameState.Roaming)
                {
                    StartConvo();
                }
            }

        }

        void StartConvo()
        {
            playerConversant = player.GetComponent<PlayerConversant>();
            if (playerConversant.IsActive())
            {
                return;
            }
            playerConversant.StartDialogue(this, dialogue);
        }

        public string GetName()
        {
            return NPCName;
        }


    }
}
