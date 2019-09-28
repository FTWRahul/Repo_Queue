using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropertyHolder))]
public class PropertyDisplay<T> : Editor where T : IWantEditor
{
    public override void OnInspectorGUI()
    {
        //T myTarget = (T)target;

        //T.ReturnAllProperties();
        
       // myTarget.Health = EditorGUILayout.IntField("Health", myTarget.Health);
       // EditorGUILayout.LabelField("Health", myTarget.Health.ToString());
    }
}
