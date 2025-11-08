using UnityEngine;

public class CraftingZone : MonoBehaviour
{
    public GameObject craftingMenuUI;
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.C))
        {
            bool isActive = craftingMenuUI.activeSelf;
            craftingMenuUI.SetActive(!isActive);
            Debug.Log("Pressed C near the house");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player entered crafting zone");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            craftingMenuUI.SetActive(false);
            Debug.Log("Player left crafting zone");
        }
    }
}
