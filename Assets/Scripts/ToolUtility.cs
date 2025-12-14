using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ToolUtility
{
    public static void UseTool(InventoryItem item)
    {
        if (item == null || item.myItem.maxDurability <= 0)
            return;

        item.currentDurability--;

        if (item.currentDurability <= 0)
        {
            BreakTool(item);
        }
    }

    private static void BreakTool(InventoryItem item)
    {
        if (item == null) return;

        Inventory.Singleton.ConsumeItem(item);
        ItemEquipper.Singleton.ResetEquipped();
    }

}
