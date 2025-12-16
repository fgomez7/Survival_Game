using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuTitle : MonoBehaviour
{
    public TextMeshProUGUI menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryPersistentStorage.failed)
        {
            menu.text = "Try again, you're at level: " + InventoryPersistentStorage.currentLevel.ToString();
        }
        else if (!InventoryPersistentStorage.failed && InventoryPersistentStorage.currentLevel == 2)
        {
            menu.text = "Congratulations, going to level: 2";
        }
        else if (!InventoryPersistentStorage.failed && InventoryPersistentStorage.currentLevel == 3)
        {
            menu.text = "Congratulations on beating the game";
        }
    }
}
