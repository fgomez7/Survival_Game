using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarSelector : MonoBehaviour
{
    public int selectedIndex = 0;

    void Update()
    {
        HandleNumberKeys();
        HandleScrollWheel();
    }

    void HandleNumberKeys()
    {
        for (int i = 0; i < 7; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Select(i);
            }
        }
    }

    void HandleScrollWheel() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0.1f)
        {
            Select((selectedIndex + 6) % 7);
        }
        else if (scroll < -0.1f)
        {
            Select((selectedIndex + 1) % 7);
        }
    }

    public void Select(int i)
    {
        selectedIndex = i;
        ItemEquipper.Singleton.EquipFromHotbar(i);
    }
}
