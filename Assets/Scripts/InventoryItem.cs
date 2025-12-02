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
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    private bool isHovered = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // ✅ Always use the manually assigned icon (from the prefab)
        if (itemIcon == null)
        {
            itemIcon = GetComponent<Image>();
            if (itemIcon == null)
                Debug.LogError("InventoryItem: No Image assigned or found!");
        }
    }

    public void Initialize( Item item, InventorySlot parent )
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;

        // ✅ Use the icon variable we assigned
        if (itemIcon == null)
        {
            Debug.LogError("InventoryItem: Icon is STILL null at Initialize! (Prefab not using correct script?)");
            return;
        }

        itemIcon.sprite = item.sprite;
        //// ✅ Force item to align perfectly inside its slot
        //RectTransform rect = GetComponent<RectTransform>();
        //rect.anchorMin = Vector2.zero;
        //rect.anchorMax = Vector2.one;
        //rect.anchoredPosition = Vector2.zero;
        //rect.offsetMin = Vector2.zero;
        //rect.offsetMax = Vector2.zero;
        //rect.localScale = Vector3.one;
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
