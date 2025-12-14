using UnityEngine;

public class CraftingZone : MonoBehaviour
{
    [Header("Crafting UI Reference")]
    [SerializeField] private GameObject craftingMenuUI;   // Drag your CraftingMenu Canvas here
    [SerializeField] CraftingMenuUI updateCraft;

    private bool playerNearby = false;
    private bool isQuitting = false; // 🧩 prevents errors when exiting Play Mode

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.C))
        {
            if (craftingMenuUI == null)
            {
                Debug.LogWarning("⚠️ CraftingMenuUI not assigned in the Inspector!");
                return;
            }

            bool isActive = craftingMenuUI.activeSelf;
            craftingMenuUI.SetActive(!isActive);

            if (craftingMenuUI.activeSelf)
            {
                updateCraft.UpdateResourceDisplay();
            } // updates resources if menuUI is open
            Debug.Log("🪓 Pressed C near the house - toggling menu: " + !isActive);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            playerNearby = true;
            
            Debug.Log("✅ Player entered crafting zone");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Skip if exiting play mode or object destroyed
        if (isQuitting || !Application.isPlaying) return;
        if (other == null) return;
        if (!other.CompareTag("Player")) return;

        playerNearby = false;

        if (craftingMenuUI != null)
        {
            craftingMenuUI.SetActive(false);
            Debug.Log("🚪 Player left crafting zone (menu closed safely)");
        }
    }

    // Called when Unity stops playing or object is destroyed
    void OnDisable()
    {
        isQuitting = true;
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}