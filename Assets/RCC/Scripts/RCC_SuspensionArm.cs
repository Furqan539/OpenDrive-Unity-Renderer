//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates and moves suspension arms based on wheelcollider suspension distance.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Visual Axle (Suspension Distance Based)")]
public class RCC_SuspensionArm : MonoBehaviour {

	public RCC_WheelCollider wheelcollider;

	public SuspensionType suspensionType;
	public enum SuspensionType{Position, Rotation}

	public Axis axis;
	public enum Axis {X, Y, Z}

	private Vector3 orgPos;
	private Vector3 orgRot;

	private float totalSuspensionDistance = 0;

	public float offsetAngle = 30;
	public float angleFactor = 150;
	
	void Start () {

		orgPos = transform.localPosition;
		orgRot = transform.localEulerAngles;

		totalSuspensionDistance = GetSuspensionDistance ();

	}

	void Update () {
		
		float suspensionCourse = GetSuspensionDistance () - totalSuspensionDistance;

		transform.localPosition = orgPos;
		transform.localEulerAngles = orgRot;

		switch (suspensionType) {

		case SuspensionType.Position:

			switch(axis){

			case Axis.X:
				transform.position += transform.right * suspensionCourse;
				break;
			case Axis.Y:
				transform.position += transform.up * suspensionCourse;
				break;
			case Axis.Z:
				transform.position += transform.forward * suspensionCourse;
				break;

			}

			break;

		case SuspensionType.Rotation:

			switch (axis) {

			case Axis.X:
				transform.Rotate (Vector3.right, suspensionCourse * angleFactor - offsetAngle, Space.Self);
				break;
			case Axis.Y:
				transform.Rotate (Vector3.up, suspensionCourse * angleFactor - offsetAngle, Space.Self);
				break;
			case Axis.Z:
				transform.Rotate (Vector3.forward, suspensionCourse * angleFactor - offsetAngle, Space.Self);
				break;

			}

			break;

		}


	}
		
	private float GetSuspensionDistance() {
		
		Quaternion quat;
		Vector3 position;
		wheelcollider.wheelCollider.GetWorldPose(out position, out quat);
		Vector3 local = wheelcollider.transform.InverseTransformPoint (position);
		return local.y;

	}

}
