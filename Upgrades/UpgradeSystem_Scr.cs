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
    [SerializeField] private List<UI_LvlUp_UpgradeOption_Scr> UIlvlUpOptions;

    //public List<UpgradeOption_SO> upgradesList = new List<UpgradeOption_SO>();
    public List<GenericUpgrade_SO> upgradesList = new List<GenericUpgrade_SO>();

    //[SerializeField] private GenericUpgrade_SO genericUpgrade;

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        AddUpgradesToList();
    }
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            genericUpgrade.UpgradeAction();
    }*/


    /// <summary>
    /// ћетод начисл€ющий очки опыта
    /// </summary>
    /// <param name="expAmount"> оличество добовл€емых очков опыта</param>
    public void AwardEXP(int expAmount)
    {
        currentExp += expAmount;
        expBarUI.UpdateEXPBar(currentExp, expForLvl);

        if (currentExp >= expForLvl) // TODO: поставить луп и вс€кое такое, чтобы при избытке опыта можно получить несколько уровней последовательно
            LevelUp();
    }
    /// <summary>
    /// ћетод, выполн€емый при повышении уровн€
    /// </summary>
    private void LevelUp()
    {
        currentExp -= expForLvl;
        expForLvl = (int)(multiplierForNextLvl*expForLvl);
        currentLvl++;
        OpenLvlUpMenu();
    }

    /// <summary>
    /// ћетод, открывающий меню выбора улучшений
    /// </summary>
    private void OpenLvlUpMenu()
    {
        if (upgradesList.Count == 0)
            CloseLvlUpMenu();

        Time.timeScale = 0;
        levelUpMenu.SetActive(true);


        int cnt = 3;
        if (upgradesList.Count < 3)
            cnt = upgradesList.Count;

        int i = 0;
        while (i < 3)
        {
            int num = UnityEngine.Random.Range(0, upgradesList.Count);
            UIlvlUpOptions[i].upgrade_SO = upgradesList[num];
            UIlvlUpOptions[i].UpdateVisuals();
            if (i + 1 > cnt)
                UIlvlUpOptions[i].gameObject.SetActive(false);

            i++;
        }
    }
    /// <summary>
    /// ћетод, закрывающий меню выбора улучшений
    /// </summary>
    public void CloseLvlUpMenu()
    {
        Time.timeScale = 1;
        expBarUI.UpdateEXPBar(currentExp, expForLvl);
        expBarUI.UpadteLVLtext(currentLvl);
        levelUpMenu.SetActive(false);
    }

    private void AddUpgradesToList() // TODO: придумать метод дл€ добавлени€ ScriptableObjects
    {
        /*upgradesList.Clear();
        upgradesList.Add(IncreasePlayerAttackSpeed);
        upgradesList.Add(IncreasePlayerSpread);
        upgradesList.Add(IncreasePlayerHP);
        upgradesList.Add(IncreasePlayerArmor);*/
    }
}
