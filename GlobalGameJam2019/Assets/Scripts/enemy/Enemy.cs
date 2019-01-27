using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
public class Enemy : MonoBehaviour
{
  public int StartHealth;

  private Animator animator;
  private AICharacterControl charControl;
  private HoboHome hoboHome;
  private Waypoint[] waypoints;
  private EnemyHealth myHealth;

  private int waypointIdx;
  private int health;
  private bool isAttacking;

  void Start()
  {
    myHealth = GetComponent<EnemyHealth>();
    charControl = GetComponent<AICharacterControl>();
    animator = GetComponent<Animator>();

    hoboHome = FindObjectOfType<HoboHome>();
    waypoints = FindObjectsOfType<Waypoint>();

    health = StartHealth;

    this.nextWaypoint();
  }

  void Update()
  {
    if (myHealth.IsDead) return;

    if (EnemyManager.pInstance.EnemyMode == EnemyMode.WALKING_WAYPOINTS)
    {
      if (Vector3.Distance(charControl.target.position, transform.position) < 5f)
      {
        this.nextWaypoint();
      }
    }
    else
    {
      if (charControl.target != hoboHome.transform)
      {
        this.SetTarget(hoboHome.transform);
      }
      if (!isAttacking && Vector3.Distance(charControl.target.position, transform.position) < 2f)
      {
        StartCoroutine(Attack());
      }
    }
  }

  IEnumerator Attack()
  {
    if (!myHealth.IsDead)
    {
      isAttacking = true;
      animator.SetTrigger("Attack");
      hoboHome.TakeDamage(2f);
      yield return new WaitForSeconds(3f);
      isAttacking = false;
    }
  }

  private void nextWaypoint()
  {
    waypointIdx = Random.Range(0, waypoints.Length);
    this.SetTarget(waypoints[waypointIdx].transform);
  }

  public void SetTarget(Transform target)
  {
    charControl.target = target;
  }

  public void TakeDamage(int damage = 1)
  {
    health -= damage;
    if (health <= 0)
    {
      this.die();
    }
  }

  private void die()
  {
    EnemyManager.pInstance.OnEnemyDed(this);
    // TODO play die animation
    this.GetComponent<Animator>().SetTrigger("Death");

    Destroy(this.gameObject, 4f);
  }
}
