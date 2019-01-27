using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostUI : MonoBehaviour
{
    public InventoryUIItem prefab;

    Dictionary<InventoryObject.ItemType, InventoryUIItem> inventoryUIItems = new Dictionary<InventoryObject.ItemType, InventoryUIItem>();

    public void UpdateUI(HouseStats stats)
    {
        foreach (ItemSystemEntry entry in GlobalItemSystem.pInstance.itemSystemEntries) {
            CostObject costObject = stats.costs.Find(item => item.itemType == entry.itemType);
            if (costObject==null || costObject.count==0) {
                if (inventoryUIItems.ContainsKey(entry.itemType)) {
                    inventoryUIItems[entry.itemType].textMesh.text = 0.ToString();
                }
                continue;
            }

            if (!inventoryUIItems.ContainsKey(entry.itemType)) {
                inventoryUIItems.Add(entry.itemType, Instantiate<InventoryUIItem>(prefab, transform));
                inventoryUIItems[entry.itemType].image.sprite = entry.icon;
                Debug.Log("Spawn UI ITEM");

            }
            inventoryUIItems[entry.itemType].textMesh.text = stats.costs.Find(item => item.itemType == entry.itemType).count.ToString();
            Debug.Log("Setting UI ITEM");

        }
    }
}
