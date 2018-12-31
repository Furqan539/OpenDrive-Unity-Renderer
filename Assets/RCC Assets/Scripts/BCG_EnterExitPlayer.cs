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

/// <summary>
/// Enter Exit for FPS Player.
/// </summary>
[AddComponentMenu("BoneCracker Games/Shared Assets/Enter-Exit/BCG Enter Exit Script For Player Characters")]
public class BCG_EnterExitPlayer : MonoBehaviour {

	public bool isTPSController = false;
	public float rayHeight = 1f;

	public bool canControl = true;
	public BCG_EnterExitVehicle targetVehicle;

	public bool playerStartsAsInVehicle = false;
	public BCG_EnterExitVehicle inVehicle;

	public Camera characterCamera;

	public delegate void onBCGPlayerSpawned(BCG_EnterExitPlayer player);
	public static event onBCGPlayerSpawned OnBCGPlayerSpawned;

	public delegate void onBCGPlayerDestroyed(BCG_EnterExitPlayer player);
	public static event onBCGPlayerDestroyed OnBCGPlayerDestroyed;

	public delegate void onBCGPlayerEnteredAVehicle(BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle);
	public static event onBCGPlayerEnteredAVehicle OnBCGPlayerEnteredAVehicle;

	public delegate void onBCGPlayerExitedFromAVehicle(BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle);
	public static event onBCGPlayerExitedFromAVehicle OnBCGPlayerExitedFromAVehicle;

	void Awake () {

		if (!playerStartsAsInVehicle)
			inVehicle = null;

		if(!isTPSController)
			characterCamera = GetComponentInChildren<Camera> ();

	}

	void OnEnable () {

		if (OnBCGPlayerSpawned != null)
			OnBCGPlayerSpawned (this);

		if (playerStartsAsInVehicle)
			StartCoroutine (StartInVehicle());
	
	}

//	IEnumerator BCGPlayerSpawned(){
//
//		yield return new WaitForEndOfFrame ();
//
//		if (OnBCGPlayerSpawned != null)
//			OnBCGPlayerSpawned (this);
//
//		if (playerStartsAsInVehicle)
//			StartCoroutine (StartInVehicle());
//
//	}

	IEnumerator StartInVehicle(){

		yield return new WaitForFixedUpdate ();

		GetIn (inVehicle);

	}

	public void GetIn(BCG_EnterExitVehicle vehicle){
		
		if(OnBCGPlayerEnteredAVehicle != null)
			OnBCGPlayerEnteredAVehicle (this, vehicle);

	}

	public void GetOut(){

		if (inVehicle == null)
			return;

		if (inVehicle.speed > BCG_EnterExitSettings.Instance.enterExitSpeedLimit)
			return;

		if(OnBCGPlayerExitedFromAVehicle != null)
			OnBCGPlayerExitedFromAVehicle (this, inVehicle);

	}

	void OnDestroy () {

		if (OnBCGPlayerDestroyed != null)
			OnBCGPlayerDestroyed (this);

	}

}
