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

[CustomEditor(typeof(BCG_EnterExitManager))]
public class BCG_EnterExitManagerEditor : Editor {

	BCG_EnterExitManager prop;

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Edit Enter-Exit Settings", false, -100)]
	public static void OpenBCGEnterExitSettings(){

		Selection.activeObject = BCG_EnterExitSettings.Instance;

	}

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Add Main Enter-Exit Manager To Scene", false)]
	public static void CreateEnterExitManager(){

		if(GameObject.FindObjectOfType<BCG_EnterExitManager>()){

			EditorUtility.DisplayDialog("Scene has _BCGEnterExitManager already!", "Scene has _BCGEnterExitManager already!", "Ok");

		}else{
			
			GameObject newBCG_EnterExitManager = new GameObject ();
			newBCG_EnterExitManager.transform.name = "_BCGEnterExitManager";
			newBCG_EnterExitManager.transform.position = Vector3.zero;
			newBCG_EnterExitManager.transform.rotation = Quaternion.identity;
			newBCG_EnterExitManager.AddComponent<BCG_EnterExitManager> ();

			Selection.activeGameObject = newBCG_EnterExitManager;

		}

	}

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Add Enter-Exit To Vehicle", false)]
	public static void CreateEnterExitVehicle(){

		if (Selection.activeGameObject == null) {
			EditorUtility.DisplayDialog ("Select your vehicle on your scene, and then come back again!", "Select your vehicle on your scene, and then come back again.", "Ok");
			return;
		}

		if (Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>() == null) {
			EditorUtility.DisplayDialog ("Selected vehicle doesn't have RCC_CarControllerV3!", "Selected vehicle doesn't have RCC_CarControllerV3! You must have a running vehicle before the Enter-Exit System.", "Ok");
			return;
		}

		if(Selection.activeGameObject.GetComponentInParent<BCG_EnterExitVehicle>()){

			EditorUtility.DisplayDialog("Selected vehicle has BCG_EnterExitVehicle already!", "Selected vehicle has BCG_EnterExitVehicle already!", "Ok");

		}else{

			Selection.activeGameObject.AddComponent<BCG_EnterExitVehicle> ();

		}

	}

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Add Enter-Exit To FPS Player", false)]
	public static void CreateEnterExitPlayer(){

		if (Selection.activeGameObject == null) {
			EditorUtility.DisplayDialog ("Select your FPS player on your scene, and then come back again!", "Select your FPS player on your scene, and then come back again.", "Ok");
			return;
		}

		if(Selection.activeGameObject.GetComponentInParent<BCG_EnterExitPlayer>()){

			EditorUtility.DisplayDialog("Selected FPS Player has BCG_EnterExitPlayer already!", "Selected FPS Player has BCG_EnterExitPlayer already!", "Ok");

		}else{
			
			Selection.activeGameObject.AddComponent<BCG_EnterExitPlayer> ();

		}

	}

	public override void OnInspectorGUI (){

		serializedObject.Update();
		prop = (BCG_EnterExitManager)target;

		EditorGUILayout.HelpBox ("General event based enter exit system for all vehicles created by BCG.", MessageType.Info);

		EditorGUILayout.LabelField ("Debug", EditorStyles.boldLabel);
		EditorGUILayout.Space ();

		EditorGUI.BeginDisabledGroup (true);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("BCGCharacterPlayer"), new GUIContent("BCG Character Player"), true);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("cachedMainCameras"), new GUIContent("Cached BCG Main Cameras"), true);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("cachedPlayers"), new GUIContent("Cached BCG Character Players"), true);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("cachedVehicles"), new GUIContent("Cached BCG Vehicles"), true);

		EditorGUI.EndDisabledGroup ();

		if(EditorApplication.isPlaying && prop.cachedMainCameras != null && prop.cachedMainCameras.Count == 0)
			EditorGUILayout.HelpBox ("One main camera needed at least.", MessageType.Error);

		serializedObject.ApplyModifiedProperties();

		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

}
