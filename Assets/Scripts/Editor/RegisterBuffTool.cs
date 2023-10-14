using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    [CustomEditor(typeof(AddBuffTool)),CanEditMultipleObjects]
    public class RegisterBuffTool:Editor
    {
        public override void OnInspectorGUI()
        {
            AddBuffTool myScript = target as AddBuffTool;
            DrawDefaultInspector();
            EditorGUILayout.Space();
            GUIStyle style = new GUIStyle();
            style.fontSize = 14;
            style.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("注册BUFF", style);
           
            if (GUILayout.Button("注册BUFF"))
            {
                Undo.RecordObject(myScript, "注册BUFF");
                myScript.RegisterBuff();
                EditorUtility.SetDirty(myScript);
            }
            EditorGUILayout.Space();
        }
    }

}