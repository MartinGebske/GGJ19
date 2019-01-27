using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventorySystem))]
public class HoboHome : MonoBehaviour
{
    public HouseStats stats;
    public InventorySystem inventory;
    public Transform visual;
    public CostUI costUI;
    public GameObject deathScreen;

    [SerializeField]
    private List<HouseUpgrade> upgrades = new List<HouseUpgrade>();

    private void Start()
    {
        costUI.UpdateUI(nextUpgrade.stats);
        deathScreen.SetActive(false);
    }

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

        if (nextUpgrade!=null) {
            costUI.UpdateUI(nextUpgrade.stats);
        } else {
            costUI.UpdateUI(new HouseStats());
        }

        return true;
    }

    public void TakeDamage(float damage)
    {
        stats.HP -= damage / stats.defense;

        if(stats.HP <= 0) {
            deathScreen.SetActive(true);
        }
    }
}