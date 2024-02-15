using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using RPG.Core;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] string playerName;
        StateMachine stateMachineScript;
        Dialogue currentDialogue;
        
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        [SerializeField] DialogueNode[] rootNode = null;

        public event Action onConversationUpdated;


        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
            stateMachineScript.EnterCutscene();
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            rootNode = FilterOnCondition(currentDialogue.GetAllRootNodes()).ToArray();
            currentNode = rootNode[rootNode.Length - 1];
            TriggerEnterAction();
            onConversationUpdated();
        }

        public void QuitConvo()
        {
            stateMachineScript.EnterRoaming();
            TriggerExitAction();
            currentDialogue = null;
            currentNode = null;
            currentConversant = null;
            onConversationUpdated();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

             return currentNode.GetText();
             
        }

        internal GameObject GetSpeakerAvatar()
        {
            if (currentNode == null)
            {
                return null;
            }
            if (currentNode.GetAvatar() == null)
            {
                return null;
            }

            return currentNode.GetAvatar();
        }

        public string GetCurrentConversantName()
        {
            if (currentNode.IsPlayerSpeaking())
            {
                return playerName;
            }
            else
            {
                return currentConversant.GetName();
            }
        }

        public AudioClip GetVoiceClip()
        {
            if (currentNode == null)
            {
                return null;
            }
            if (currentNode.GetVoiceClip() == null)
            {
                return null;
            }

            return currentNode.GetVoiceClip();
        }

        public void Next()
        {
            if (HasNext())
            {
                DialogueNode[] children = FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).ToArray();
                TriggerExitAction();
                currentNode = children[0];
                TriggerEnterAction();
                onConversationUpdated();
            }
            else
            {
                QuitConvo();
            }
        }

        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
        }

        IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        void TriggerEnterAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        void TriggerExitAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        void TriggerAction(string action)
        {
            if (action == "")
            {
                return;
            }

            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }



    }
}
