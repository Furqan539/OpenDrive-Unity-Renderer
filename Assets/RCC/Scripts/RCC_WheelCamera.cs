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
/// RCC Camera will be parented to this gameobject when current camera mode is Wheel Camera.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/RCC Wheel Camera")]
public class RCC_WheelCamera : MonoBehaviour {

	public void FixShake(){

		StartCoroutine (FixShakeDelayed());

	}

	IEnumerator FixShakeDelayed(){

		if (!GetComponent<Rigidbody> ())
			yield break;

		yield return new WaitForFixedUpdate ();
		GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.None;
		yield return new WaitForFixedUpdate ();
		GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.Interpolate;

	}

}
