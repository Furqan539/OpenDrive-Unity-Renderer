//----------------------------------------------
//            Realistic Tank Controller
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
/// Enter Exit for BCG Vehicles.
/// </summary>
[AddComponentMenu("BoneCracker Games/Shared Assets/Enter-Exit/BCG Enter Exit Script For Vehicle")]
public class BCG_EnterExitVehicle : MonoBehaviour {

	private Rigidbody rigid;
	public BCG_EnterExitPlayer driver;

	public GameObject correspondingCamera;
	public Transform getOutPosition;

	internal float speed = 0f;

	public delegate void onBCGVehicleSpawned(BCG_EnterExitVehicle player);
	public static event onBCGVehicleSpawned OnBCGVehicleSpawned;

	void Awake () {
			
		rigid = GetComponent<Rigidbody> ();

		gameObject.SendMessage ("SetCanControl", false, SendMessageOptions.DontRequireReceiver);

	}

	void OnEnable(){
		
		Reset ();

		RCC_Camera.OnBCGCameraSpawned += OnBCGCameraSpawned;

		if (OnBCGVehicleSpawned != null)
			OnBCGVehicleSpawned (this);

	}

	void FindCamera(){

		if(correspondingCamera)
			return;

		#if BCG_RCC

		if(GetComponent<RCC_CarControllerV3>()){
			
			correspondingCamera = GameObject.FindObjectOfType<RCC_Camera> ().gameObject;
			return;

		}

		#endif

		#if BCG_RTC

//		if(GetComponent<RCC_CarControllerV3>()){
//
//			correspondingCamera = GameObject.FindObjectOfType<RCC_Camera> ().gameObject;
//			return;
//
//		}

		#endif

		#if BCG_RHOC

//		if(GetComponent<RCC_CarControllerV3>()){
//
//		correspondingCamera = GameObject.FindObjectOfType<RCC_Camera> ().gameObject;
//		return;
//
//		}

		#endif

	}

	IEnumerator BCGVehicleSpawned(){

		yield return new WaitForEndOfFrame ();

		if (OnBCGVehicleSpawned != null)
			OnBCGVehicleSpawned (this);

	}

	void OnBCGCameraSpawned (GameObject BCGCamera){

		correspondingCamera = BCGCamera;

	}

	void FixedUpdate(){

		//Speed.
		speed = rigid.velocity.magnitude * 3.6f;

	}

	void OnDisable(){

		RCC_Camera.OnBCGCameraSpawned -= OnBCGCameraSpawned;

	}

	void OnDestroy(){

//		if (driver)
//			driver.GetOut ();

	}

	public void Reset(){
		
		FindCamera ();
		
		if (transform.Find ("Get Out Pos")) {
			
			getOutPosition = transform.Find ("Get Out Pos");

		} else {

			GameObject getOut = new GameObject ("Get Out Pos");
			getOut.transform.SetParent (transform, false);
			getOut.transform.rotation = transform.rotation;
			getOut.transform.localPosition = new Vector3 (-1.15f, 0f, 0f);
			getOutPosition = getOut.transform;

		}

	}

}
