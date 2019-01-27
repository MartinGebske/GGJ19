using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeEKW : MonoBehaviour
{
    public Transform player;
    public Transform EKWParent;
    public Animator animator;
    Transform child = null;

    public InventorySystem ekwSystem;
    public InventorySystem hoboHomeSystem;

    public bool take()
    {
        if (child!=null) {
            child.parent = player.parent;
            child = null;

            ekwSystem.autoUpdateUI = false;
            hoboHomeSystem.autoUpdateUI = true;
            hoboHomeSystem.ui.UpdateUI(hoboHomeSystem);
            return true;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 3)) {
            if (hit.collider.tag!="EKW") {
                return false;
            }
            hit.collider.transform.parent = EKWParent;
            hit.collider.transform.position = EKWParent.position;
            hit.collider.transform.rotation = EKWParent.rotation;
            animator.SetBool("Pushing", true);
            child = hit.collider.transform;
            hoboHomeSystem.autoUpdateUI = false;
            ekwSystem.autoUpdateUI = true;
            ekwSystem.ui.UpdateUI(ekwSystem);
            return true;
        }

        return false;
    }
}
