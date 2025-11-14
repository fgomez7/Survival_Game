using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ItemPickup : MonoBehaviour
{
    // public Item item; // Reference to the item asset
    // private bool isPlayerNear = false;
    // [SerializeField] private SpriteRenderer spriteRenderer;

    // private void Start()
    // {
    //     if (spriteRenderer == null)
    //         spriteRenderer = GetComponent<SpriteRenderer>();
    // }

    // private void Update()
    // {
    //     if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
    //     {
    //         Inventory.Singleton.SpawnInventoryItem(item);
    //         Destroy(gameObject);
    //     }

    //     if (spriteRenderer != null)
    //         spriteRenderer.color = isPlayerNear ? Color.yellow : Color.white;
    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //         isPlayerNear = true;
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //         isPlayerNear = false;
    //     Debug.Log("Triggered by: " + collision.name);
    //     if (collision.CompareTag("Player"))
    //         isPlayerNear = true;
    // }

    [SerializeField] private Item itemData;
    public bool playerInRange = false;
    //[SerializeField] public GameObject promptUI;
    public GameObject promptUI;

    void Start()
    {
        //if (promptUI == null)
        //{
        //    promptUI = GameObject.Find("E");
        //}
        //if (promptUI != null)
        //{
        //    promptUI.SetActive(false);
        //}
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        // if (playerInRange)
        {
            CollectItem();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            //if(promptUI != null)
            //{
            //    promptUI.SetActive(true);
            //    // promptUI.transform.SetAsLastSibling();
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            //if(promptUI != null)
            //{
            //    promptUI.SetActive(false);
            //}
        }
    }

    private void CollectItem()
    {
        Debug.Log($"Collected {itemData.name}");

        if (Inventory.Singleton != null)
        {
            Inventory.Singleton.SpawnInventoryItem(itemData);
            Destroy(gameObject);
        }

        //if (promptUI != null)
        //{
        //    promptUI.SetActive(false);
        //}
    }

}
