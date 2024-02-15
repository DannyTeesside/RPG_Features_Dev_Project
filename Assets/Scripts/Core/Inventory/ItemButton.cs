using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.PlayerController;

public class ItemButton : MonoBehaviour
{
    public ItemObject item;


    public void UseItem()
    {
        if(item.type == ItemType.Consumable)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            item.UseItem();
            player.GetComponent<Player>().inventory.RemoveItem(item, 1);
        }
    }
}
