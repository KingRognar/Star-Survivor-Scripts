using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem_Scr : MonoBehaviour
{
    public static UpgradeSystem_Scr instance;

    [SerializeField] private int currentExp = 0;
    [SerializeField] private int expForLvl = 10;
    private float multiplierForNextLvl = 1.4f;
    private int currentLvl = 1;

    [SerializeField] private GameObject levelUpMenu;

    [SerializeField] private UI_EXP_Bar_Scr expBarUI;
    [SerializeField] private List<UI_LvlUp_UpgradeOption_Scr> lvlUpOptions;

    [SerializeField] private List<UpgradeOption_SO> upgradesList = new List<UpgradeOption_SO>(); 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        AddUpgradesToList();
    }

    public void AwardEXP(int expAmount)
    {
        currentExp += expAmount;
        expBarUI.UpdateEXPBar(currentExp, expForLvl);

        if (currentExp >= expForLvl) // TODO: поставить луп и всякое такое, чтобы при избытке опыта можно получить несколько уровней последовательно
            LevelUp();
    }
    private void LevelUp()
    {
        currentExp -= expForLvl;
        expForLvl = (int)(multiplierForNextLvl*expForLvl);
        currentLvl++;
        OpenLvlUpMenu();
    }

    private void OpenLvlUpMenu()
    {
        Time.timeScale = 0;
        levelUpMenu.SetActive(true);

        foreach (UI_LvlUp_UpgradeOption_Scr lvlUpOtion in lvlUpOptions) // TODO: доделать
        {
            int num = UnityEngine.Random.Range(0, upgradesList.Count);
            lvlUpOtion.bonusNum = num;
            lvlUpOtion.upgradeOptionSO = upgradesList[num];
            lvlUpOtion.UpdateVisuals();
        }
    }
    public void CloseLvlUpMenu()
    {
        Time.timeScale = 1;
        expBarUI.UpdateEXPBar(currentExp, expForLvl);
        expBarUI.UpadteLVLtext(currentLvl);
        levelUpMenu.SetActive(false);
    }


    public void upgrade(int option)
    {
        upgradesList[option].UpgradeAction();
    }


    private void AddUpgradesToList() // TODO: придумать метод для добавления ScriptableObjects
    {
        /*upgradesList.Clear();
        upgradesList.Add(IncreasePlayerAttackSpeed);
        upgradesList.Add(IncreasePlayerSpread);
        upgradesList.Add(IncreasePlayerHP);
        upgradesList.Add(IncreasePlayerArmor);*/
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
