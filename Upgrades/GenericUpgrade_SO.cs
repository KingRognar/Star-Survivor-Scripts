using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Scriptable Objects/New Upgrade", order = 3)]
public class GenericUpgrade_SO : ScriptableObject
{
    [SerializeField] private string stat1Name;
    [SerializeField] private int stat1ValueChange;
    public UnityEvent upgradeEvent = new UnityEvent();
    private UnityAction newAction;

    public void UpgradeAction()
    {
        //newAction = new UnityAction(Weapon_SnakeDrone_Scr.instance.AddTailSegments);
        upgradeEvent.AddListener(newAction);

        Type type = Player_Stats_Scr.instance.GetType();
        FieldInfo field = type.GetField(stat1Name, BindingFlags.NonPublic | BindingFlags.Instance);
        Debug.Log(stat1Name + " value is " + field.GetValue(Player_Stats_Scr.instance));

        upgradeEvent.Invoke();
    }
}
