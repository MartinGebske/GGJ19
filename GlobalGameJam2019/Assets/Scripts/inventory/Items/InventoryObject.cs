using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryObject
{
    public ItemType itemType;
    public int size
    {
        get { return (int)itemType; }
    }

    public enum ItemType
    {
        wood = 1, scrap = 2, stone = 3
    }
}
