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
using UnityEngine.UI;

/// <summary>
/// Handles RCC Canvas dashboard elements.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Dashboard Displayer")]
[RequireComponent (typeof(RCC_DashboardInputs))]
public class RCC_UIDashboardDisplay : MonoBehaviour {

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

	private RCC_DashboardInputs inputs;

	public DisplayType displayType;
	public enum DisplayType{Full, Customization, TopButtonsOnly, Off}

	public GameObject topButtons;
	public GameObject controllerButtons;
	public GameObject gauges;
	public GameObject customizationMenu;
	
	public Text RPMLabel;
	public Text KMHLabel;
	public Text GearLabel;
	public Text recordingLabel;

	public Image ABS;
	public Image ESP;
	public Image Park;
	public Image Headlights;
	public Image leftIndicator;
	public Image rightIndicator;
	public Image heatIndicator;
	public Image fuelIndicator;
	public Image rpmIndicator;

	void Awake(){

		inputs = GetComponent<RCC_DashboardInputs>();

		if (!inputs) {

			enabled = false;
			return;

		}

	}
	
	void Start () {
		
//		StartCoroutine(LateDisplay());
		
	}

	void OnEnable(){

//		StopAllCoroutines();
//		StartCoroutine(LateDisplay());

	}

	void Update(){

		switch (displayType) {

		case DisplayType.Full:

			if(topButtons && !topButtons.activeInHierarchy)
				topButtons.SetActive(true);

			if(controllerButtons && !controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(true);

			if(gauges && !gauges.activeInHierarchy)
				gauges.SetActive(true);

			if(customizationMenu && customizationMenu.activeInHierarchy)
				customizationMenu.SetActive(false);

			break;

		case DisplayType.Customization:

			if(topButtons && topButtons.activeInHierarchy)
				topButtons.SetActive(false);

			if(controllerButtons && controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(false);

			if(gauges && gauges.activeInHierarchy)
				gauges.SetActive(false);

			if(customizationMenu && !customizationMenu.activeInHierarchy)
				customizationMenu.SetActive(true);

			break;

		case DisplayType.TopButtonsOnly:

			if(!topButtons.activeInHierarchy)
				topButtons.SetActive(true);

			if(controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(false);

			if(gauges.activeInHierarchy)
				gauges.SetActive(false);

			if(customizationMenu.activeInHierarchy)
				customizationMenu.SetActive(false);

			break;

		case DisplayType.Off:

			if(topButtons &&topButtons.activeInHierarchy)
				topButtons.SetActive(false);

			if(controllerButtons &&controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(false);

			if(gauges &&gauges.activeInHierarchy)
				gauges.SetActive(false);

			if(customizationMenu &&customizationMenu.activeInHierarchy)
				customizationMenu.SetActive(false);

			break;

		}

	}
	
	void LateUpdate () {

		if (RCC_SceneManager.Instance.activePlayerVehicle) {
	
			if (RPMLabel)
				RPMLabel.text = inputs.RPM.ToString ("0");
		
			if (KMHLabel) {
			
				if (RCCSettings.units == RCC_Settings.Units.KMH)
					KMHLabel.text = inputs.KMH.ToString ("0") + "\nKMH";
				else
					KMHLabel.text = (inputs.KMH * 0.62f).ToString ("0") + "\nMPH";
			
			}

			if (GearLabel) {
			
				if (!inputs.NGear && !inputs.changingGear)
					GearLabel.text = inputs.direction == 1 ? (inputs.Gear + 1).ToString ("0") : "R";
				else
					GearLabel.text = "N";
			
			}

			if (recordingLabel) {

				switch (RCC_SceneManager.Instance.recordMode) {

				case RCC_SceneManager.RecordMode.Neutral:

					if (recordingLabel.gameObject.activeInHierarchy)
						recordingLabel.gameObject.SetActive (false);

					recordingLabel.text = "";

					break;

				case RCC_SceneManager.RecordMode.Play:

					if (!recordingLabel.gameObject.activeInHierarchy)
						recordingLabel.gameObject.SetActive (true);

					recordingLabel.text = "Playing";
					recordingLabel.color = Color.green;

					break;

				case RCC_SceneManager.RecordMode.Record:

					if (!recordingLabel.gameObject.activeInHierarchy)
						recordingLabel.gameObject.SetActive (true);

					recordingLabel.text = "Recording";
					recordingLabel.color = Color.red;

					break;

				}

			}

			if (ABS)
				ABS.color = inputs.ABS == true ? Color.yellow : Color.white;
			if (ESP)
				ESP.color = inputs.ESP == true ? Color.yellow : Color.white;
			if (Park)
				Park.color = inputs.Park == true ? Color.red : Color.white;
			if (Headlights)
				Headlights.color = inputs.Headlights == true ? Color.green : Color.white;
			if (heatIndicator)
				heatIndicator.color = RCC_SceneManager.Instance.activePlayerVehicle.engineHeat >= 100f ? Color.red : new Color (.1f, 0f, 0f);
			if (fuelIndicator)
				fuelIndicator.color = RCC_SceneManager.Instance.activePlayerVehicle.fuelTank < 10f ? Color.red : new Color (.1f, 0f, 0f); 
			if (rpmIndicator)
				rpmIndicator.color = RCC_SceneManager.Instance.activePlayerVehicle.engineRPM >= RCC_SceneManager.Instance.activePlayerVehicle.maxEngineRPM - 500f ? Color.red : new Color (.1f, 0f, 0f); 
		
			if (leftIndicator && rightIndicator) {

				switch (inputs.indicators) {

				case RCC_CarControllerV3.IndicatorsOn.Left:
					leftIndicator.color = new Color (1f, .5f, 0f);
					rightIndicator.color = new Color (.5f, .25f, 0f);
					break;
				case RCC_CarControllerV3.IndicatorsOn.Right:
					leftIndicator.color = new Color (.5f, .25f, 0f);
					rightIndicator.color = new Color (1f, .5f, 0f);
					break;
				case RCC_CarControllerV3.IndicatorsOn.All:
					leftIndicator.color = new Color (1f, .5f, 0f);
					rightIndicator.color = new Color (1f, .5f, 0f);
					break;
				case RCC_CarControllerV3.IndicatorsOn.Off:
					leftIndicator.color = new Color (.5f, .25f, 0f);
					rightIndicator.color = new Color (.5f, .25f, 0f);
					break;

				}

			}

		}

	}

	public void SetDisplayType(DisplayType _displayType){

		displayType = _displayType;

	}

}
