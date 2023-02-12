using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "Items/ItemDatabase")]
public class ItemDataBase : ScriptableObject
{
    public c_item[] gameItems; 
}
