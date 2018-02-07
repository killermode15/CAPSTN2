using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(iTweenPathMoveable))]
public class iTweenPathMoveableEditor : Editor
{

	private static GUIStyle ToggleButtonStyleNormal = null;
	private static GUIStyle ToggleButtonStyleToggled = null;

	private bool toggled;

	public override void OnInspectorGUI()
	{
		iTweenPathMoveable script = target as iTweenPathMoveable;
		GUI.enabled = false;
		serializedObject.Update();
		SerializedProperty prop = serializedObject.FindProperty("m_Script");
		EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
		serializedObject.ApplyModifiedProperties();
		GUI.enabled = true;

		if (ToggleButtonStyleNormal == null)
		{
			ToggleButtonStyleNormal = "Button";
			ToggleButtonStyleToggled = new GUIStyle(ToggleButtonStyleNormal);
			ToggleButtonStyleToggled.normal.background = ToggleButtonStyleToggled.active.background;
		}

		script.PathOffset = EditorGUILayout.Vector3Field("Offset", script.PathOffset);
		script.EnableMove = EditorGUILayout.Toggle("Enable Movement", script.EnableMove);

		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Edit Nodes", script.CanChangeNodePositions ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
		{
			script.CanChangeNodePositions = !script.CanChangeNodePositions;
		}

		if (GUILayout.Button("Save Nodes"))
		{

			script.originalNodes = new List<Vector3>();
			for (int i = 0; i < script.path.nodeCount; i++)
			{
				script.originalNodes.Add(script.path.nodes[i]);
			}
		}

		if (GUILayout.Button("Get Current Nodes"))
		{
			for (int i = 0; i < script.path.nodeCount; i++)
			{
				script.pathNodes[i] = script.originalNodes[i];
			}
		}
		/*
		if(GUILayout.Button("Get Saved Nodes"))
		{
			for (int i = 0; i < script.path.nodeCount; i++)
			{
				script.pathNodes[i] = script.originalNodes[i];
			}
			//script.path.nodes = script.originalNodes;
		}*/

		GUILayout.EndHorizontal();
	}

}
