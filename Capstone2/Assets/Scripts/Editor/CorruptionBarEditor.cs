using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CorruptionBar))]
public class CorruptionBarEditor : Editor {


	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		/*
		serializedObject.Update();
		SerializedProperty prop = serializedObject.FindProperty("m_Script");
		EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
		serializedObject.ApplyModifiedProperties();

		CorruptionBar script = target as CorruptionBar;

		EditorGUILayout.FloatField("Current Corruption", script.CurrentCorruption, GUILayout.Width(350));
		EditorGUILayout.FloatField("Max Corruption", script.MaxCorruption, GUILayout.Width(350));
		EditorGUILayout.FloatField("Health Decay Rate", script.HealthDecayRate, GUILayout.Width(350));
		EditorGUILayout.FloatField("Time Till Decay Amplify", script.TimeTillDecayAmplify, GUILayout.Width(350));
		
		EditorGUILayout.MinMaxSlider(new GUIContent("Percent Till Decay"), ref script.MinPercentTillDecay, ref script.MaxPercentTillDecay, 0, 1, GUILayout.Width(350));
		

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Min: " + Mathf.Round(script.MinPercentTillDecay * 100f) / 100f);
		EditorGUILayout.LabelField("Max: " + Mathf.Round(script.MaxPercentTillDecay * 100f) / 100f);
		EditorGUILayout.EndHorizontal();
		*/


	}
}
