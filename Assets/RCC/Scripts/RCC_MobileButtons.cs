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
/// Receiving inputs from UI buttons, and feeds active vehicles on your scene.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Mobile/RCC UI Mobile Buttons")]
public class RCC_MobileButtons : MonoBehaviour {

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

	public RCC_UIController gasButton;
	public RCC_UIController gradualGasButton;
	public RCC_UIController brakeButton;
	public RCC_UIController leftButton;
	public RCC_UIController rightButton;
	public RCC_UISteeringWheelController steeringWheel;
	public RCC_UIController handbrakeButton;
	public RCC_UIController NOSButton;
	public RCC_UIController NOSButtonSteeringWheel;
	public RCC_UIDrag drag;
	public GameObject gearButton;
	public RCC_UIJoystick joystick;

	private float gasInput = 0f;
	private float brakeInput = 0f;
	private float leftInput = 0f;
	private float rightInput = 0f;
	private float steeringWheelInput = 0f;
	private float handbrakeInput = 0f;
	private float NOSInput = 1f;
	private float gyroInput = 0f;
	private float joystickInput = 0f;
	private bool canUseNos = false;

	private Vector3 orgBrakeButtonPos;

	void Start(){

		if(RCCSettings.controllerType != RCC_Settings.ControllerType.Mobile){
			
			if(gasButton)
				gasButton.gameObject.SetActive(false);
			if(gradualGasButton)
				gradualGasButton.gameObject.SetActive(false);
			if(leftButton)
				leftButton.gameObject.SetActive(false);
			if(rightButton)
				rightButton.gameObject.SetActive(false);
			if(brakeButton)
				brakeButton.gameObject.SetActive(false);
			if(steeringWheel)
				steeringWheel.gameObject.SetActive(false);
			if(handbrakeButton)
				handbrakeButton.gameObject.SetActive(false);
			if(NOSButton)
				NOSButton.gameObject.SetActive(false);
			if(NOSButtonSteeringWheel)
				NOSButtonSteeringWheel.gameObject.SetActive(false);
			if(gearButton)
				gearButton.gameObject.SetActive(false);
			if(joystick)
				joystick.gameObject.SetActive(false);
			
			enabled = false;
			return;

		}

		orgBrakeButtonPos = brakeButton.transform.position;

	}

	void Update(){

		switch (RCCSettings.mobileController) {

		case RCC_Settings.MobileController.TouchScreen:

			gyroInput = 0f;

			if(steeringWheel && steeringWheel.gameObject.activeInHierarchy)
				steeringWheel.gameObject.SetActive(false);

			if(NOSButton && NOSButton.gameObject.activeInHierarchy != canUseNos)
				NOSButton.gameObject.SetActive(canUseNos);

			if(joystick && joystick.gameObject.activeInHierarchy)
				joystick.gameObject.SetActive(false);

			if(!leftButton.gameObject.activeInHierarchy){

				brakeButton.transform.position = orgBrakeButtonPos;
				leftButton.gameObject.SetActive(true);

			}

			if(!rightButton.gameObject.activeInHierarchy)
				rightButton.gameObject.SetActive(true);

			break;

		case RCC_Settings.MobileController.Gyro:

			gyroInput = Input.acceleration.x * RCCSettings.gyroSensitivity;
			brakeButton.transform.position = leftButton.transform.position;

			if(steeringWheel.gameObject.activeInHierarchy)
				steeringWheel.gameObject.SetActive(false);

			if(NOSButton && NOSButton.gameObject.activeInHierarchy != canUseNos)
				NOSButton.gameObject.SetActive(canUseNos);

			if(joystick && joystick.gameObject.activeInHierarchy)
				joystick.gameObject.SetActive(false);

			if(leftButton.gameObject.activeInHierarchy)
				leftButton.gameObject.SetActive(false);

			if(rightButton.gameObject.activeInHierarchy)
				rightButton.gameObject.SetActive(false);

			break;

		case RCC_Settings.MobileController.SteeringWheel:

			gyroInput = 0f;

			if(!steeringWheel.gameObject.activeInHierarchy){
				steeringWheel.gameObject.SetActive(true);
				brakeButton.transform.position = orgBrakeButtonPos;
			}

			if(NOSButton && NOSButton.gameObject.activeInHierarchy)
				NOSButton.gameObject.SetActive(false);

			if(NOSButtonSteeringWheel && NOSButtonSteeringWheel.gameObject.activeInHierarchy != canUseNos)
				NOSButtonSteeringWheel.gameObject.SetActive(canUseNos);

			if(joystick && joystick.gameObject.activeInHierarchy)
				joystick.gameObject.SetActive(false);

			if(leftButton.gameObject.activeInHierarchy)
				leftButton.gameObject.SetActive(false);
			if(rightButton.gameObject.activeInHierarchy)
				rightButton.gameObject.SetActive(false);

			break;

		case RCC_Settings.MobileController.Joystick:

			gyroInput = 0f;

			if (steeringWheel && steeringWheel.gameObject.activeInHierarchy)
				steeringWheel.gameObject.SetActive (false);

			if (NOSButton && NOSButton.gameObject.activeInHierarchy != canUseNos)
				NOSButton.gameObject.SetActive (canUseNos);

			if (joystick && !joystick.gameObject.activeInHierarchy) {
				joystick.gameObject.SetActive (true);
				brakeButton.transform.position = orgBrakeButtonPos;
			}

			if(leftButton.gameObject.activeInHierarchy)
				leftButton.gameObject.SetActive(false);

			if(rightButton.gameObject.activeInHierarchy)
				rightButton.gameObject.SetActive(false);

			break;

		}

		gasInput = GetInput(gasButton) + GetInput(gradualGasButton);
		brakeInput = GetInput(brakeButton);
		leftInput = GetInput(leftButton);
		rightInput = GetInput(rightButton);
		handbrakeInput = GetInput(handbrakeButton);
		NOSInput = Mathf.Clamp((GetInput(NOSButton) + GetInput(NOSButtonSteeringWheel)) * 2.5f, 1f, 2.5f);

		if(steeringWheel)
			steeringWheelInput = steeringWheel.input;

		if(joystick)
			joystickInput = joystick.inputHorizontal;

		if (RCC_SceneManager.Instance.activePlayerVehicle) {

			canUseNos = RCC_SceneManager.Instance.activePlayerVehicle.useNOS;

			if (RCC_SceneManager.Instance.activePlayerVehicle.canControl && !RCC_SceneManager.Instance.activePlayerVehicle.externalController) {

				RCC_SceneManager.Instance.activePlayerVehicle.gasInput = gasInput;
				RCC_SceneManager.Instance.activePlayerVehicle.brakeInput = brakeInput;
				RCC_SceneManager.Instance.activePlayerVehicle.steerInput = -leftInput + rightInput + steeringWheelInput + gyroInput + joystickInput;
				RCC_SceneManager.Instance.activePlayerVehicle.handbrakeInput = handbrakeInput;
				RCC_SceneManager.Instance.activePlayerVehicle.boostInput = NOSInput;

			}

		}

	}

	// Gets input from button.
	float GetInput(RCC_UIController button){

		if(button == null)
			return 0f;

		return(button.input);

	}

	// Sets the mobile controller type.
	public void SetMobileController(int index){

		switch(index){

		case 0:
			RCC.SetMobileController (RCC_Settings.MobileController.TouchScreen);
			break;
		case 1:
			RCC.SetMobileController (RCC_Settings.MobileController.Gyro);
			break;
		case 2:
			RCC.SetMobileController (RCC_Settings.MobileController.SteeringWheel);
			break;
		case 3:
			RCC.SetMobileController (RCC_Settings.MobileController.Joystick);
			break;

		}

	}

	// Sets the quality.
	public void SetQuality(int index){

		QualitySettings.SetQualityLevel (index);

	}

	// Changes current camera mode.
	public void ChangeCamera () {

		if(RCC_SceneManager.Instance.activePlayerCamera)
			RCC_SceneManager.Instance.activePlayerCamera.ChangeCamera();

	}

}
