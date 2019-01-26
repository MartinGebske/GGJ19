using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public InventoryUI ui;
    public bool autoUpdateUI = false;
    public List<InventoryObject> inventory = new List<InventoryObject>();
    public int maxItems = -1;

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
        if (maxItems == -1 ||usedSize + obj.size > maxItems) {
            return false;
        }
        inventory.Add(obj);

        if (ui != null && autoUpdateUI) {
            ui.UpdateUI(this);
        }
        return true;
    }

    public int GetItemCount(InventoryObject.ItemType type)
    {
        return inventory.Count(item => item.itemType==type);
    }

    public void Remove(InventoryObject.ItemType type, int count)
    {
        InventoryObject[] objs = inventory.Where(item => item.itemType == type).ToArray();
        if (objs.Length<count) {
            Debug.LogWarning("cant remove that much, reducing amount to: "+ objs.Length);
            count = objs.Length;
        }
        for (int i = 0; i < count; i++) {
            inventory.Remove(objs[i]);
        }

        if (ui!=null && autoUpdateUI) {
            ui.UpdateUI(this);
        }
    }
}
