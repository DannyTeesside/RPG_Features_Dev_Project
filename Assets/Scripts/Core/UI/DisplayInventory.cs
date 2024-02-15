using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{

    public InventoryObject inventory;

    int xStart = -1400;
    int yStart = 700;
    
    public int xSpaceBetweenItems;
    public int numberOfColumns;
    public int ySpaceBetweenItems;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                if (inventory.Container[i].amount >= 1)
                {
                    itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name + " x" + inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed[inventory.Container[i]].GetComponent<ItemButton>().item = inventory.Container[i].item;
                }
                else
                {
                    Destroy(itemsDisplayed[inventory.Container[i]]);
                    inventory.Container.Remove(inventory.Container[i]);
                }
            }
            else
            { 
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name + " x" + inventory.Container[i].amount.ToString("n0"); 
            itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name + " x" + inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

    public Vector3 GetPosition(int i) 
        {
            return new Vector3(xStart + (xSpaceBetweenItems *(i % numberOfColumns)), yStart + (-ySpaceBetweenItems * (i/numberOfColumns)), 0f);
        }
    
}
