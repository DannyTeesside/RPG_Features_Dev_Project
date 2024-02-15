using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{

    [SerializeField] GameObject menuToOpen;
    [SerializeField] GameObject menuToClose;

    public void ChangeMenu()
    {
        menuToOpen.SetActive(true);
        menuToClose.SetActive(false);
    }


    
}
