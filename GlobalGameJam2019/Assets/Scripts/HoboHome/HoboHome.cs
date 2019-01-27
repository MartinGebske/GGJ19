using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventorySystem))]
public class HoboHome : MonoBehaviour
{
    public HouseStats stats;
    public InventorySystem inventory;
    public Transform visual;

    [SerializeField]
    private List<HouseUpgrade> upgrades = new List<HouseUpgrade>();

    public bool canUpgrade
    {
        get
        {
            return upgrades[0].IsAffordable(inventory);
        }
    }

    public HouseUpgrade nextUpgrade
    {
        get
        {
            if ((upgrades.Count == 0)) {
                return null;
            }
            return upgrades[0];
        }
    }

    public bool Upgrade()
    {
        if (upgrades.Count==0) {
            Debug.LogWarning("No more upgrades Avaliable");
            return false;
        }
        if (!canUpgrade) {
            Debug.LogError("Cant afford to Upgrade your Home to "+upgrades[0]);
            return false;
        }
        
        stats = upgrades[0].Buy(inventory);
        Destroy(visual.gameObject);
        visual = Instantiate(upgrades[0].visualPrefab,transform).transform;
        upgrades.RemoveAt(0);
        return true;
    }
}