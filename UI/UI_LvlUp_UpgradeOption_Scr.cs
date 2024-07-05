using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UI_LvlUp_UpgradeOption_Scr : MonoBehaviour, IPointerClickHandler
{
    public int bonusNum; // TEMP
    public float bonusAmount = 1.5f; // TEMP
    public UpgradeOption_SO upgradeOptionSO;

    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescriptionText;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");

        UpgradeSystem_Scr.instance.upgrade(bonusNum);
        UpgradeSystem_Scr.instance.CloseLvlUpMenu();
    }

    public void UpdateVisuals()
    {
        upgradeDescriptionText.text = string.Format(upgradeOptionSO.upgradeDesription, upgradeOptionSO.value_1, upgradeOptionSO.value_2, upgradeOptionSO.value_3);
        upgradeNameText.text = upgradeOptionSO.upgradeName;
    }
}
