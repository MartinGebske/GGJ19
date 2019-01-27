using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InventoryTransferZone : MonoBehaviour
{
    public InventorySystem destination;

    void OnTriggerEnter(Collider col)
    {
        InventorySystem source = col.GetComponent<InventorySystem>();
        if (source == null) {
            return;
        }

        destination.inventory.AddRange(source.inventory);
        source.inventory.Clear();

        //source.autoUpdateUI = false;
        //destination.autoUpdateUI = true;
        source.ui.UpdateUI(source);
    }

    void OnTriggerExit(Collider col)
    {
        Debug.LogWarning("Exit");
        InventorySystem leaving = col.GetComponent<InventorySystem>();
        if (leaving == null) {
            return;
        }

        //leaving.autoUpdateUI = true;
        //leaving.ui.UpdateUI(leaving);
    }

}
