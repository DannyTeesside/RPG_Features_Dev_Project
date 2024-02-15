using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Junk Item" , menuName = "Inventory/Items/Junk")]
public class Junk : ItemObject
{

    private void Awake()
    {
        type = ItemType.Junk;
    }


}
