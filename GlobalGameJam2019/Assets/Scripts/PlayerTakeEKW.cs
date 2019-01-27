using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeEKW : MonoBehaviour
{
    public Transform player;
    public Transform EKWParent;
    public Animator animator;
    Transform child = null;

    public bool take()
    {
        if (child!=null) {
            child.parent = player.parent;
            child = null;
            return true;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1)) {
            if (hit.collider.tag!="EKW") {
                return false;
            }
            hit.collider.transform.parent = EKWParent;
            hit.collider.transform.position = EKWParent.position;
            hit.collider.transform.rotation = EKWParent.rotation;
            animator.SetBool("Pushing", true);
            child = hit.collider.transform;
            return true;
        }

        return false;
    }
}
