using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Items/Inventory Item")]
public class c_inventoryitem : c_item
{
    public override void OnCraft()
    {
        GetInventory().AddItem(this);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_craftSystem>().RemoveCraftableItem(this.name);
        base.OnCraft();
    }

    public override bool canCraft()
    {
        if (!GetInventory().GetItem(this.name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
