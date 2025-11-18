using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class InteractionDetector : MonoBehaviour
{
    public GameObject interactableDetector;
    private ItemPickup nearbyItem;
    public bool isColliding = false;


    private void Start()
    {
        interactableDetector.SetActive(false);
    }

    //private void Update()
    //{
    //    // Just show prompt if something interactable is nearby
    //    //interactableDetector.SetActive(nearbyItem != null);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactableDetector.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactableDetector.SetActive(false);
    }
}
