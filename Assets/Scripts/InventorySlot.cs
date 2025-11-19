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
        //myItem.activeSlot = item.activeSlot;
        if (item.activeSlot != null)
        {
            if (item.activeSlot.myItem == item)
            {
                item.activeSlot.myItem = null;
            }
            //item.activeSlot = null;
        }

        // set current slot
        //myItem.activeSlot = null;
        myItem = item;
        myItem.activeSlot = this;
        Debug.Log($"{this.myItem == null}, {item.activeSlot == this}");
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
