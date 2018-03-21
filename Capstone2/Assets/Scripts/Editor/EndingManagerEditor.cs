using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EndingManager))]
public class EndingManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EndingManager script = target as EndingManager;
        if(GUILayout.Button("Reset Skybox Color"))
        {
            script.skybox.SetColor("_Tint", script.initialColor/255);
        }
    }
}
