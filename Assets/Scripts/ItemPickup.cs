using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // Reference to the item asset
    private bool isPlayerNear = false;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Singleton.SpawnInventoryItem(item);
            Destroy(gameObject);
        }

        if (spriteRenderer != null)
            spriteRenderer.color = isPlayerNear ? Color.yellow : Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerNear = false;
        Debug.Log("Triggered by: " + collision.name);
        if (collision.CompareTag("Player"))
            isPlayerNear = true;
    }

}
