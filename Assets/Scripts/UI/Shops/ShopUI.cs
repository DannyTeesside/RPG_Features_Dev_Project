using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Shops;
using System;
using TMPro;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {

        Shopper shopper = null;
        Shop currentShop = null;
        [SerializeField] Transform itemList;
        [SerializeField] GameObject itemPrefab;

        // Start is called before the first frame update
        void Start()
        {
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;
            ShopChanged();
        }

        void ShopChanged()
        {
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            if (currentShop == null) return;

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform child in itemList)
            {
                Destroy(child.gameObject);

            }
            if (currentShop.GetFilteredItems() != null)
            { 
                foreach (ShopItem item in currentShop.GetFilteredItems())
                {
               

                    GameObject newShopItem = Instantiate(itemPrefab, itemList);
                    newShopItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.GetName();
                    newShopItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "£" + item.GetPrice().ToString();
                    //itemPrefab.GetComponent<ItemButton>().item = item;
                }
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Circle"))
            {
                shopper.SetActiveShop(null);
                

            }
        }
    }
}
