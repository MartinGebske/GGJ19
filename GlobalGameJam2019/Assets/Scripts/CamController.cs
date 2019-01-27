using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace CAM
{
    public class CamController : MonoBehaviour
    {
        public Transform near;
        public Transform far;
        public float camSpeed = 4;
        ThirdPersonCharacter person;

        public void Update()
        {
            Vector3 targetPosition = Vector3.Lerp(near.position, far.position, person.movement);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * camSpeed);
        }
    }
}
