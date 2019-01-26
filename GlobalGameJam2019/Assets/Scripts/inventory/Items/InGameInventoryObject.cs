using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InGameInventoryObject : MonoBehaviour
{
    public InventoryObject item;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("OnTriggerEnter", col.gameObject);
        if (col.tag != "EKW") {
            return;
        }
        Debug.Log("found EKW");
        EKW ekw = col.GetComponent<EKW>();

        ekw.Pickup(this);
    }
}
