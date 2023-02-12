using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_craftSystem : MonoBehaviour
{
    [SerializeField]
    ItemDataBase craftDatabase;

    public c_item craftableItem;

    scr_inventorysystem inventory;

    void Awake()
    {
        inventory = GetComponent<scr_inventorysystem>();
    }

    public void RemoveCraftableItem(string itemClassName)
    {
        craftableItem = null;
    }

    public c_item GetCraftableItem()
    {
        return craftableItem;
    }

    public int GetCraftableItemId()
    {
        if (craftableItem != null)
        {
            return craftableItem.id;
        }
        else
        {
            return -1;
        }
        
    }

    public void SetCraftableItem(c_item itemToAdd)
    {
        craftableItem = itemToAdd;
    }

    public void SetCraftableItem(int id)
    {
        foreach (c_item item in craftDatabase.gameItems)
        {
            if (item.id == id)
            {
                craftableItem = item;
            }
        }
    }

    public void CraftItem()
    {
        if (CheckResources(craftableItem) && craftableItem.canCraft())
        {
            inventory.AddResource(0, -craftableItem.requiredWood);
            inventory.AddResource(1, -craftableItem.requiredStone);
            inventory.AddResource(2, -craftableItem.requiredStrings);
            craftableItem.OnCraft();
            craftableItem = null;
        }
    }

    bool CheckResources(c_item item)
    {
        if ((inventory.resources[0] >= item.requiredWood) 
            && (inventory.resources[1] >= item.requiredStone) 
            && (inventory.resources[2] >= item.requiredStrings))
        {
            return true;
        }
        else 
        {
            Debug.Log("No tienes suficientes materiales");
            return false;
        }
    }
}
