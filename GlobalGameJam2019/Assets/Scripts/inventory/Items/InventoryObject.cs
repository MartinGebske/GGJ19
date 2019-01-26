using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryObject
{
    public ItemType itemType;
    private ItemSystemEntry _itemSystemEntry = null;
    public ItemSystemEntry itemSystemEntry
    {
        get
        {
            if (_itemSystemEntry==null) {
                _itemSystemEntry = GlobalItemSystem.pInstance.getEntry(itemType);
            }
            return _itemSystemEntry;
        }
    }


    public float size
    {
        get
        {
            return itemSystemEntry.size;
        }
    }

    public enum ItemType
    {
        wood = 1, scrap = 2, stone = 3
    }
}
