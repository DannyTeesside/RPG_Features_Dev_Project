using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using RPG.UI.Quests;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{

    [SerializeField] QuestItemUI questPrefab;
    [SerializeField] QuestDescriptionUI questDescription;
    QuestList questList;
    QuestItemUI uiInstance;

    // Start is called before the first frame update
    void Start()
    {
        questList.onUpdate += Redraw;

    }

    private void Redraw()
    {
        //transform.DetachChildren();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (QuestStatus status in questList.GetStatuses())
        {
            
            
            uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.Setup(status);
            questDescription.Setup(status);
        }
    }

    void OnEnable()
    {
        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        
        Redraw();
    }
}
