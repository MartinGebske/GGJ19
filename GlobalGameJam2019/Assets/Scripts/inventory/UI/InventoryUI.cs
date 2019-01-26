using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryUIItem uiItemPrefab;
    public HorizontalLayoutGroup layoutGroup;
    private Dictionary<InventoryObject.ItemType, InventoryUIItem> inventoryUIItems = new Dictionary<InventoryObject.ItemType, InventoryUIItem>();

    public void UpdateUI(InventorySystem source)
    {
        Debug.Log("UPDATE");
        foreach (ItemSystemEntry entry in GlobalItemSystem.pInstance.itemSystemEntries) {
            if (source.GetItemCount(entry.itemType)==0) {
                continue;
            }

            if (!inventoryUIItems.ContainsKey(entry.itemType)) {
                inventoryUIItems.Add(entry.itemType,Instantiate<InventoryUIItem>(uiItemPrefab,transform));
                inventoryUIItems[entry.itemType].image.sprite = entry.icon;
                updateLayout(inventoryUIItems[entry.itemType].gameObject);

            }
            inventoryUIItems[entry.itemType].textMesh.text = source.GetItemCount(entry.itemType).ToString();
        }
    }

    public IEnumerator updateLayout(GameObject gO)
    {
        yield return new WaitForEndOfFrame();
    }
}