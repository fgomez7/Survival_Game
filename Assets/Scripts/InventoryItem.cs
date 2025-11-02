using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    // Public reference to the icon that we assign in the prefab
    public Image icon;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // ✅ Always use the manually assigned icon (from the prefab)
        if (icon == null)
        {
            icon = GetComponent<Image>();
            if (icon == null)
                Debug.LogError("InventoryItem: No Image assigned or found!");
        }
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;

        // ✅ Use the icon variable we assigned
        if (icon == null)
        {
            Debug.LogError("InventoryItem: Icon is STILL null at Initialize! (Prefab not using correct script?)");
            return;
        }

        icon.sprite = item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}
