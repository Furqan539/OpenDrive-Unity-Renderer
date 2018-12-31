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
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// UI Steering Wheel controller.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Mobile/RCC UI Steering Wheel")]
public class RCC_UISteeringWheelController : MonoBehaviour {

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

	private GameObject steeringWheelGameObject;
	private Image steeringWheelTexture;

	public float input = 0f;
	public float steeringWheelAngle = 0f;
	public float steeringWheelMaximumsteerAngle = 270f;
	public float steeringWheelResetPosSpeed = 20f;	
	public float steeringWheelCenterDeadZoneRadius = 5f;

	private RectTransform steeringWheelRect;
	private CanvasGroup steeringWheelCanvasGroup;

	private float steeringWheelTempAngle, steeringWheelNewAngle;
	private bool steeringWheelPressed;

	private Vector2 steeringWheelCenter, steeringWheelTouchPos;

	private EventTrigger eventTrigger;

	void Awake(){

		steeringWheelTexture = GetComponent<Image>();

	}

	void Update () {

		if(RCCSettings.mobileController != RCC_Settings.MobileController.SteeringWheel)
			return;

		SteeringWheelInit();
		SteeringWheelControlling();
		input = GetSteeringWheelInput();

	}

	void SteeringWheelInit(){

		if (steeringWheelRect && !steeringWheelTexture)
			return;

		steeringWheelGameObject = steeringWheelTexture.gameObject;
		steeringWheelRect = steeringWheelTexture.rectTransform;
		steeringWheelCanvasGroup = steeringWheelTexture.GetComponent<CanvasGroup> ();
		steeringWheelCenter = steeringWheelRect.position;
		
		SteeringWheelEventsInit ();

	}

	//Events Initialization For Steering Wheel.
	void SteeringWheelEventsInit(){

		eventTrigger = steeringWheelGameObject.GetComponent<EventTrigger>();
		
		var a = new EventTrigger.TriggerEvent();
		a.AddListener( data => 
		              {
			var evData = (PointerEventData)data;
			data.Use();
			
			steeringWheelPressed = true;
			steeringWheelTouchPos = evData.position;
			steeringWheelTempAngle = Vector2.Angle(Vector2.up, evData.position - steeringWheelCenter);
		});
		
		eventTrigger.triggers.Add(new EventTrigger.Entry{callback = a, eventID = EventTriggerType.PointerDown});
		
		
		var b = new EventTrigger.TriggerEvent();
		b.AddListener( data => 
		              {
			var evData = (PointerEventData)data;
			data.Use();
			steeringWheelTouchPos = evData.position;
		});
		
		eventTrigger.triggers.Add(new EventTrigger.Entry{callback = b, eventID = EventTriggerType.Drag});
		
		
		var c = new EventTrigger.TriggerEvent();
		c.AddListener( data => 
		              {
			steeringWheelPressed = false;
		});
		
		eventTrigger.triggers.Add(new EventTrigger.Entry{callback = c, eventID = EventTriggerType.EndDrag});

	}

	public float GetSteeringWheelInput(){

		return Mathf.Round(steeringWheelAngle / steeringWheelMaximumsteerAngle * 100) / 100;

	}

	public bool isSteeringWheelPressed(){

		return steeringWheelPressed;

	}

	public void SteeringWheelControlling (){

		if(!steeringWheelCanvasGroup || !steeringWheelRect || RCCSettings.mobileController != RCC_Settings.MobileController.SteeringWheel){
			
			if(steeringWheelGameObject)
				steeringWheelGameObject.SetActive(false);
			
			return;

		}

		if(!steeringWheelGameObject.activeSelf)
			steeringWheelGameObject.SetActive(true);

		if(steeringWheelPressed){

			steeringWheelNewAngle = Vector2.Angle(Vector2.up, steeringWheelTouchPos - steeringWheelCenter);

			if(Vector2.Distance( steeringWheelTouchPos, steeringWheelCenter ) > steeringWheelCenterDeadZoneRadius){

				if(steeringWheelTouchPos.x > steeringWheelCenter.x)
					steeringWheelAngle += steeringWheelNewAngle - steeringWheelTempAngle;
				else
					steeringWheelAngle -= steeringWheelNewAngle - steeringWheelTempAngle;

			}

			if(steeringWheelAngle > steeringWheelMaximumsteerAngle)
				steeringWheelAngle = steeringWheelMaximumsteerAngle;
			else if(steeringWheelAngle < -steeringWheelMaximumsteerAngle)
				steeringWheelAngle = -steeringWheelMaximumsteerAngle;
			
			steeringWheelTempAngle = steeringWheelNewAngle;

		}else{

			if(!Mathf.Approximately(0f, steeringWheelAngle)){

				float deltaAngle = steeringWheelResetPosSpeed;
				
				if(Mathf.Abs(deltaAngle) > Mathf.Abs(steeringWheelAngle)){
					steeringWheelAngle = 0f;
					return;
				}
				
				if(steeringWheelAngle > 0f)
					steeringWheelAngle -= deltaAngle;
				else
					steeringWheelAngle += deltaAngle;

			}

		}

		steeringWheelRect.eulerAngles = new Vector3 (0f, 0f, -steeringWheelAngle);
		
	}

	void OnDisable(){
		
		steeringWheelPressed = false;
		input = 0f;

	}

}
