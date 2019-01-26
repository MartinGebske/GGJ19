using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryObject> inventory = new List<InventoryObject>();
    public int maxItems;

    public int usedSize
    {
        get
        {
            int curSize = 0;
            foreach (InventoryObject item in inventory) {
                curSize += item.size;
            }
            return curSize;
        }
    }

    public bool AddObject(InventoryObject obj)
    {
        if (usedSize + obj.size > maxItems) {
            return false;
        }
        inventory.Add(obj);
        return true;
    }

    public int getItemCount(System.Type type)
    {
        return inventory.Count(item => item.GetType()==type);
    }
}
