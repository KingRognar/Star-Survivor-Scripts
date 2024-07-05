using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeOption_SO : ScriptableObject
{
    protected UpgradeSystem_Scr upgradeSystem;
    protected Player_Stats_Scr playerStats;

    public string upgradeName;
    public string upgradeDesription;

    private void Start()
    {
        upgradeSystem = UpgradeSystem_Scr.instance;
        playerStats = Player_Stats_Scr.instance;
    }

    public virtual void UpgradeAction() { }

}
