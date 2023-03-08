using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterStats))]
public class CharacterStatsEditor : Editor
{
    public CharacterStats StatsTarget => target as CharacterStats;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset Values"))
        {
            StatsTarget.ResetValues();
        }
    }
}
