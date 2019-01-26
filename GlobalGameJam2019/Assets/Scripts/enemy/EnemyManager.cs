using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  [Header("Assign Prefab")]
  public GameObject EnemyPrefab;

  [Header("Settings")]
  public int MinutesBetweenWaves = 5;
  public int SpawnsPerWave = 3;
  public int MaxEnemiesCount = 50;

  private TimeSystem timeSystem;
  private Waypoint[] waypoints;

  private List<Enemy> enemies = new List<Enemy>();

  void Awake()
  {
    timeSystem = FindObjectOfType<TimeSystem>();
    waypoints = FindObjectsOfType<Waypoint>();
  }

  void Start()
  {
    timeSystem.SubscribeEvent(timeSystem.time.AddMinutes(MinutesBetweenWaves), this.onSpawnWave);
  }

  private void onSpawnWave()
  {
    // Spawn Enemies for this wave
    for (int i = 0; i < SpawnsPerWave; i++)
    {
      if (enemies.Count < MaxEnemiesCount)
      {
        this.SpawnEnemy();
      }
    }

    // Resubscribe to time the next wave
    timeSystem.SubscribeEvent(timeSystem.time.AddMinutes(MinutesBetweenWaves), this.onSpawnWave);
  }

  public void SpawnEnemy()
  {
    var enemy = GameObject.Instantiate(
        EnemyPrefab,
        waypoints[Random.Range(0, waypoints.Length)].transform.position,
        Quaternion.identity
    );
    enemies.Add(enemy.GetComponent<Enemy>());
  }

  public void OnEnemyDed(Enemy enemy)
  {
    enemies.Remove(enemy);
  }
}
