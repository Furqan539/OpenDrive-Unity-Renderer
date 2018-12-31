//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the caliper.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Visual Brake Caliper")]
public class RCC_Caliper : MonoBehaviour {

	public RCC_WheelCollider wheelCollider;

	private GameObject newPivot;
	private Quaternion defLocalRotation;

	void Start () {

		if (!wheelCollider){

			Debug.LogError ("WheelCollider is not selected for this caliper named " + transform.name);
			enabled = false;
			return;

		}

		newPivot = new GameObject ("Pivot_" + transform.name);
		newPivot.transform.SetParent (wheelCollider.wheelCollider.transform, false);
		transform.SetParent (newPivot.transform, true);

		defLocalRotation = newPivot.transform.localRotation;
		
	}

	void Update () {

		if (!wheelCollider.wheelModel || !wheelCollider.wheelCollider)
			return;

		newPivot.transform.position = new Vector3 (wheelCollider.wheelModel.transform.position.x, wheelCollider.wheelModel.transform.position.y, wheelCollider.wheelModel.transform.position.z);
		newPivot.transform.localRotation = defLocalRotation * Quaternion.AngleAxis (wheelCollider.wheelCollider.steerAngle, Vector3.up);
		
	}

}
