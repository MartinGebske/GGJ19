using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnemyMode
{
  WALKING_WAYPOINTS,
  ATTACKING_HOBO_HOME
}

public class EnemyManager : MonoBehaviour
{
  [Header("Assign Prefab")]
  public GameObject EnemyPrefab;
  public int NightTimeStartHour;
  public int NightTimeEndHour;

  [Header("Settings")]
  public int MinutesBetweenWaves = 5;
  public int SpawnsPerWave = 3;
  public int MaxEnemiesCount = 50;

  private Waypoint[] waypoints;

  private EnemyMode enemyMode;
  public EnemyMode EnemyMode { get => enemyMode; private set => enemyMode = value; }

  private List<Enemy> enemies = new List<Enemy>();

  public static EnemyManager pInstance
  {
    get
    {
      if (mInstance == null)
      {
        mInstance = FindObjectOfType<EnemyManager>();
        if (mInstance == null)
        {
          GameObject gO = Instantiate(new GameObject("EnemyManager"));
          mInstance = gO.AddComponent<EnemyManager>();
        }
        DontDestroyOnLoad(mInstance);
      }
      return mInstance;
    }
  }
  private static EnemyManager mInstance;

  void Start()
  {
    waypoints = FindObjectsOfType<Waypoint>();

    // Start spawn waves loop
    TimeSystem.pInstance.SubscribeEvent(
      TimeSystem.pInstance.time.AddMinutes(MinutesBetweenWaves), this.onSpawnWave);

    // Start day/night loop
    TimeSystem.pInstance.SubscribeEvent(new System.DateTime(
      TimeSystem.pInstance.time.Year,
      TimeSystem.pInstance.time.Month,
      TimeSystem.pInstance.time.Day, NightTimeStartHour, 0, 0), this.onStartNight);
  }

  private void onStartNight()
  {
    this.EnemyMode = EnemyMode.ATTACKING_HOBO_HOME;

    // Wait for end of night
    TimeSystem.pInstance.SubscribeEvent(new System.DateTime(
      TimeSystem.pInstance.time.Year,
      TimeSystem.pInstance.time.Month,
      TimeSystem.pInstance.time.Day + 1, NightTimeEndHour, 0, 0), this.onEndNight);
  }

  private void onEndNight()
  {
    this.EnemyMode = EnemyMode.WALKING_WAYPOINTS;

    // Wait for start of night
    TimeSystem.pInstance.SubscribeEvent(new System.DateTime(
      TimeSystem.pInstance.time.Year,
      TimeSystem.pInstance.time.Month,
      TimeSystem.pInstance.time.Day, NightTimeStartHour, 0, 0), this.onStartNight);
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
    TimeSystem.pInstance.SubscribeEvent(
      TimeSystem.pInstance.time.AddMinutes(MinutesBetweenWaves), this.onSpawnWave);
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
