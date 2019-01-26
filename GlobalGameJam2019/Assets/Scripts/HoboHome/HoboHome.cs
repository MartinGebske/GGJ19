using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventorySystem))]
public class HoboHome : MonoBehaviour
{
    public HouseStats stats;
    public InventorySystem inventory;

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

    public void Upgrade()
    {
        if (upgrades.Count==0) {
            Debug.LogWarning("No more upgrades Avaliable");
        }
        if (!canUpgrade) {
            Debug.LogError("Cant afford to Upgrade your Home to "+upgrades[0]);
            return;
        }

        stats = upgrades[0].Buy(inventory);
        upgrades.RemoveAt(0);
    }
}