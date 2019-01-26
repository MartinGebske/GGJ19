using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    public SpawnItemObject[] initSpawnItemObjects;
    private List<InventoryObject> spawnItemObjects = new List<InventoryObject>();
    public float spawnByDayChance = 0.5f;
    public InventoryObject child;

    void Awake()
    {
        foreach (SpawnItemObject item in initSpawnItemObjects) {
            for (int i = 0; i < item.priority; i++) {
                spawnItemObjects.Add(item.item);
            }
        }
    }

    public bool spawnRandomObject()
    {
        if (child == null && spawnByDayChance>Random.Range(0,1)) {
            child = Instantiate<InventoryObject>(getRandomObject(),transform);
            return true;
        }
        return false;
    }

    public InventoryObject take()
    {
        InventoryObject output = child;
        child = null;
        return output;
    }

    private InventoryObject getRandomObject()
    {
        return spawnItemObjects[Random.Range(0, spawnItemObjects.Count - 1)];
    }
}
