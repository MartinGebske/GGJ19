using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(InventorySystem))]
public class EKW : MonoBehaviour
{
    public InventorySystem inventorySystem;
    void Start()
    {
        transform.tag = "EKW";
    }

    public void Pickup(InGameInventoryObject inGameInventoryObject)
    {
        if (inventorySystem.AddObject(inGameInventoryObject.item)) {
            Destroy(inGameInventoryObject.gameObject);
        }
    }
}
