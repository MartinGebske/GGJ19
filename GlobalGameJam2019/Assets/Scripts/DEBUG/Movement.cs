using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float speed = 1;
    public Rigidbody rBody;
    // Update is called once per frame
    void Update()
    {
        rBody.velocity = new Vector3(Input.GetAxis("Horizontal")*speed, 0, Input.GetAxis("Vertical")*speed);
    }
}
