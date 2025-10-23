using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }
    public SlotTag myTag;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inventory.carriedItem == null) return;
        if (myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;
        SetItem(Inventory.carriedItem);
    }

    public void SetItem(InventoryItem item)
    {
        Inventory.carriedItem = null;

        //reset old slot
        item.activeSlot.myItem = null;

        // set current slot
        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (myTag != SlotTag.None)
        {
            Inventory.Singleton.EquipEquipment(myTag, myItem);
        }

    }

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    
}

// hello there
