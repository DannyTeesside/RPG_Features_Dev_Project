using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using RPG.Core;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        enum eSpeaker { Player, NPC1, NPC2, LastSpeaker};
        [SerializeField] bool[] speaker = new bool[(int)eSpeaker.LastSpeaker];
        [SerializeField] string text;
        [SerializeField] GameObject avatar;
        [SerializeField] AudioClip voiceClip;
        [SerializeField] string onEnterAction;
        [SerializeField] string onExitAction;
        [SerializeField] List<string> parents = new List<string>();
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect (0,0,200,100);
        [SerializeField] Condition condition;

        public string GetText()
        {
            return text;
        }

        public AudioClip GetVoiceClip()
        {
            return voiceClip;
        }

        public GameObject GetAvatar()
        {
            return avatar;
        }

        public List<string> GetChildren()
        {
            return children;
        }
        public List<string> GetParents()
        {
            return parents;
        }

        public Rect GetRect()
        {
            return rect;
        }
        public bool IsPlayerSpeaking()
        {
            //speaker[(int)eSpeaker.NPC1] = false;
            //speaker[(int)eSpeaker.NPC2] = false;
            //speaker[(int)eSpeaker.Player] = true;
            return speaker[(int)eSpeaker.Player];
        }

        public bool IsNPC2Speaking()
        {
            
            return speaker[(int)eSpeaker.NPC2];
            
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }


#if UNITY_EDITOR

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Added Dialogue link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void AddParent(string parentID)
        {
            Undo.RecordObject(this, "Added Dialogue link");
            parents.Add(parentID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Unlink Dialogue");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveParent(string parentID)
        {
            Undo.RecordObject(this, "Unlink Dialogue");
            parents.Remove(parentID);
            EditorUtility.SetDirty(this);
        }

        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Node Position");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        



#endif

    }
    
}
