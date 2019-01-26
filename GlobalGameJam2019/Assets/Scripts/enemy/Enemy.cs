using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
public class Enemy : MonoBehaviour
{
  public int StartHealth;

  private AICharacterControl charControl;
  private HoboHome hoboHome;
  private Waypoint[] waypoints;

  private int waypointIdx;
  private int health;

  void Start()
  {
    charControl = GetComponent<AICharacterControl>();

    hoboHome = FindObjectOfType<HoboHome>();
    waypoints = FindObjectsOfType<Waypoint>();

    health = StartHealth;

    this.nextWaypoint();
  }

  void Update()
  {
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

    Destroy(this.gameObject, 1f);
  }
}
