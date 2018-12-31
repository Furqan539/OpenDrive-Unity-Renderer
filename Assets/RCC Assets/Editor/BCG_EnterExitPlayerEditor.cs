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

[CustomEditor(typeof(BCG_EnterExitPlayer))]
public class BCG_EnterExitPlayerEditor : Editor {

	BCG_EnterExitPlayer prop;


	void OnEnable(){



	}

	public override void OnInspectorGUI (){

		serializedObject.Update();
		prop = (BCG_EnterExitPlayer)target;

		EditorGUILayout.HelpBox ("Script must be attached to root of your character player.", MessageType.Info);
		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("isTPSController"), new GUIContent("Is TPS Controller?"), false);

		if (prop.isTPSController)
			EditorGUILayout.PropertyField (serializedObject.FindProperty("rayHeight"), new GUIContent("Ray Height"), false);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("playerStartsAsInVehicle"), new GUIContent("Player Starts As In Vehicle"), false);

		if (prop.playerStartsAsInVehicle) {
			
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("inVehicle"), new GUIContent ("Vehicle"), false);
			EditorGUI.indentLevel--;

		}

		if (!GameObject.FindObjectOfType<BCG_EnterExitManager> ()) {

			EditorGUILayout.HelpBox ("Your scene doesn't have BCG_EnterExitManager. In order to use enter-exit system, your scene must have _BCGEnterExitManager.", MessageType.Error);

			if (GUILayout.Button ("Create BCG_EnterExitManager")) {

				GameObject newBCG_EnterExitManager = new GameObject ();
				newBCG_EnterExitManager.transform.name = "_BCGEnterExitManager";
				newBCG_EnterExitManager.transform.position = Vector3.zero;
				newBCG_EnterExitManager.transform.rotation = Quaternion.identity;
				newBCG_EnterExitManager.AddComponent<BCG_EnterExitManager> ();

			}

		}

		serializedObject.ApplyModifiedProperties();

		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

}
