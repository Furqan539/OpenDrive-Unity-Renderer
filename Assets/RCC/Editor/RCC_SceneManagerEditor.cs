//----------------------------------------------
//            Realistic Tank Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RCC_SceneManager))]
public class RCC_SceneManagerEditor : Editor {

	RCC_SceneManager prop;

	public override void OnInspectorGUI (){

		serializedObject.Update();
		prop = (RCC_SceneManager)target;

		EditorGUILayout.HelpBox ("Scene manager that contains current player vehicle, current player camera, current player UI, current player character, recording/playing mechanim, and other vehicles as well.", MessageType.Info);
		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("registerFirstVehicleAsPlayer"), new GUIContent("Register First Vehicle As Player"), false);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("disableUIWhenNoPlayerVehicle"), new GUIContent("Disable UI When No Player Vehicle"), false);

		EditorGUILayout.LabelField ("Debug", EditorStyles.boldLabel);
		EditorGUILayout.Space ();

		EditorGUI.BeginDisabledGroup (true);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("activePlayerVehicle"), new GUIContent("Active Player Vehicle"), false);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("activePlayerCamera"), new GUIContent("Active Player Camera"), false);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("activePlayerCanvas"), new GUIContent("Active Player UI Canvas"), false);
		#if BCG_ENTEREXIT
		EditorGUILayout.PropertyField (serializedObject.FindProperty("activePlayerCharacter"), new GUIContent("Active Player FPS / TPS Character"), false);
		#endif
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (serializedObject.FindProperty("recordMode"), new GUIContent("Record Mode"), false);
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (serializedObject.FindProperty("allVehicles"), new GUIContent("All Vehicles"), true);
		EditorGUILayout.Space ();

		EditorGUI.EndDisabledGroup ();

		serializedObject.ApplyModifiedProperties();

		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

}
