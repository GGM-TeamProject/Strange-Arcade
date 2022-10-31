using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataManager))]
public class DataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Reset Challenge")){
            DataManager.Instance.ResetData();
            Debug.Log("user 데이터가 리셋 되었습니다");
        }
    }
}
