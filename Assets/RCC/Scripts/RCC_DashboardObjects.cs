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
using UnityEngine.UI;

/// <summary>
/// Receiving inputs from active vehicle on your scene, and feeds visual dashboard needles.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Visual Dashboard Objects Manager")]
public class RCC_DashboardObjects : MonoBehaviour {

	// Getting an Instance of Main Shared RCC Settings.
	#region RCC Settings Instance

	private RCC_Settings RCCSettingsInstance;
	private RCC_Settings RCCSettings {
		get {
			if (RCCSettingsInstance == null) {
				RCCSettingsInstance = RCC_Settings.Instance;
			}
			return RCCSettingsInstance;
		}
	}

	#endregion

	private RCC_CarControllerV3 carController;

	public GameObject RPMMeterDial;
	public GameObject speedOMeterDial;
	public GameObject engineHeatDial;
	public GameObject fuelDial;

	private Quaternion RPMMeterDialOrgRotation;
	private Quaternion speedOMeterDialOrgRotation;
	private Quaternion engineHeatDialOrgRotation;
	private Quaternion fuelDialOrgRotation;

	public float RPMMeterDialMultiplier = 1f;
	public float speedOMeterDialMultiplier = 1f;
	public float engineHeatDialMultiplier = 1f;
	public float fuelDialMultiplier = 1f;

	public RotateAround rotateAround;
	public enum RotateAround{X, Y, Z}

	public Text RPMMeterText;
	public Text speedOMeterText;

	public Light[] dashboardLights;
	public float lightIntensity = 1f;

	void Awake () {

		carController = GetComponentInParent<RCC_CarControllerV3> ();

		if(RPMMeterDial)
			RPMMeterDialOrgRotation = RPMMeterDial.transform.localRotation;

		if(speedOMeterDial)
			speedOMeterDialOrgRotation = speedOMeterDial.transform.localRotation;

		if(engineHeatDial)
			engineHeatDialOrgRotation = engineHeatDial.transform.localRotation;

		if(fuelDial)
			fuelDialOrgRotation = fuelDial.transform.localRotation;

		for (int i = 0; i < dashboardLights.Length; i++) {

			if(RCCSettings.useLightsAsVertexLights)
				dashboardLights [i].renderMode = LightRenderMode.ForceVertex;
			else
				dashboardLights [i].renderMode = LightRenderMode.ForcePixel;

		}

	}

	void Update(){

		if (!carController)
			return;

		Dials ();
		DashboardLights ();

	}
	
	void Dials () {

		Vector3 targetAxis;

		switch (rotateAround) {

		case RotateAround.X:

			targetAxis = Vector3.right;

			break;

		case RotateAround.Y:

			targetAxis = Vector3.up;
			
			break;

		case RotateAround.Z:

			targetAxis = Vector3.forward;
			
			break;

		}

		if(RPMMeterDial)
			RPMMeterDial.transform.localRotation = RPMMeterDialOrgRotation * Quaternion.AngleAxis (-RPMMeterDialMultiplier * carController.engineRPM, Vector3.forward);

		if(speedOMeterDial)
			speedOMeterDial.transform.localRotation = speedOMeterDialOrgRotation * Quaternion.AngleAxis (-speedOMeterDialMultiplier * carController.speed, Vector3.forward);

		if(engineHeatDial)
			engineHeatDial.transform.localRotation = engineHeatDialOrgRotation * Quaternion.AngleAxis (-engineHeatDialMultiplier * carController.engineHeat, Vector3.forward);

		if(fuelDial)
			fuelDial.transform.localRotation = fuelDialOrgRotation * Quaternion.AngleAxis (-fuelDialMultiplier * carController.engineHeat, Vector3.forward);

		if (RPMMeterText)
			RPMMeterText.text = carController.engineRPM.ToString ("F0");

		if (speedOMeterText)
			speedOMeterText.text = carController.speed.ToString ("F0");

	}

	void DashboardLights (){

		for (int i = 0; i < dashboardLights.Length; i++) {

			dashboardLights [i].intensity = carController.lowBeamHeadLightsOn || carController.highBeamHeadLightsOn ? lightIntensity : 0f;

			if (!dashboardLights [i].enabled)
				dashboardLights [i].enabled = true;

		}

	}

}
