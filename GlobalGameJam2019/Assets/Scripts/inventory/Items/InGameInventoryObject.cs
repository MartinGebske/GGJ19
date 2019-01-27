using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InGameInventoryObject : MonoBehaviour
{
    public InventoryObject item;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "EKW") {
            return;
        }
        EKW ekw = col.GetComponent<EKW>();

        ekw.Pickup(this);
    }
}
