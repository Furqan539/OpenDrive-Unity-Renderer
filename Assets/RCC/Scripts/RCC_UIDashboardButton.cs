//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI buttons used in options panel. It has an enum for all kind of buttons. 
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Dashboard Button")]
public class RCC_UIDashboardButton : MonoBehaviour {
	
	public ButtonType _buttonType;
	public enum ButtonType{Start, ABS, ESP, TCS, Headlights, LeftIndicator, RightIndicator, Gear, Low, Med, High, SH, GearUp, GearDown, HazardLights, SlowMo, Record, Replay, Neutral};
	private Scrollbar gearSlider;

	public int gearDirection = 0;

	void Start(){

		if(_buttonType == ButtonType.Gear && GetComponentInChildren<Scrollbar>()){
			
			gearSlider = GetComponentInChildren<Scrollbar>();
			gearSlider.onValueChanged.AddListener (delegate {ChangeGear ();});

		}

	}

	void OnEnable(){

		Check();

	}
	
	public void OnClicked () {

		if (!RCC_SceneManager.Instance.activePlayerVehicle)
			return;
		
		switch(_buttonType){
			
		case ButtonType.Start:

			RCC_SceneManager.Instance.activePlayerVehicle.KillOrStartEngine();
			
			break;
			
		case ButtonType.ABS:

			RCC_SceneManager.Instance.activePlayerVehicle.ABS = !RCC_SceneManager.Instance.activePlayerVehicle.ABS;
			
			break;
			
		case ButtonType.ESP:

			RCC_SceneManager.Instance.activePlayerVehicle.ESP = !RCC_SceneManager.Instance.activePlayerVehicle.ESP;
			
			break;
			
		case ButtonType.TCS:

			RCC_SceneManager.Instance.activePlayerVehicle.TCS = !RCC_SceneManager.Instance.activePlayerVehicle.TCS;
			
			break;

		case ButtonType.SH:

			RCC_SceneManager.Instance.activePlayerVehicle.steeringHelper = !RCC_SceneManager.Instance.activePlayerVehicle.steeringHelper;

			break;
			
		case ButtonType.Headlights:

			if(!RCC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn && RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn){
			
				RCC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn = true;
				RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn = true;
				break;

			}

			if(!RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn)
				RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn = true;
		
			if(RCC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn){
			
				RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn = false;
				RCC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn = false;

			}
			
			break;

		case ButtonType.LeftIndicator:

			if(RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn != RCC_CarControllerV3.IndicatorsOn.Left)
				RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Left;
			else
				RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;

			break;

		case ButtonType.RightIndicator:

			if(RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn != RCC_CarControllerV3.IndicatorsOn.Right)
				RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Right;
			else
				RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;

			break;

		case ButtonType.HazardLights:

			if(RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn != RCC_CarControllerV3.IndicatorsOn.All)
				RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.All;
			else
				RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;

			break;

		case ButtonType.Low:

			QualitySettings.SetQualityLevel (1);

			break;

		case ButtonType.Med:

			QualitySettings.SetQualityLevel (3);

			break;

		case ButtonType.High:

			QualitySettings.SetQualityLevel (5);

			break;

		case ButtonType.GearUp:

			RCC_SceneManager.Instance.activePlayerVehicle.GearShiftUp ();

			break;

		case ButtonType.GearDown:

			RCC_SceneManager.Instance.activePlayerVehicle.GearShiftDown ();

			break;

		case ButtonType.SlowMo:

			if (Time.timeScale != .2f)
				Time.timeScale = .2f;
			else
				Time.timeScale = 1f;

			break;

		case ButtonType.Record:

			RCC.StartStopRecord ();

			break;

		case ButtonType.Replay:

			RCC.StartStopReplay ();

			break;

		case ButtonType.Neutral:

			RCC.StopRecordReplay ();

			break;
			
		}
		
		Check();
		
	}
	
	public void Check(){

		if (!GetComponent<Image> ())
			return;

		if (!RCC_SceneManager.Instance.activePlayerVehicle)
			return;
		
		switch(_buttonType){
			
		case ButtonType.ABS:

			if(RCC_SceneManager.Instance.activePlayerVehicle.ABS)
				GetComponent<Image>().color = new Color(1, 1, 1, 1);
			else
				GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);
			
			break;
			
		case ButtonType.ESP:

			if(RCC_SceneManager.Instance.activePlayerVehicle.ESP)
				GetComponent<Image>().color = new Color(1, 1, 1, 1);
			else
				GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);
			
			break;
			
		case ButtonType.TCS:

			if(RCC_SceneManager.Instance.activePlayerVehicle.TCS)
				GetComponent<Image>().color = new Color(1, 1, 1, 1);
			else
				GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);
			
			break;

		case ButtonType.SH:

			if(RCC_SceneManager.Instance.activePlayerVehicle.steeringHelper)
				GetComponent<Image>().color = new Color(1, 1, 1, 1);
			else
				GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);

			break;
			
		case ButtonType.Headlights:

			if(RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn || RCC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn)
				GetComponent<Image>().color = new Color(1, 1, 1, 1);
			else
				GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);
			
			break;
			
		}
		
	}

	public void ChangeGear(){

		if (!RCC_SceneManager.Instance.activePlayerVehicle)
			return;

		if(gearDirection == Mathf.CeilToInt(gearSlider.value * 2))
			return;

		gearDirection = Mathf.CeilToInt(gearSlider.value * 2);

		RCC_SceneManager.Instance.activePlayerVehicle.semiAutomaticGear = true;

		switch(gearDirection){

		case 0:
			RCC_SceneManager.Instance.activePlayerVehicle.StartCoroutine("ChangeGear", 0);
			RCC_SceneManager.Instance.activePlayerVehicle.NGear = false;
			break;

		case 1:
			RCC_SceneManager.Instance.activePlayerVehicle.NGear = true;
			break;

		case 2:
			RCC_SceneManager.Instance.activePlayerVehicle.StartCoroutine("ChangeGear", -1);
			RCC_SceneManager.Instance.activePlayerVehicle.NGear = false;
			break;

		}

	}

	void OnDisable(){

//		if (!RCC_SceneManager.Instance.activePlayerVehicle)
//			return;
//
//		if(_buttonType == ButtonType.Gear){
//
//			RCC_SceneManager.Instance.activePlayerVehicle.semiAutomaticGear = false;
//
//		}

	}
	
}
