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
using System.Collections.Generic;

/// <summary>
/// Fixed camera system for RCC Camera. It simply parents the RCC Camera, and calculates target position, rotation, FOV, etc...
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/RCC Fixed Camera")]
public class RCC_FixedCamera : MonoBehaviour {

	private Vector3 targetPosition;
	private Vector3 smoothedPosition;
	public float maxDistance = 50f;
	private float distance;

	public float minimumFOV = 20f;
	public float maximumFOV = 60f;
	public bool canTrackNow = false;

	void LateUpdate(){

		if (!canTrackNow)
			return;

		if (!RCC_SceneManager.Instance.activePlayerCamera)
			return;

		if (!RCC_SceneManager.Instance.activePlayerVehicle)
			return;
			
		targetPosition = RCC_SceneManager.Instance.activePlayerVehicle.transform.position;
		targetPosition += RCC_SceneManager.Instance.activePlayerVehicle.transform.rotation * Vector3.forward * (RCC_SceneManager.Instance.activePlayerVehicle.speed * .05f);
		smoothedPosition = Vector3.Lerp (smoothedPosition, targetPosition, Time.deltaTime * 5f);

		RCC_SceneManager.Instance.activePlayerCamera.targetFieldOfView = Mathf.Lerp (distance > maxDistance / 10f ? maximumFOV : 70f, minimumFOV, (distance * 1.5f) / maxDistance);

		transform.LookAt (smoothedPosition);

		transform.Translate ((-RCC_SceneManager.Instance.activePlayerVehicle.transform.forward * RCC_SceneManager.Instance.activePlayerVehicle.speed) / 50f * Time.deltaTime);

		distance = Vector3.Distance (transform.position, RCC_SceneManager.Instance.activePlayerVehicle.transform.position);

		if (distance > maxDistance)
			ChangePosition ();

	}

	public void ChangePosition(){

		if (!canTrackNow)
			return;

		if (!RCC_SceneManager.Instance.activePlayerCamera)
			return;

		if (!RCC_SceneManager.Instance.activePlayerVehicle)
			return;

		float randomizedAngle = Random.Range (-15f, 15f);
		RaycastHit hit;

		if (Physics.Raycast (RCC_SceneManager.Instance.activePlayerVehicle.transform.position, Quaternion.AngleAxis (randomizedAngle, RCC_SceneManager.Instance.activePlayerVehicle.transform.up) * RCC_SceneManager.Instance.activePlayerVehicle.transform.forward, out hit, maxDistance) && !hit.transform.IsChildOf(RCC_SceneManager.Instance.activePlayerVehicle.transform) && !hit.collider.isTrigger) {

			transform.position = hit.point;
			transform.LookAt (RCC_SceneManager.Instance.activePlayerVehicle.transform.position + new Vector3(0f, Mathf.Clamp(randomizedAngle, .5f, 5f), 0f));
			transform.position += transform.rotation * Vector3.forward * 5f;

		} else {
			
			transform.position = RCC_SceneManager.Instance.activePlayerVehicle.transform.position + new Vector3(0f, Mathf.Clamp(randomizedAngle, 0f, 5f), 0f);
			transform.position += Quaternion.AngleAxis (randomizedAngle, RCC_SceneManager.Instance.activePlayerVehicle.transform.up) * RCC_SceneManager.Instance.activePlayerVehicle.transform.forward * (maxDistance * .9f);

		}

	}
	
}
