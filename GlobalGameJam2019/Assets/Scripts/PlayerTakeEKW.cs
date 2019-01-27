using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeEKW : MonoBehaviour
{
    public Transform EKWParent;

    public bool take()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1)) {
            if (hit.collider.tag!="EKW") {
                return false;
            }
            hit.collider.transform.parent = EKWParent;
            hit.collider.transform.position = EKWParent.position;
            hit.collider.transform.rotation = EKWParent.rotation;

            return true;
        }

        return false;
    }
}
