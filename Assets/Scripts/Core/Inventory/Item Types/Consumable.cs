using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item" , menuName = "Inventory/Items/Consumable")]
public class Consumable : ItemObject
{

    [SerializeField] float restoreHealthValue = 10f;
    [SerializeField] float restoreMPValue = 0f;

    private void Awake()
    {
        type = ItemType.Consumable;
        effects.Add(restoreHealthValue);
        effects.Add(restoreMPValue);

    }


}
