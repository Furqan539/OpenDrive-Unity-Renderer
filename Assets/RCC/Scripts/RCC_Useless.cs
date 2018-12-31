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

public class RCC_Useless : MonoBehaviour {

	public Useless useless;
	public enum Useless{Controller, Behavior, Graphics}

	// Use this for initialization
	void Awake () {

		int type = 0;

		if(useless == Useless.Behavior){

			RCC_Settings.BehaviorType behavior = RCC_Settings.Instance.behaviorType;

			switch(behavior){
			case(RCC_Settings.BehaviorType.Simulator):
				type = 0;
				break;
			case(RCC_Settings.BehaviorType.Racing):
				type = 1;
				break;
			case(RCC_Settings.BehaviorType.SemiArcade):
				type = 2;
				break;
			case(RCC_Settings.BehaviorType.Drift):
				type = 3;
				break;
			case(RCC_Settings.BehaviorType.Fun):
				type = 4;
				break;
			case(RCC_Settings.BehaviorType.Custom):
				type = 5;
				break;
			}

		}if(useless == Useless.Controller){

			switch (RCC_Settings.Instance.mobileController) {

			case RCC_Settings.MobileController.TouchScreen:

				type = 0;

				break;

			case RCC_Settings.MobileController.Gyro:

				type = 1;

				break;

			case RCC_Settings.MobileController.SteeringWheel:

				type = 2;

				break;

			case RCC_Settings.MobileController.Joystick:

				type = 3;

				break;

			}

		}if(useless == Useless.Graphics){

			type = QualitySettings.GetQualityLevel ();

		}

		GetComponent<Dropdown>().value = type;
		GetComponent<Dropdown>().RefreshShownValue();
	
	}

}
