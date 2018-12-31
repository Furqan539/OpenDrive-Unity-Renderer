//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2016 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RCC_AICarController)), CanEditMultipleObjects]
public class RCC_AIEditor : Editor {

	RCC_AICarController aiController;

	Texture2D AIIcon;

	void Awake () {

		AIIcon = Resources.Load("Editor/AIIcon", typeof(Texture2D)) as Texture2D;

	}

	[MenuItem("Tools/BoneCracker Games/Realistic Car Controller/AI Controller/Add AI Controller To Vehicle", false)]
	static void CreateAIBehavior(){

		if (!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3> ()) {
			EditorUtility.DisplayDialog("Your Vehicle Has Not RCC_CarControllerV3", "Your Vehicle Has Not RCC_CarControllerV3.", "Ok");
			return;
		}

		if (Selection.activeGameObject.GetComponentInParent<RCC_AICarController> ()) {
			EditorUtility.DisplayDialog("Your Vehicle Already Has AI Car Controller", "Your Vehicle Already Has AI Car Controller", "Ok");
			return;
		}

		Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3> ().gameObject.AddComponent<RCC_AICarController>();
		GameObject vehicle = Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3> ().gameObject;
		Selection.activeGameObject = vehicle;

	}

	[MenuItem("Tools/BoneCracker Games/Realistic Car Controller/AI Controller/Add AI Controller To Vehicle", true)]
	static bool CheckAIBehavior() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}

	[MenuItem("Tools/BoneCracker Games/Realistic Car Controller/AI Controller/Add Waypoints Container To Scene", false)]
	static void CreateWaypointsContainer(){

		GameObject wp = new GameObject("Waypoints Container");
		wp.transform.position = Vector3.zero;
		wp.transform.rotation = Quaternion.identity;
		wp.AddComponent<RCC_AIWaypointsContainer>();
		Selection.activeGameObject = wp;

	}

	[MenuItem("Tools/BoneCracker Games/Realistic Car Controller/AI Controller/Add BrakeZones Container To Scene", false)]
	static void CreateBrakeZonesContainer(){

		if(GameObject.FindObjectOfType<RCC_AIBrakeZonesContainer>() == null){

			GameObject bz = new GameObject("Brake Zones Container");
			bz.transform.position = Vector3.zero;
			bz.transform.rotation = Quaternion.identity;
			bz.AddComponent<RCC_AIBrakeZonesContainer>();
			Selection.activeGameObject = bz;

		}else{
			EditorUtility.DisplayDialog("Your Scene Already Has Brake Zones Container", "Your Scene Already Has Brake Zones", "Ok");
		}

	}

	public override void OnInspectorGUI () {

		serializedObject.Update();

		aiController = (RCC_AICarController)target;

		aiController.gameObject.GetComponent<RCC_CarControllerV3>().externalController = true;

		//		if(aiController.gameObject.GetComponent<RCC_CarControllerV3>().canEngineStall)
		//			aiController.gameObject.GetComponent<RCC_CarControllerV3>().canEngineStall = false;
		//
		//		if(!aiController.gameObject.GetComponent<RCC_CarControllerV3>().autoReverse)
		//			aiController.gameObject.GetComponent<RCC_CarControllerV3>().autoReverse = true;

		EditorGUILayout.Separator();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Box(AIIcon, GUILayout.ExpandWidth(true));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Separator();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("_AIType"), new GUIContent("AI Type", "AI Type."), false);

		EditorGUI.indentLevel++;

		if (aiController._AIType == RCC_AICarController.AIType.FollowWaypoints) {
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("waypointsContainer"), new GUIContent ("Waypoints Container", "Waypoints Container."), false);
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("currentWaypoint"), new GUIContent ("Current Waypoint", "Current Waypoint."), false);
		} else {
			//EditorGUILayout.PropertyField (serializedObject.FindProperty ("targetChase"), new GUIContent ("Target For Chase", "Target For Chase."), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("targetTag"), new GUIContent("Target Tag For Chase", "Target Tag For Chase."), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("detectorRadius"), new GUIContent("Detector Radius."), false);
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("obstacleLayers"), new GUIContent("Obstacle Layers", "Obstacle Layers For Avoid Dynamic Objects."), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("wideRayLength"), new GUIContent("Wide Ray Distance", "Wide Rays For Avoid Dynamic Objects."), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("tightRayLength"), new GUIContent("Tight Ray Distance", "Tight Rays For Avoid Dynamic Objects."), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("sideRayLength"), new GUIContent("Side Ray Distance", "Side Rays For Avoid Dynamic Objects."), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("limitSpeed"), new GUIContent("Limit Speed", "Limits The Speed."), false);

		if(aiController.limitSpeed)
			EditorGUILayout.Slider(serializedObject.FindProperty("maximumSpeed"), 0f, aiController.GetComponent<RCC_CarControllerV3>().maxspeed);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("smoothedSteer"), new GUIContent("Smooth Steering", "Smooth Steering."), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("nextWaypointPassRadius"), new GUIContent("Next Waypoint Pass Radius", "If vehicle gets closer then this radius, goes to next waypoint."), false);

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Current Waypoint: ", aiController.currentWaypoint.ToString());
		EditorGUILayout.LabelField("Laps: ", aiController.lap.ToString());
		EditorGUILayout.LabelField("Total Waypoints Passed: ", aiController.totalWaypointPassed.ToString());
		EditorGUILayout.LabelField("Ignoring Waypoint Due To Unexpected Obstacle: ", aiController.ignoreWaypointNow.ToString());
		EditorGUILayout.Separator();

		serializedObject.ApplyModifiedProperties();

	}

}
