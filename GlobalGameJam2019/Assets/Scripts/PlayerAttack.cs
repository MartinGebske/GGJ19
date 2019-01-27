using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;



public class PlayerAttack : MonoBehaviour
{
  public float timeBetweenAttacks = 0.5f;
  public GameObject myEnemy;


  float timer;
  bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.

  Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

    timer += Time.deltaTime;
    if (CrossPlatformInputManager.GetButtonDown("Fire1"))
    {
      if (timer >= timeBetweenAttacks && playerInRange /*&& enemyHealth.currentHealth > 0*/)
      {
        // ... attack.
        Attack();
      }

    }

#if UNITY_EDITOR
    // for testing
    if (Input.GetKeyDown(KeyCode.F))
    {
      this.Attack();
    }
#endif

  }

  void OnTriggerEnter(Collider other)
  {
    // TODO handle case when multiple enemies are in range
    // If the entering collider is the player...
    if (other.gameObject.tag == "Enemy" && myEnemy == null)
    {
      // ... the player is in range.
      playerInRange = true;
      Debug.Log("Enemy in Range");
      myEnemy = other.gameObject;
    }
  }


  void OnTriggerExit(Collider other)
  {
    // If the exiting collider is the player...
    if (myEnemy != null && other.gameObject == myEnemy.gameObject)
    {
      // ... the player is no longer in range.
      playerInRange = false;
      myEnemy = null;
    }
  }

  void Attack()
  {
    Debug.Log("Fire");
    animator.SetTrigger("Attack");

    if (myEnemy != null)
    {
      myEnemy.GetComponent<EnemyHealth>().TakeDamage(100, new Vector3(0, 0, 0));
    }
  }
}
