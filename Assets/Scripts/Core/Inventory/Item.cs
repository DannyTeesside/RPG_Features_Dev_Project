using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.PlayerController;

public class Item : MonoBehaviour
{
    public ItemObject item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            var item = GetComponent<Item>();
            if (item)
            {
                player.GetComponent<Player>().inventory.AddItem(item.item, 1);
                Destroy(gameObject);
            }
        }
    }
}
