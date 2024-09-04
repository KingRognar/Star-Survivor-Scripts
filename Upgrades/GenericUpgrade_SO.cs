using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Upgrade", menuName = "Scriptable Objects/New Upgrade", order = 2)]
public class GenericUpgrade_SO : ScriptableObject
{
    [SerializeField] private string stat1Name;
    [SerializeField] private int stat1ValueChange;

    public void UpgradeAction()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Type type = Player_Stats_Scr.instance.GetType();
            FieldInfo field = type.GetField(stat1Name, BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Log(field.GetValue(Player_Stats_Scr.instance));

        }
    }
}
