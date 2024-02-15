using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Quests;
using RPG.UI.Quests;
using UnityEngine.EventSystems;
using System;


    public class QuestItemUI : MonoBehaviour, ISelectHandler, IPointerEnterHandler
    {

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI progress;
        GameObject descriptionUIObject;

        QuestStatus status;

        public void Setup(QuestStatus status)
        {
            this.status = status;
            title.text = status.GetQuest().GetTitle();
        //progress.text = status.GetCompletedCount() + "/" + status.GetQuest().GetObjectiveCount();
            
    }

        public QuestStatus GetQuestStatus()
        {
            return status;
        }

    //public string GetObjective()
    //{
    //    return quest.GetObjective();
    //}

    //public string GetDescription()
    //{
    //    return quest.GetDescription();
    //}

    //void OnSelect(BaseEventData eventData)
    //{
    //    Debug.Log("OnSelect");
    //    descriptionUI.Setup(quest);
    //}

    private void OnMouseOver()
    {

        Debug.Log("HEY");
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        Debug.Log("OnSelect");
        descriptionUIObject = GameObject.FindGameObjectWithTag("QuestDescription");
        QuestDescriptionUI descriptionUI = descriptionUIObject.GetComponent<QuestDescriptionUI>();
        descriptionUI.Setup(status);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionUIObject = GameObject.FindGameObjectWithTag("QuestDescription");
        QuestDescriptionUI descriptionUI = descriptionUIObject.GetComponent<QuestDescriptionUI>();
        descriptionUI.Setup(status);
    }
}

