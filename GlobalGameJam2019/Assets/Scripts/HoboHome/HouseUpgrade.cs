using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HouseUpgrade
{
    public string name;
    public GameObject visualPrefab;

    [SerializeField]
    public HouseStats stats;

    public bool IsAffordable(InventorySystem scource)
    {
        foreach (CostObject costObject in stats.costs) {
            if (costObject.count > scource.GetItemCount(costObject.itemType)) {
                return false;
            }
        }
        return true;
    }

    public HouseStats Buy(InventorySystem scource)
    {
        if (!IsAffordable(scource)) {
            return null;
        }
        foreach (CostObject costObject in stats.costs) {
            scource.Remove(costObject.itemType, costObject.count);
        }
        return stats;
    }
}