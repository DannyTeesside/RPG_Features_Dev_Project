using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour
    {

        StateMachine stateMachineScript;


        bool isInRange = false;
        GameObject player;

        [SerializeField] List<ItemObject> items = new List<ItemObject>();

        

        public event Action onChange;


        private void Start()
        {
            stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
        }


        private void Update()
        {
            Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")

            {
                player = other.gameObject;
                isInRange = true;
            }
        }




        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")

            {
                isInRange = false;
            }
        }

        void Interact()
        {

            if (Input.GetButtonDown("X") && isInRange)
            {

                if (stateMachineScript.currentGameState == StateMachine.GameState.Roaming)
                {
                    //open shop
                    player.GetComponent<Shopper>().SetActiveShop(this);
                }
            }

        }


        public IEnumerable<ShopItem> GetFilteredItems()
        {
            foreach (ItemObject item in items)
            {

            yield return new ShopItem(item, item.GetItemPrice(), 1);
            }
            //return null;
        }

        public void FilterItems(ItemType itemType)
        {

        }

        public ItemType GetFilter()
        {
            return ItemType.Consumable;
        }

        public void SelectMode(bool isBuying)
        {

        }

        public bool IsBuyingMode()
        {
            return true;
        }

        public bool CanBuy()
        {
            return true;
        }

        public void ConfirmTransaction()
        {

        }

        public float TransactionTotal()
        {
            return 0;
        }

        public void AddToTransaction(ItemObject item, int quantity)
        {

        }
    }
}
