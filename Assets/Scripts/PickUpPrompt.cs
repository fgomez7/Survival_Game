using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    public void Show(string message)
    {
        promptText.text = message;
        promptText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        promptText.gameObject.SetActive(false);
    }
}
