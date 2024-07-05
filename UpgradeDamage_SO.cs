using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Damage", menuName = "ScriptableObjects/Upgrade Damage", order = 1)]
public class UpgradeDamage_SO : UpgradeOption_SO
{
    [SerializeField] private float damageMultiplierIncrease = 0.1f;

    public override void UpgradeAction()
    {
        Player_Stats_Scr.ship.damageMultiplier += damageMultiplierIncrease;
    }
}
