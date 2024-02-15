using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Saving;
using RPG.PlayerController;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour, IPredicateEvaluator , ISaveable
    {

        List<QuestStatus> statuses = new List<QuestStatus>();
        public event Action onUpdate;

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;
            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            if(onUpdate != null)
            {
                onUpdate();
            }
        }

        public void CompleteObjective(Quest quest, string task)
        {
            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(task);
            if (status.IsComplete())
            {
                GiveReward(quest);
            }
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }
        QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        private void GiveReward(Quest quest)
        {
            foreach (var reward in quest.GetRewards())
            {                
                GetComponent<Player>().inventory.AddItem(reward.item, reward.number);
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            
            switch (predicate)
            {
                case "HasQuest":
                    return HasQuest(Quest.GetByName(parameters[0]));
                case "CompletedQuest":
                    if (HasQuest(Quest.GetByName(parameters[0])))
                    {
                        return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
                    }
                    return false;
                case "ObjectiveComplete":
                    if (HasQuest(Quest.GetByName(parameters[0])))
                    {
                        return GetQuestStatus(Quest.GetByName(parameters[0])).IsObjectiveComplete(parameters[1]);
                    }
                    return false;
                case "ObjectiveNotComplete":
                    if (HasQuest(Quest.GetByName(parameters[0])))
                    {
                        return !GetQuestStatus(Quest.GetByName(parameters[0])).IsObjectiveComplete(parameters[1]);
                    }
                    return false;
            }

            return null;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach (QuestStatus status in statuses)
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null) return;

            statuses.Clear();

            foreach (object objectState in stateList)
            {
                statuses.Add(new QuestStatus(objectState));
            }
        }
    }
}
