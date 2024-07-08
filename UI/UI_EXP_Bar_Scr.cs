using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_EXP_Bar_Scr : MonoBehaviour
{
    [SerializeField] private Image expBar;
    [SerializeField] private TMP_Text lvlText;

    private void Start()
    {
        UpdateEXPBar(0,10);
        UpadteLVLtext(1);
    }

    public void UpdateEXPBar(int curEXP, int lvlEXP)
    {
        expBar.fillAmount = (float)curEXP / lvlEXP;
    }
    public void UpadteLVLtext(int lvl)
    {
        lvlText.text = lvl.ToString();
    }
}
