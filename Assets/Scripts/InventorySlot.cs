using UnityEngine;
using UnityEngine.EventSystems;   // ✅ Needed for IPointerClickHandler and PointerEventData

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem;          // ✅ Must be a field (not { get; set; })
    public SlotTag myTag = SlotTag.None;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inventory.carriedItem == null) return;
        if (myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;

        SetItem(Inventory.carriedItem);
    }

    public void SetItem(InventoryItem item)
    {
        Inventory.carriedItem = null;

        if (item.activeSlot != null)
            item.activeSlot.myItem = null;   // Reset old slot

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (myTag != SlotTag.None)
            Inventory.Singleton.EquipEquipment(myTag, myItem);
    }
}