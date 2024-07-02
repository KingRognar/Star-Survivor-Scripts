using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem_Scr : MonoBehaviour
{
    public static UpgradeSystem_Scr instance;

    [SerializeField] private float currentExp = 0;
    [SerializeField] private float expForLvl = 10;
    private float multiplierForNextLvl = 1.4f;
    private int currentLvl = 1;

    [SerializeField] private GameObject levelUpMenu;

    [SerializeField] private List<UI_LvlUp_UpgradeOption_Scr> lvlUpOptions;

    private List<Action> upgradesList = new List<Action>(); 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        AddUpgradesToList();
    }

    public void AwardEXP(float expAmount)
    {
        currentExp += expAmount;

        if (currentExp >= expForLvl)
            LevelUp();
    }
    private void LevelUp()
    {
        currentExp -= expForLvl;
        expForLvl *= multiplierForNextLvl;
        currentLvl++;
        OpenLvlUpMenu();
    }

    private void OpenLvlUpMenu()
    {
        Time.timeScale = 0;
        levelUpMenu.SetActive(true);

        foreach (UI_LvlUp_UpgradeOption_Scr lvlUpOtion in lvlUpOptions)
        {
            lvlUpOtion.bonusNum = UnityEngine.Random.Range(0, upgradesList.Count);
            lvlUpOtion.UpdateVisuals();
        }
    }
    public void CloseLvlUpMenu()
    {
        Time.timeScale = 1;
        levelUpMenu.SetActive(false);
    }


    public void upgrade(int option)
    {
        upgradesList[option]();
    }


    private void AddUpgradesToList()
    {
        upgradesList.Clear();
        upgradesList.Add(IncreasePlayerAttackSpeed);
        upgradesList.Add(IncreasePlayerSpread);
        upgradesList.Add(IncreasePlayerHP);
        upgradesList.Add(IncreasePlayerArmor);
    }
    private void IncreasePlayerSpread()
    {
        Player_Stats_Scr.machineGun.bulletSpreadAngle *= 1.6f;
    }
    private void IncreasePlayerAttackSpeed()
    {
        Player_Stats_Scr.machineGun.bulletSpawnDelay *= 0.8f;
    }
    private void IncreasePlayerHP()
    {
        Player_Stats_Scr.ship.hp = (int)(Player_Stats_Scr.ship.hp * 1.2f);
    }
    private void IncreasePlayerArmor()
    {
        Player_Stats_Scr.ship.armor += 2;
    }    
}
