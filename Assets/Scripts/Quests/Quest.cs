using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG Project/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        //[SerializeField] string[] objectives;
        [SerializeField] string mainObjective;
        [SerializeField] string description;
        [SerializeField] List<Task> objectives = new List<Task>();
        [SerializeField] List<Reward> rewards = new List<Reward>();


        [System.Serializable]
        public class Reward
        {
            public int number;
            public ItemObject item;
        }

        [System.Serializable]
        public class Task
        {
            public string reference;
            public string description;
        }

        public string GetTitle()
        {
            return name;
        }

        public string GetObjective()
        {
            return mainObjective;
        }

        public string GetDescription()
        {
            return description;
        }

        public IEnumerable<Task> GetTasks()
        {
            return objectives;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }

        

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public bool HasTask(string taskRef)
        {
            foreach (var task in objectives)
            {
                if (task.reference == taskRef)
                {
                    return true;
                }
            }
            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
                return null;
        }

    }
}