using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public int attackDamage = 100;
    AudioSource trapAudio;
    public AudioClip trapClip;

    // Start is called before the first frame update
    void Awake()
    {
        trapAudio = GetComponent<AudioSource>();
    }

     void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDamage,new Vector3(0,0,0));

            if (trapClip)
            {
                trapAudio.clip = trapClip;
                trapAudio.Play();
            }


            Destroy(gameObject, 1f);
        }
    }


}
