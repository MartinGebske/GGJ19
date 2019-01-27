using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;



public class PlayerAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public List<EnemyHealth> enemysInRange = new List<EnemyHealth>();
    public GameObject NewsPaper;
    bool playerInRange
    {
        get
        {
            return enemysInRange.Count > 0;
        }
    }
    DateTime nextAttack = DateTime.Now;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    #if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.F)) {
            this.Attack();
        }
    }
        #endif

    void OnTriggerEnter(Collider col)
    {
        // TODO handle case when multiple enemies are in range
        // If the entering collider is the player...
        if (col.gameObject.tag == "Enemy") {
            // ... the player is in range.
            Debug.Log("Enemy in Range");
            enemysInRange.Add(col.GetComponent<EnemyHealth>());
        }
    }


    void OnTriggerExit(Collider col)
    {
        EnemyHealth toCheck = col.GetComponent<EnemyHealth>();
        if (toCheck!=null && enemysInRange.Contains(toCheck)) {
            enemysInRange.Remove(toCheck);
        }
    }

    public bool Attack()
    {
        if (nextAttack > DateTime.Now || !playerInRange) {
            return false;
        }
        Debug.Log("Fire");
        animator.SetTrigger("Attack");
        StartCoroutine(showNewspaper());

        enemysInRange[0].TakeDamage(100, new Vector3(0, 0, 0));
        return true;
    }

    IEnumerator showNewspaper()
    {
        NewsPaper.SetActive(true);
        yield return new WaitForSeconds(2f);
        NewsPaper.SetActive(false);
    }
}
