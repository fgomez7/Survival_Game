using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class InteractionDetector : MonoBehaviour
{
    public GameObject interactableDetector;
    public GameObject C_Detector;
    private ItemPickup nearbyItem;
    public bool isColliding = false;


    private void Start()
    {
        interactableDetector.SetActive(false);
        C_Detector.SetActive(false);
    }

    //private void Update()
    //{
    //    // Just show prompt if something interactable is nearby
    //    //interactableDetector.SetActive(nearbyItem != null);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactableDetector.SetActive(true);
        }
        else if (collision.CompareTag("Crafing_House"))
        {
            C_Detector.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactableDetector.SetActive(false);
        }
        else if (collision.CompareTag("Crafing_House"))
        {
            C_Detector.SetActive(false);
        }
    }
}
