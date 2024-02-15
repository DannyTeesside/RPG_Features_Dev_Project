using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestDescriptionUI : MonoBehaviour
    {

        [SerializeField] Transform objectiveContainer;
        [SerializeField] TextMeshProUGUI objective;
        [SerializeField] GameObject taskPrefab;
        [SerializeField] GameObject description;
        [SerializeField] GameObject taskIncompletePrefab;


        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            objective.text = quest.GetObjective();
            //objectiveContainer.DetachChildren();
            foreach (Transform child in objectiveContainer)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (var task in quest.GetTasks())
            {
                GameObject prefab = taskIncompletePrefab;
                if(status.IsObjectiveComplete(task.reference))
                {
                    prefab = taskPrefab;
                }
                GameObject taskInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI taskText = taskInstance.GetComponentInChildren<TextMeshProUGUI>();
                taskText.text = task.description;
            }

            //description.text = quest.GetDescription();
            //Debug.Log(objective.text);
            //GameObject descriptionInstance = Instantiate(description, objectiveContainer);
            TextMeshProUGUI descriptionText = description.GetComponent<TextMeshProUGUI>();
            descriptionText.text = quest.GetDescription();
        }
    }
}
