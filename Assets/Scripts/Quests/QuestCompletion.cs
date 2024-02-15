using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestCompletion : MonoBehaviour
    {

        [SerializeField] Quest quest;
        [SerializeField] string task;

        public void CompleteTask()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.CompleteObjective(quest, task);
        }
    }
}
