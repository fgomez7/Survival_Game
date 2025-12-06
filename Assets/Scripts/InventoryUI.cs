using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Singleton;
    public GameObject inventoryPanel;
    public bool isInventoryOpen;
    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("ui is awake");
        Singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Inventory.Singleton.RefreshInventoryUI();
        }
    }
}
