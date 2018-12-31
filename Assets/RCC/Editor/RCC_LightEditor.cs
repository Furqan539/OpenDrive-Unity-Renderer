//----------------------------------------------
//            Realistic Car Controller
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

[CustomEditor(typeof(RCC_Light)), CanEditMultipleObjects]
public class RCC_LightEditor : Editor {

	RCC_Light prop;

	Color originalGUIColor;

	public override void OnInspectorGUI (){

		originalGUIColor = GUI.color;
		serializedObject.Update();
		prop = (RCC_Light)target;

		CheckLights ();

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("RCC lights will receive inputs from parent car controller and adjusts intensity for lights. You can choose which type of light you want to use below. You won't need to specify left or right indicator lights.", EditorStyles.helpBox);
		EditorGUILayout.LabelField("''Important'' or ''Not Important'' modes (Pixel or Vertex) overrided by RCC_Settings.", EditorStyles.helpBox);
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("lightType"), new GUIContent("Light Type"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("inertia"), new GUIContent("Inertia"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("flare"), new GUIContent("Lens Flare"), false);

		if (!prop.GetComponent<LensFlare> ()) {
			
			if (GUILayout.Button ("Create LensFlare")) {

				GameObject[] lights = Selection.gameObjects;

				for (int i = 0; i < lights.Length; i++) {

					if (lights [i].GetComponent<LensFlare> ())
						break;

					lights[i].AddComponent<LensFlare> ();
					LensFlare lf = lights[i].GetComponent<LensFlare> ();
					lf.brightness = 0f;
					lf.color = Color.white;
					lf.fadeSpeed = 20f;

				}

			}
			
		} else {

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("RCC uses ''Interpolation'' mode for all rigidbodies. Therefore, lights at front of the vehicle will blink while on high speeds. To fix this, select your RCC layer in LensFlare component as ignored layer. RCC_Light script will simulate lens flares depending on camera distance and angle.''.", EditorStyles.helpBox);
			EditorGUILayout.Space();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("flareBrightness"), new GUIContent("Flare Brightness"), false);

		}
			
		EditorGUILayout.Space();

		serializedObject.ApplyModifiedProperties();
		
		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

	void CheckLights(){

		if (!prop.gameObject.activeInHierarchy)
			return;

		Vector3 relativePos = prop.GetComponentInParent<RCC_CarControllerV3>().transform.InverseTransformPoint (prop.transform.position);

		if (relativePos.z > 0f) {
			
			if (Mathf.Abs (prop.transform.localRotation.y) > .5f) {

				GUI.color = Color.red;
				EditorGUILayout.HelpBox ("Lights is facing to wrong direction!", MessageType.Error);
				GUI.color = originalGUIColor;

				GUI.color = Color.green;

				if (GUILayout.Button ("Fix Rotation"))
					prop.transform.localRotation = Quaternion.identity;

				GUI.color = originalGUIColor;

			}

		} else {

			if (Mathf.Abs (prop.transform.localRotation.y) < .5f) {

				GUI.color = Color.red;
				EditorGUILayout.HelpBox ("Lights is facing to wrong direction!", MessageType.Error);
				GUI.color = originalGUIColor;

				GUI.color = Color.green;

				if (GUILayout.Button ("Fix Rotation"))
					prop.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

				GUI.color = originalGUIColor;

			}

		}

		if (!EditorApplication.isPlaying) {

			GameObject[] lights = Selection.gameObjects;

			for (int i = 0; i < lights.Length; i++) {

				if (lights[i].GetComponent<Light> ().flare != null)
					lights[i].GetComponent<Light> ().flare = null;

				if (lights[i].GetComponent<LensFlare> ())
					lights[i].GetComponent<LensFlare> ().brightness = 0f;

			}
			
		}

	}

}
