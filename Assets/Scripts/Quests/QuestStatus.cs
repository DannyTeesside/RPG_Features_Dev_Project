using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Quests
{
    public class QuestStatus
    {
        Quest quest;
        List<string> completedObjectives = new List<string>();
        private object objectState;

        [System.Serializable]
        class QuestStatusRecord
        {
            public string questName;
            public List<string> completedObjectives;
        }

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public QuestStatus(object objectState)
        {
            QuestStatusRecord state = objectState as QuestStatusRecord;
            quest = Quest.GetByName(state.questName);
            completedObjectives = state.completedObjectives;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompletedCount()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        public bool IsComplete()
        {
            foreach (var task in quest.GetTasks())
            {
                if (!completedObjectives.Contains(task.reference))
                {
                    return false;
                }
            }
            return true;
        }

        public void CompleteObjective(string task)
        {
            if (quest.HasTask(task))
            {
                completedObjectives.Add(task);
            }
        }

        public object CaptureState()
        {
            QuestStatusRecord state = new QuestStatusRecord();
            state.questName = quest.name;
            state.completedObjectives = completedObjectives;
            return state;
        }
    }
}
