using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Shops
{
    public class Shopper : MonoBehaviour
    {

        Shop activeshop = null;
        StateMachine stateMachineScript;

        public event Action activeShopChange;

        private void Start()
        {
            stateMachineScript = GameObject.Find("StateMachine").GetComponent<StateMachine>();
        }

        public void SetActiveShop(Shop shop)
        {
            activeshop = shop;
            if (activeShopChange != null)
            {
                activeShopChange();
                stateMachineScript.EnterCutscene();
            }
            if (activeshop == null)
            {
                stateMachineScript.EnterRoaming();
            }
        }

        public Shop GetActiveShop()
        {
            return activeshop;
        }
    }
}
