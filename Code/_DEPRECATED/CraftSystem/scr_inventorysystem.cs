using System.Collections.Generic;
using UnityEngine;

public class scr_inventorysystem : MonoBehaviour
{
    public int[] resources = { 0, 0, 0};
    //[HideInInspector] 
    public List<c_item> itemsStored;

    [SerializeField]
    ItemDataBase itemDataBase;

    public void AddResource(int index, int quantity)
    {
        resources[index] += quantity;
    }

    public void AddItemByID(int itemID)
    {
        foreach (c_item item in itemDataBase.gameItems)
        {
            if (item.id == itemID)
            {
                itemsStored.Add(item);
            }
        }
        if (GetItem("Guantelete"))
        {
            //GetComponent<scr_gamestate>().makeAllBoxesMovable();
        }
    }

    public int[] GetInventory()
    {
        int[] idStored = new int[itemsStored.Count];

        for (int i = 0; i < itemsStored.Count; i++)
        {
            idStored[i] = itemsStored[i].id;
        }

        return idStored;
    }

    public void AddItem(c_item itemToAdd)
    {
        itemsStored.Add(itemToAdd);
        Debug.Log("A�adido " + itemToAdd.ToString() + " al inventario.");
        if (GetItem("Guantelete"))
        {
            //GetComponent<scr_gamestate>().makeAllBoxesMovable();
        }
    }

    public c_item GetItem(string itemName) 
    {
        foreach (c_item item in itemsStored) 
        {
            if (item.itemName == itemName) 
            {
                return item;
            }
        }
        return null;
    }
}
