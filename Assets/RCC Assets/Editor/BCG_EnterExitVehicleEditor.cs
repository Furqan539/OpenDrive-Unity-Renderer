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

[CustomEditor(typeof(BCG_EnterExitVehicle))]
public class BCG_EnterExitVehicleEditor : Editor {

	BCG_EnterExitVehicle prop;

	public override void OnInspectorGUI (){

		serializedObject.Update();
		prop = (BCG_EnterExitVehicle)target;

		EditorGUILayout.HelpBox ("Script must be attached to root of your vehicle.", MessageType.Info);
		EditorGUILayout.HelpBox ("Select proper camera for each vehicle. Selecting RCC Camera for RCC vehicles, selecting RTC Camera for RTC vehicles, etc...", MessageType.Info);
		EditorGUILayout.Space ();

		if (!GameObject.FindObjectOfType<BCG_EnterExitManager> ()) {

			EditorGUILayout.HelpBox ("Your scene doesn't have BCG_EnterExitManager. In order to use enter-exit system, your scene must have _BCGEnterExitManager.", MessageType.Error);

			if (GUILayout.Button ("Create BCG_EnterExitManager")) {

				GameObject newBCG_EnterExitManager = new GameObject ();
				newBCG_EnterExitManager.transform.name = "BCG_EnterExitManager";
				newBCG_EnterExitManager.transform.position = Vector3.zero;
				newBCG_EnterExitManager.transform.rotation = Quaternion.identity;
				newBCG_EnterExitManager.AddComponent<BCG_EnterExitManager> ();

			}

		} else {

			EditorGUI.BeginDisabledGroup (true);
			EditorGUILayout.PropertyField (serializedObject.FindProperty("driver"), new GUIContent("Current Driver"), false);
			EditorGUI.EndDisabledGroup ();

			prop.correspondingCamera = (GameObject)EditorGUILayout.ObjectField ("Corresponding Camera", prop.correspondingCamera, typeof(GameObject), true);
			EditorGUILayout.PropertyField (serializedObject.FindProperty("getOutPosition"), new GUIContent("Get Out Position"), false);

		}
			
		serializedObject.ApplyModifiedProperties();

		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

}
