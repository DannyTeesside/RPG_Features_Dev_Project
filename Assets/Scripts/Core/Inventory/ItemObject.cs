using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Armour,
    Material,
    KeyItem,
    Junk

}

public abstract class ItemObject : ScriptableObject
{
    public  string itemName;
    public float itemPrice;
    public GameObject prefab;
    public ItemType type;
    [TextArea(10, 14)] public string description;
    public List<float> effects = new List<float>();

    //Effects list:
    //    Consumable: 0 - Health heal amount | 1 - MP heal amount
    //    Armour:
    //    Material:
    //    KeyItem:
    //    Junk:


    public string GetItemName()
    {
        return itemName;
    }

    public float GetItemPrice()
    {
        return itemPrice;
    }

    public void UseItem()
    {
        if (type == ItemType.Consumable)
        {
           float healHPAmount = effects[0];
           float healMPAmount = effects[1];
            Debug.Log("Healing player Health by " + healHPAmount + " HP");
            Debug.Log("Recovering player MP by " + healMPAmount + " MP");
        }
    }

}
