using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Scriptable Objects/New Enemy Wave", order = 3)]
public class EnemyWave_SO : ScriptableObject
{
    public float waveDuration;
    public List<GameObject> enemiesList;
    public List<int> numberOfEnmemiesPerType;
}
