using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class c_item : ScriptableObject
{
    public int id;
    public string itemName;
    [TextArea(0,3)]
    public string itemDescription;
    public Sprite art;

    public int requiredWood;
    public int requiredStone;
    public int requiredStrings;

    [Header("Usar solo si el item llama un evento al crearse.")]
    public UnityEvent customCraftEvent;

    public virtual void OnCraft() 
    {
        customCraftEvent.Invoke();
    }

    public scr_inventorysystem GetInventory() 
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_inventorysystem>();
    }

    public virtual bool canCraft()
    {
        return true;
    }
}