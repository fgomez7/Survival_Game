using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Concurrent;

public class InventoryItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Public reference to the icon that we assign in the prefab
    Image itemIcon;
    //[SerializeField] private Image itemIcon;

    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    private bool isHovered = false;

    void Awake()
    {
        //canvasGroup = GetComponent<CanvasGroup>();

        //// ✅ Always use the manually assigned icon (from the prefab)
        //if (itemIcon == null)
        //{
        //    itemIcon = GetComponent<Image>();
        //    //itemIcon = GetComponentInChildren<Image>();
        //    if (itemIcon == null)
        //        Debug.LogError("InventoryItem: No Image assigned or found!");
        //}

        canvasGroup = GetComponent<CanvasGroup>();

        // Try to get Image from root
        itemIcon = GetComponent<Image>();

        // If not on root, try children
        if (itemIcon == null)
            itemIcon = GetComponentInChildren<Image>();

        //if (itemIcon == null)
        //    Debug.LogError("InventoryItem: No Image component found on prefab or children!", this);
    }

    public void Initialize( Item item, InventorySlot parent )
    {
        itemIcon.sprite = item.sprite;
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Inventory.Singleton.DropItem(this, player.transform);
            }
            else
            {
                Debug.LogWarning("Player not found in scene!");
            }
        }
        //else if (eventData.button == PointerEventData.InputButton.Middle)
        //{

        //}
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    void Update()
    {
        if (isHovered && Input.GetKeyDown(KeyCode.R))
        {
            Inventory.Singleton.ConsumeItem(this);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            InventorySlot currslot = ItemEquipper.Singleton.CurrentSlot();
            if (currslot != null && currslot == this.activeSlot)
            {
                Inventory.Singleton.ConsumeItem(this);
            }
        }
    }
}
