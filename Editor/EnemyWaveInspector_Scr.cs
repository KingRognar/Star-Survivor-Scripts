using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyWave_SO))]
public class EnemyWaveInspector_Scr : Editor
{
    int count;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        Display();
    }

    private void Display()
    {
        serializedObject.Update();


        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

        SerializedProperty waveDuration = serializedObject.FindProperty("waveDuration");
        EditorGUILayout.PropertyField(waveDuration, new GUIContent("Wave Duration (in seconds)"));
        EditorGUILayout.Space(20f);

        SerializedProperty enemy = serializedObject.FindProperty("enemiesList");
        SerializedProperty enemyNum = serializedObject.FindProperty("numberOfEnmemiesPerType");
        count = enemy.arraySize;

        //---// ������ ��� ����������\���������� ������
        EditorGUILayout.LabelField("Enemy Types and their total number", style);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+", GUILayout.MinWidth(80f)))
        {
            AddElements();
        }
        //GUILayout.FlexibleSpace();

        EditorGUILayout.LabelField(count.ToString(), style, GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
        //GUILayout.FlexibleSpace();
        if (GUILayout.Button("-", GUILayout.MinWidth(80f)))
        {
            RemoveElements();
        }
        EditorGUILayout.EndHorizontal();

        //---// ������ ������ � �� ����������
        for (int i = 0; i < count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(enemy.GetArrayElementAtIndex(i), GUIContent.none);
            EditorGUILayout.PropertyField(enemyNum.GetArrayElementAtIndex(i), GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        //EditorGUILayout.PropertyField(enemy);
        //EditorGUILayout.PropertyField(enemyNum);


        serializedObject.ApplyModifiedProperties();
    }

    void AddElements()
    {
        SerializedProperty enemy = serializedObject.FindProperty("enemiesList");
        SerializedProperty enemyNum = serializedObject.FindProperty("numberOfEnmemiesPerType");
        enemy.arraySize++;
        enemyNum.arraySize++;
        count++;
    }
    void RemoveElements()
    {
        SerializedProperty enemy = serializedObject.FindProperty("enemiesList");
        SerializedProperty enemyNum = serializedObject.FindProperty("numberOfEnmemiesPerType");
        if (enemy.arraySize > 0)
        enemy.arraySize--;
        if (enemyNum.arraySize > 0)
        enemyNum.arraySize--;
        count = Mathf.Max(enemy.arraySize, enemyNum.arraySize);
    }
}
