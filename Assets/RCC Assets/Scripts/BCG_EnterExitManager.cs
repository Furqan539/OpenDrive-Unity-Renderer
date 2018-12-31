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
/// Main Enter Exit Manager for Scene.
/// </summary>
[AddComponentMenu("BoneCracker Games/Shared Assets/Enter-Exit/BCG Main Enter Exit Manager")]
public class BCG_EnterExitManager : MonoBehaviour {

	#region singleton
	private static BCG_EnterExitManager instance;
	public static BCG_EnterExitManager Instance{

		get{

			if (instance == null) {

				instance = FindObjectOfType<BCG_EnterExitManager> ();

				if (instance == null) {

					GameObject sceneManager = new GameObject ("_BCGEnterExitManager");
					instance = sceneManager.AddComponent<BCG_EnterExitManager> ();

				}

			}

			return instance;

		}

	}
	#endregion

	public List <GameObject> cachedMainCameras = new List<GameObject> ();
	public List<BCG_EnterExitPlayer> cachedPlayers = new List<BCG_EnterExitPlayer>();
	public List<BCG_EnterExitVehicle> cachedVehicles = new List<BCG_EnterExitVehicle>();
	public BCG_EnterExitCharacterUICanvas cachedCanvas;

	internal float waitTime = 5;
	public BCG_EnterExitVehicle targetVehicle;

	[System.Serializable]
	public class BCG_CharacterPlayer{

		public BCG_EnterExitPlayer characterPlayer;
		public BCG_EnterExitVehicle currentlyInThisVehicle;

	}
		
	public BCG_CharacterPlayer BCGCharacterPlayer;

	public bool showGui = false;

	void Awake () {

		BCG_EnterExitPlayer.OnBCGPlayerSpawned += BCG_Player_OnBCGPlayerSpawned;
		BCG_EnterExitPlayer.OnBCGPlayerDestroyed += BCG_Player_OnBCGPlayerDestroyed;
		BCG_EnterExitVehicle.OnBCGVehicleSpawned += BCG_Player_OnBCGVehicleSpawned;
		BCG_EnterExitPlayer.OnBCGPlayerEnteredAVehicle += BCG_Player_OnBCGPlayerEnteredAVehicle;
		BCG_EnterExitPlayer.OnBCGPlayerExitedFromAVehicle += BCG_Player_OnBCGPlayerExitedFromAVehicle;
		BCG_EnterExitCharacterUICanvas.OnBCGPlayerCanvasSpawned += BCG_EnterExitCharacterUICanvas_OnBCGPlayerCanvasSpawned;

	}

	void Start(){

		for (int i = 0; i < cachedMainCameras.Count; i++)
			cachedMainCameras [i].SendMessage ("ToggleCamera", false, SendMessageOptions.DontRequireReceiver);
			
	}

	void Update(){

		waitTime += Time.deltaTime;

		Inputs ();

		if (showGui && RCC_InfoLabel.Instance) {

			#if MOBILE_INPUT
			RCC_InfoLabel.Instance.ShowInfo ("Press ''" + "interaction" + "'' key to Get In");
			#else
			RCC_InfoLabel.Instance.ShowInfo ("Press ''" + BCG_EnterExitSettings.Instance.enterExitVehicleKB.ToString () + "'' key to Get In");
			#endif

		}

	}

	void BCG_Player_OnBCGPlayerSpawned (BCG_EnterExitPlayer player){

		if(!cachedPlayers.Contains(player))
			cachedPlayers.Add (player);

		BCG_CharacterPlayer newPlayer = new BCG_CharacterPlayer ();

		newPlayer.characterPlayer = player;
		newPlayer.currentlyInThisVehicle = null;

		BCGCharacterPlayer = newPlayer;
		
	}

	void BCG_Player_OnBCGPlayerDestroyed (BCG_EnterExitPlayer player){

		if(cachedPlayers.Contains(player))
			cachedPlayers.Remove (player);

		BCGCharacterPlayer = null;

	}

	void BCG_Player_OnBCGVehicleSpawned (BCG_EnterExitVehicle player){

		if(!cachedVehicles.Contains(player))
			cachedVehicles.Add (player);
		
		if (!cachedMainCameras.Contains (player.correspondingCamera)) {

			cachedMainCameras.Add (player.correspondingCamera);
			player.correspondingCamera.SendMessage ("ToggleCamera", false, SendMessageOptions.DontRequireReceiver);

		}

	}
		
	public void BCG_Player_OnBCGPlayerEnteredAVehicle (BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle){
		
		if (waitTime < 1)
			return;

		print ("Player Named " + player.name + " has entered a vehicle named " + vehicle.name);

		player.inVehicle = vehicle;
		player.gameObject.SetActive (false);
		player.transform.SetParent (vehicle.transform, true);
		player.transform.localPosition = Vector3.zero;
		player.transform.localRotation = Quaternion.identity;
		player.transform.position = vehicle.transform.position;
		player.transform.localRotation = Quaternion.identity;
		BCGCharacterPlayer.currentlyInThisVehicle = vehicle;

		if(cachedCanvas)
			cachedCanvas.displayType = BCG_EnterExitCharacterUICanvas.DisplayType.InVehicle;

		for (int i = 0; i < cachedMainCameras.Count; i++) {

			if (cachedMainCameras [i] != vehicle.correspondingCamera) {
				cachedMainCameras [i].SendMessage ("ToggleCamera", false, SendMessageOptions.DontRequireReceiver);
			} else {
				cachedMainCameras [i].SendMessage ("ToggleCamera", true, SendMessageOptions.DontRequireReceiver);
				cachedMainCameras [i].SendMessage ("SetTarget", vehicle.gameObject, SendMessageOptions.DontRequireReceiver);
			}

		}

		vehicle.gameObject.SendMessage ("SetCanControl", true, SendMessageOptions.DontRequireReceiver);
		vehicle.gameObject.SendMessage ("SetEngine", true, SendMessageOptions.DontRequireReceiver);
		vehicle.driver = player;

		RCC_SceneManager.Instance.activePlayerVehicle = vehicle.GetComponent<RCC_CarControllerV3> ();

		waitTime = 0f;

	}

	void BCG_Player_OnBCGPlayerExitedFromAVehicle (BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle){

		if (waitTime < 1)
			return;

		print ("Player Named " + player.name + " has exited from a vehicle named " + vehicle.name);
		player.inVehicle = null;
		player.transform.SetParent (null);
		player.transform.rotation = Quaternion.Euler(0f, vehicle.transform.rotation.y, 0f);
		BCGCharacterPlayer.currentlyInThisVehicle = null;

		if(cachedCanvas)
			cachedCanvas.displayType = BCG_EnterExitCharacterUICanvas.DisplayType.OnFoot;

		if (vehicle.getOutPosition) {
			player.transform.position = vehicle.getOutPosition.position;
		} else {
			player.transform.position = vehicle.transform.position;
			player.transform.position += vehicle.transform.right * -1.5f;
		}
			
		player.gameObject.SetActive (true);

		for (int i = 0; i < cachedMainCameras.Count; i++)
			cachedMainCameras [i].SendMessage ("ToggleCamera", false, SendMessageOptions.DontRequireReceiver);

		vehicle.gameObject.SendMessage ("SetCanControl", false, SendMessageOptions.DontRequireReceiver);
		vehicle.gameObject.SendMessage ("SetEngine", BCG_EnterExitSettings.Instance.keepEnginesAlive, SendMessageOptions.DontRequireReceiver);
		vehicle.driver = null;

//		if(RCC_CustomizerExample.Instance)
//			RCC_CustomizerExample.Instance.vehicle = null;

		RCC_SceneManager.Instance.activePlayerVehicle = null;

		waitTime = 0f;

	}

	void BCG_EnterExitCharacterUICanvas_OnBCGPlayerCanvasSpawned (BCG_EnterExitCharacterUICanvas canvas){

		cachedCanvas = canvas;

		if (BCGCharacterPlayer.currentlyInThisVehicle != null)
			cachedCanvas.displayType = BCG_EnterExitCharacterUICanvas.DisplayType.InVehicle;
		else
			cachedCanvas.displayType = BCG_EnterExitCharacterUICanvas.DisplayType.OnFoot;

	}

	void Inputs(){

		if (!BCGCharacterPlayer.characterPlayer)
			return;

		if (!BCGCharacterPlayer.characterPlayer.inVehicle) {

			Vector3 rayPosition;
			Quaternion rayRotation = new Quaternion ();

			if (BCGCharacterPlayer.characterPlayer.characterCamera) {

				rayPosition = BCGCharacterPlayer.characterPlayer.characterCamera.transform.position;
				rayRotation = BCGCharacterPlayer.characterPlayer.characterCamera.transform.rotation;

			} else {

				rayPosition = BCGCharacterPlayer.characterPlayer.transform.position + (Vector3.up * BCGCharacterPlayer.characterPlayer.rayHeight);
				rayRotation = BCGCharacterPlayer.characterPlayer.transform.rotation;

			}

			Vector3 rayDirection = rayRotation * Vector3.forward;

			RaycastHit hit;

			Debug.DrawRay (rayPosition, rayDirection * 1.5f, Color.blue);

			if (Physics.Raycast (rayPosition, rayDirection, out hit, 1.5f)) {

				if (!targetVehicle) {

					targetVehicle = hit.collider.transform.GetComponentInParent<BCG_EnterExitVehicle> ();

				} else {

					showGui = true;

					if (Input.GetKeyDown (BCG_EnterExitSettings.Instance.enterExitVehicleKB)) {

						BCGCharacterPlayer.characterPlayer.GetIn (targetVehicle);

					}

				}

			} else {

				showGui = false;
				targetVehicle = null;

			}

		} else {

			showGui = false;
			targetVehicle = null;

			if (Input.GetKeyDown (BCG_EnterExitSettings.Instance.enterExitVehicleKB))
				BCGCharacterPlayer.characterPlayer.GetOut ();

		}

	}

	public void Interact(){

		if(BCGCharacterPlayer.currentlyInThisVehicle == null && targetVehicle)
			BCGCharacterPlayer.characterPlayer.GetIn (targetVehicle);
		else
			BCGCharacterPlayer.characterPlayer.GetOut ();

	}

	void OnDisable () {

		BCG_EnterExitPlayer.OnBCGPlayerSpawned -= BCG_Player_OnBCGPlayerSpawned;
		BCG_EnterExitPlayer.OnBCGPlayerDestroyed -= BCG_Player_OnBCGPlayerDestroyed;
		BCG_EnterExitVehicle.OnBCGVehicleSpawned -= BCG_Player_OnBCGVehicleSpawned;
		BCG_EnterExitPlayer.OnBCGPlayerEnteredAVehicle -= BCG_Player_OnBCGPlayerEnteredAVehicle;
		BCG_EnterExitPlayer.OnBCGPlayerExitedFromAVehicle -= BCG_Player_OnBCGPlayerExitedFromAVehicle;
		BCG_EnterExitCharacterUICanvas.OnBCGPlayerCanvasSpawned -= BCG_EnterExitCharacterUICanvas_OnBCGPlayerCanvasSpawned;

	}

}
