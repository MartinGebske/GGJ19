using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
public class Enemy : MonoBehaviour
{
  AICharacterControl charControl;

  // Start is called before the first frame update
  void Start()
  {
    charControl = GetComponent<AICharacterControl>();

#if UNITY_EDITOR
    var tempTarget = new GameObject("Temp Target");
    tempTarget.transform.position = new Vector3(35f, 0f, 20f);
    this.SetTarget(tempTarget.transform);
#endif
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetTarget(Transform target)
  {
    charControl.target = target;
  }
}
