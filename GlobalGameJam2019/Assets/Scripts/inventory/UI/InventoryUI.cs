using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventoryUIItem uiItemPrefab;
    private Dictionary<InventoryObject.ItemType, InventoryUIItem> inventoryUIItems = new Dictionary<InventoryObject.ItemType, InventoryUIItem>();

    public void UpdateUI(InventorySystem source)
    {
        foreach (ItemSystemEntry entry in GlobalItemSystem.pInstance.itemSystemEntries) {
            if (source.GetItemCount(entry.itemType)==0) {
                continue;
            }

            if (!inventoryUIItems.ContainsKey(entry.itemType)) {
                inventoryUIItems.Add(entry.itemType,Instantiate<InventoryUIItem>(uiItemPrefab,transform));
                inventoryUIItems[entry.itemType].image.sprite = entry.icon;
            }
            inventoryUIItems[entry.itemType].textMesh.text = source.GetItemCount(entry.itemType).ToString();
        }
    }
}