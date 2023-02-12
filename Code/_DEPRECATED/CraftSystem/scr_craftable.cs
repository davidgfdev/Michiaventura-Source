using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_craftable : MonoBehaviour
{
    [Header("Requeriments")]
    [SerializeField] int[] rResources = { 0, 0, 0 };

    [Header("Crafted Object")]
    [SerializeField] GameObject craftedObject;

    scr_inventorysystem inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_inventorysystem>();
    }
   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You need wood: " + rResources[0] + ",stone: " + rResources[1] + ",strings: " + rResources[2]);
            if (Input.GetKeyDown(KeyCode.X) && hasMaterials())
            {
                Instantiate(craftedObject, transform.position, transform.rotation);
                inventory.AddResource(0, -rResources[0]);
                inventory.AddResource(1, -rResources[1]);
                inventory.AddResource(2, -rResources[2]);
                Destroy(gameObject);
            }
        }
    }

    bool hasMaterials()
    {
        if (inventory.resources[0] >= rResources[0] && inventory.resources[1] >= rResources[1] && inventory.resources[2] >= rResources[2])
        {
            return true;
        }
        else
        {
            Debug.Log("No tienes recursos");
            return false;
        }
    }
}
