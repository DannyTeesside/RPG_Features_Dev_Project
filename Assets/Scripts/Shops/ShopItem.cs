using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Shops
{
    public class ShopItem
    {
        ItemObject item;
        float price;
        int quantityInTransaction;

        public ShopItem(ItemObject item, float price, int quantityInTransaction)
        {
            this.item = item;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;


        }

        public string GetName()
        {
            return item.GetItemName();
        }

        public float GetPrice()
        {
            return price;
        }


    }
}
