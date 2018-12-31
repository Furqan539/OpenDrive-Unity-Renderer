using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(BCG_EnterExitSettings))]
public class BCG_EnterExitSettingsEditor : Editor {

	public bool EnableEnterExit{
		
		get{
			
			bool _bool = BCG_EnterExitSettings.Instance.enableEnterExit;
			return _bool;

		}

		set{
			
			bool _bool = BCG_EnterExitSettings.Instance.enableEnterExit;

			if(_bool == value)
				return;

			BCG_EnterExitSettings.Instance.enableEnterExit = value;

			foreach (BuildTargetGroup buildTarget in Enum.GetValues(typeof(BuildTargetGroup))) {
				
				if(buildTarget != BuildTargetGroup.Unknown)
					SetScriptingSymbol("BCG_ENTEREXIT", buildTarget, value);
				
			}

		}

	}

	public override void OnInspectorGUI () {

		serializedObject.Update();

		EnableEnterExit = EditorGUILayout.ToggleLeft(new GUIContent("Enable Enter Exit", "It will enable Enter Exit support for all BCG vehicles."), EnableEnterExit);

		if (!EnableEnterExit)
			return;

		EditorGUILayout.PropertyField(serializedObject.FindProperty("enterExitVehicleKB"), new GUIContent("Enter Exit Vehicle"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("keepEnginesAlive"), new GUIContent("Keep Engines Alive"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("enterExitSpeedLimit"), new GUIContent("Enter Exit Speed Limit"));

		EditorGUILayout.LabelField("BCG Enter Exit  " + BCG_EnterExitSettings.Instance.BCGVersion + " \nBoneCracker Games", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));

		EditorGUILayout.LabelField("Created by Buğra Özdoğanlar", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));

		serializedObject.ApplyModifiedProperties();

	}

	void SetScriptingSymbols(string symbol, bool isActivate){

		SetScriptingSymbol(symbol, BuildTargetGroup.Android, isActivate);

		#if UNITY_5 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3_OR_NEWER
		SetScriptingSymbol(symbol, BuildTargetGroup.iOS, isActivate);
		#else
		SetScriptingSymbol(symbol, BuildTargetGroup.iPhone, isActivate);
		#endif

	}

	void SetScriptingSymbol(string symbol, BuildTargetGroup target, bool isActivate){

		if(target == BuildTargetGroup.Unknown)
			return;

		var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);

		s = s.Replace(symbol + ";","");

		s = s.Replace(symbol,"");

		if(isActivate)
			s = symbol + ";" + s;

		PlayerSettings.SetScriptingDefineSymbolsForGroup(target,s);

	}

}
