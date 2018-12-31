//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RCC_Settings))]
public class RCC_SettingsEditor : Editor {

	RCC_Settings RCCSettingsAsset;

	Color originalGUIColor;
	Vector2 scrollPos;
	PhysicMaterial[] physicMaterials;

	bool foldGeneralSettings = false;
	bool foldControllerSettings = false;
	bool foldUISettings = false;
	bool foldWheelPhysics = false;
	bool foldSFX = false;
	bool foldOptimization = false;
	bool foldTagsAndLayers = false;

//	public bool EnableReWired
//	{
//		get
//		{
//			bool _bool = RCC_Settings.Instance.enableReWired;
//
//			return _bool;
//		}
//
//		set
//		{
//			bool _bool = RCC_Settings.Instance.enableReWired;
//
//			if(_bool == value)
//				return;
//
//			RCC_Settings.Instance.enableReWired = value;
//
//			foreach (BuildTargetGroup buildTarget in Enum.GetValues(typeof(BuildTargetGroup))) {
//				if(buildTarget != BuildTargetGroup.Unknown)
//					SetScriptingSymbol("RCC_REWIRED", buildTarget, value);
//			}
//
//		}
//	}

	void OnEnable(){

		foldGeneralSettings = RCC_Settings.Instance.foldGeneralSettings;
		foldControllerSettings = RCC_Settings.Instance.foldControllerSettings;
		foldUISettings = RCC_Settings.Instance.foldUISettings;
		foldWheelPhysics = RCC_Settings.Instance.foldWheelPhysics;
		foldSFX = RCC_Settings.Instance.foldSFX;
		foldOptimization = RCC_Settings.Instance.foldOptimization;
		foldTagsAndLayers = RCC_Settings.Instance.foldTagsAndLayers;

	}

	void OnDestroy(){

		RCC_Settings.Instance.foldGeneralSettings = foldGeneralSettings;
		RCC_Settings.Instance.foldControllerSettings = foldControllerSettings;
		RCC_Settings.Instance.foldUISettings = foldUISettings;
		RCC_Settings.Instance.foldWheelPhysics = foldWheelPhysics;
		RCC_Settings.Instance.foldSFX = foldSFX;
		RCC_Settings.Instance.foldOptimization = foldOptimization;
		RCC_Settings.Instance.foldTagsAndLayers = foldTagsAndLayers;

	}

	public override void OnInspectorGUI (){

		serializedObject.Update();
		RCCSettingsAsset = (RCC_Settings)target;

		originalGUIColor = GUI.color;
		EditorGUIUtility.labelWidth = 250;
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("RCC Asset Settings Editor Window", EditorStyles.boldLabel);
		GUI.color = new Color(.75f, 1f, .75f);
		EditorGUILayout.LabelField("This editor will keep update necessary .asset files in your project for RCC. Don't change directory of the ''Resources/RCC Assets''.", EditorStyles.helpBox);
		GUI.color = originalGUIColor;
		EditorGUILayout.Space();

		EditorGUI.indentLevel++;

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false );

		EditorGUILayout.Space();

		foldGeneralSettings = EditorGUILayout.Foldout(foldGeneralSettings, "General Settings");

		if(foldGeneralSettings){

			EditorGUILayout.BeginVertical (GUI.skin.box);
			GUILayout.Label("General Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("overrideFixedTimeStep"), new GUIContent("Override FixedTimeStep"));
			if(RCCSettingsAsset.overrideFixedTimeStep)
				EditorGUILayout.PropertyField(serializedObject.FindProperty("fixedTimeStep"), new GUIContent("Fixed Timestep"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxAngularVelocity"), new GUIContent("Maximum Angular Velocity"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("behaviorType"), new GUIContent("Behavior Type"));

			GUI.color = new Color(.75f, 1f, .75f);
			EditorGUILayout.HelpBox("Using behavior preset will override wheelcollider settings, chassis joint, antirolls, and other stuff. Using ''Custom'' mode will not override anything.", MessageType.Info);
			GUI.color = originalGUIColor;

			switch (RCCSettingsAsset.behaviorType) {

			case RCC_Settings.BehaviorType.Simulator:

				EditorGUILayout.HelpBox("Simulator Mode is making these changes;", MessageType.Info);
				EditorGUILayout.HelpBox("RCC_CarController.cs \n \n Limits antiRollFrontHorizontal, 1000f - Mathf.Infinity \n Limits antiRollRearHorizontal, 1000f - Mathf.Infinity", MessageType.None);
				EditorGUILayout.HelpBox("RCC_WheelCollider.cs \n \n Sets forward and sideways friction curves. ForwardFrictionCurve, .2f, 1f, .8f, .75f. SidewaysFrictionCurve, .25f, 1f, .5f, .75f. \n Limits wheelCollider.forceAppPointDistance, 0f - 1f", MessageType.None);

				break;

			case RCC_Settings.BehaviorType.Racing:

				EditorGUILayout.HelpBox("Racing Mode is making these changes;", MessageType.Info);
				EditorGUILayout.HelpBox("RCC_CarController.cs \n \n steeringHelper = true \n tractionHelper = true \n Limits steerHelperLinearVelStrength .25f - 1f \n Limits steerHelperAngularVelStrength, .25f - 1f \n Limits tractionHelperStrength, .25f - 1f \n Limits antiRollFrontHorizontal, 10000f - Mathf.Infinity \n Limits antiRollRearHorizontal, 10000f - Mathf.Infinity", MessageType.None);
				EditorGUILayout.HelpBox("RCC_WheelCollider.cs \n \n Sets forward and sideways friction curves. ForwardFrictionCurve, .2f, 1f, .8f, .75f. SidewaysFrictionCurve, 3f, 1f, .25f, .75f. \n Limits wheelCollider.forceAppPointDistance, .25f - 1f", MessageType.None);

				break;

			case RCC_Settings.BehaviorType.SemiArcade:

				EditorGUILayout.HelpBox("SemiArcade Mode is making these changes;", MessageType.Info);
				EditorGUILayout.HelpBox("RCC_CarController.cs \n \n steeringHelper = true \n tractionHelper = true \n ABS, ESP, TCS = false \n Limits steerHelperLinearVelStrength .5f - 1f \n Limits steerHelperAngularVelStrength, 1f - 1f \n Limits tractionHelperStrength, .25f - 1f \n Limits antiRollFrontHorizontal, 10000f - Mathf.Infinity \n Limits antiRollRearHorizontal, 10000f - Mathf.Infinity \n Limits gearShiftingDelay, 0f - .1f", MessageType.None);
				EditorGUILayout.HelpBox("RCC_WheelCollider.cs \n \n Sets forward and sideways friction curves. ForwardFrictionCurve, .2f, 2f, 2f, 2f. SidewaysFrictionCurve, .25f, 2f, 2f, 2f. \n Limits wheelCollider.forceAppPointDistance, .35f - 1f", MessageType.None);

				break;

			case RCC_Settings.BehaviorType.Drift:

				EditorGUILayout.HelpBox("Drift Mode is making these changes;", MessageType.Info);
				EditorGUILayout.HelpBox("RCC_CarController.cs \n \n steeringHelper = false \n tractionHelper = true \n ABS, ESP, TCS = false \n Limits highspeedsteerAngle, 40f - 50f \n Limits highspeedsteerAngleAtspeed, 100f - maxspeed \n Limits tractionHelperStrength, .1f - 1f\n Limits engineTorque, 2000f - Mathf.Infinity \n Limits antiRollFrontHorizontal, 2500f - Mathf.Infinity \n Limits antiRollRearHorizontal, 2500f - Mathf.Infinity \n Limits gearShiftingDelay, 0f - .15f", MessageType.None);
				EditorGUILayout.HelpBox("RCC_WheelCollider.cs \n \n Sets forward and sideways friction curves. ForwardFrictionCurve, .25f, 1f, .8f, .75f. SidewaysFrictionCurve, .4f, 1f, .5f, .75f. \n Limits wheelCollider.forceAppPointDistance, .1f - 1f", MessageType.None);

				break;

			case RCC_Settings.BehaviorType.Fun:

				EditorGUILayout.HelpBox("Fun Mode is making these changes;", MessageType.Info);
				EditorGUILayout.HelpBox("RCC_CarController.cs \n \n steeringHelper = true \n tractionHelper = true \n ABS, ESP, TCS = false \n Limits steerHelperLinearVelStrength .5f - 1f \n Limits steerHelperAngularVelStrength, 1f - 1f \n Limits highspeedsteerAngle, 30f - 50f \n Limits highspeedsteerAngleAtspeed, 100f - maxspeed \n Limits antiRollFrontHorizontal, 20000f - Mathf.Infinity \n Limits antiRollRearHorizontal, 20000f - Mathf.Infinity \n Limits gearShiftingDelay, 0f - .1f", MessageType.None);
				EditorGUILayout.HelpBox("RCC_WheelCollider.cs \n \n Sets forward and sideways friction curves. ForwardFrictionCurve, .2f, 2f, 2f, 2f. SidewaysFrictionCurve, .25f, 2f, 2f, 2f. \n Limits wheelCollider.forceAppPointDistance, .35f - 1f", MessageType.None);

				break;

			case RCC_Settings.BehaviorType.Custom:

				EditorGUILayout.HelpBox("Custom Mode is not making any changes.", MessageType.Info);

				break;

			}

			EditorGUILayout.HelpBox("You can find all references to any mode. Open up ''RCC_Settings.cs'' and right click to any mode. Hit ''Find references'' to find all modifications.", MessageType.Info);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("useFixedWheelColliders"), new GUIContent("Use Fixed WheelColliders", "Improves stability by increasing mass of the WheelColliders."));
			EditorGUILayout.EndVertical ();

		}

		EditorGUILayout.Space();

		foldControllerSettings = EditorGUILayout.Foldout(foldControllerSettings, "Controller Settings");

		if(foldControllerSettings){
			
			List<string> controllerTypeStrings =  new List<string>();
			controllerTypeStrings.Add("Keyboard");	controllerTypeStrings.Add("Mobile");		controllerTypeStrings.Add("Custom");
			EditorGUILayout.BeginVertical (GUI.skin.box);

			GUI.color = new Color(.5f, 1f, 1f, 1f);
			GUILayout.Label("Main Controller Type", EditorStyles.boldLabel);
			RCCSettingsAsset.toolbarSelectedIndex = GUILayout.Toolbar(RCCSettingsAsset.toolbarSelectedIndex, controllerTypeStrings.ToArray());
			GUI.color = originalGUIColor;
			EditorGUILayout.Space();

			if(RCCSettingsAsset.toolbarSelectedIndex == 0){

				RCCSettingsAsset.controllerType = RCC_Settings.ControllerType.Keyboard;

				EditorGUILayout.BeginVertical (GUI.skin.box);

				GUILayout.Label("Keyboard Settings", EditorStyles.boldLabel);

				EditorGUILayout.PropertyField(serializedObject.FindProperty("verticalInput"), new GUIContent("Gas/Reverse Input Axis"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("horizontalInput"), new GUIContent("Steering Input Axis"));
				GUI.color = new Color(.75f, 1f, .75f);
				EditorGUILayout.HelpBox("You can edit your vertical and horizontal input axis in Edit --> Project Settings --> Input.", MessageType.Info);
				GUI.color = originalGUIColor;
				EditorGUILayout.PropertyField(serializedObject.FindProperty("startEngineKB"), new GUIContent("Start/Stop Engine Key"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("lowBeamHeadlightsKB"), new GUIContent("Low Beam Headlights"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("highBeamHeadlightsKB"), new GUIContent("High Beam Headlights"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("changeCameraKB"), new GUIContent("Change Camera"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("rightIndicatorKB"), new GUIContent("Indicator Right"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("leftIndicatorKB"), new GUIContent("Indicator Left"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("hazardIndicatorKB"), new GUIContent("Indicator Hazard"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shiftGearUp"), new GUIContent("Gear Shift Up"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shiftGearDown"), new GUIContent("Gear Shift Down"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("NGear"), new GUIContent("N Gear"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("boostKB"), new GUIContent("Boost"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("slowMotionKB"), new GUIContent("Slow Motion"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("recordKB"), new GUIContent("Record"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("playbackKB"), new GUIContent("Playback"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("lookBackKB"), new GUIContent("Look Back"));
				EditorGUILayout.Space();

				EditorGUILayout.EndVertical ();

		}
				
		if(RCCSettingsAsset.toolbarSelectedIndex == 1){

			EditorGUILayout.BeginVertical (GUI.skin.box);

			RCCSettingsAsset.controllerType = RCC_Settings.ControllerType.Mobile;

			if (GUILayout.Button ("Enable Mobile Input For FPS/TPS Character Controllers"))
				SetEnabled("MOBILE_INPUT", true);

			if (GUILayout.Button ("Disable Mobile Input For FPS/TPS Character Controllers"))
				SetEnabled("MOBILE_INPUT", false);

			GUILayout.Label("Mobile Settings", EditorStyles.boldLabel);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("uiType"), new GUIContent("UI Type"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("mobileController"), new GUIContent("Mobile Controller"));

			GUI.color = new Color(.75f, 1f, .75f);
			EditorGUILayout.HelpBox("All UI/NGUI buttons will feed the vehicles at runtime.", MessageType.Info);
			GUI.color = originalGUIColor;

			EditorGUILayout.PropertyField(serializedObject.FindProperty("UIButtonSensitivity"), new GUIContent("UI Button Sensitivity"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("UIButtonGravity"), new GUIContent("UI Button Gravity"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("gyroSensitivity"), new GUIContent("Gyro Sensitivity"));

			EditorGUILayout.Space();
			
			GUI.color = new Color(.75f, 1f, .75f);
			EditorGUILayout.HelpBox("You can enable/disable Accelerometer in your game by just calling ''RCCSettings.Instance.useAccelerometerForSteering = true/false;''.", MessageType.Info);
			EditorGUILayout.HelpBox("You can enable/disable Steering Wheel Controlling in your game by just calling ''RCCSettings.Instance.useSteeringWheelForSteering = true/false;''.", MessageType.Info);
			GUI.color = originalGUIColor;
			EditorGUILayout.Space();

			EditorGUILayout.EndVertical ();

		}

		if(RCCSettingsAsset.toolbarSelectedIndex == 2){

				EditorGUILayout.BeginVertical (GUI.skin.box);

			RCCSettingsAsset.controllerType = RCC_Settings.ControllerType.Custom;

				GUILayout.Label("Custom Input Settings", EditorStyles.boldLabel);

				GUI.color = new Color(.75f, 1f, .75f);
				EditorGUILayout.HelpBox("In this mode, RCC won't receive these inputs from keyboard or UI buttons. You need to feed these inputs in your own script.", MessageType.Info);
				EditorGUILayout.Space();
				EditorGUILayout.HelpBox("RCC uses these inputs; \n  \n    gasInput = Clamped 0f - 1f.  \n    brakeInput = Clamped 0f - 1f.  \n    steerInput = Clamped -1f - 1f. \n    clutchInput = Clamped 0f - 1f. \n    handbrakeInput = Clamped 0f - 1f. \n    boostInput = Clamped 0f - 1f.", MessageType.Info);
				EditorGUILayout.Space();
				GUI.color = originalGUIColor;

				EditorGUILayout.EndVertical ();
			
		}

//			EnableReWired = EditorGUILayout.ToggleLeft(new GUIContent("Enable ReWired", "It will enable ReWired support for RCC. Be sure you have imported latest ReWired to your project before enabling this."), EnableReWired);

			EditorGUILayout.Space();

//			if (!EnableReWired) {
//
//				GUI.color = new Color(.75f, .75f, 0f);
//				EditorGUILayout.HelpBox ("It will enable ReWired support for RCC. Be sure you have imported latest ReWired to your project before enabling this.", MessageType.Warning);
//				GUI.color = originalGUIColor;
//
//			} else {
//
////				EditorGUILayout.BeginVertical (GUI.skin.box);
////
////				GUILayout.Label("ReWired Settings", EditorStyles.boldLabel);
////
////				#if RTC_REWIRED
////				GUI.color = new Color(.75f, 1f, .75f);
////				EditorGUILayout.HelpBox("These input strings must be exactly same with your ReWired Inputs. You can edit them from ''ReWired Input Manager'' on your scene.", MessageType.Info);
////				GUI.color = originalGUIColor;
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_gasInput"), new GUIContent("Gas / Reverse Input Axis"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_steerInput"), new GUIContent("Steering Input Axis"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_mainGunXInput"), new GUIContent("Main Gun X Input Axis"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_mainGunYInput"), new GUIContent("Main Gun Y Input Axis"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_startEngineKB"), new GUIContent("Start / Stop Engine Key"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_handbrakeKB"), new GUIContent("Handbrake Key"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_headlightsKB"), new GUIContent("Toggle Headlights"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_changeCameraKB"), new GUIContent("Change Camera"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_enterExitVehicleKB"), new GUIContent("Get In & Get Out Of The Vehicle"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_fireKB"), new GUIContent("Fire"));
////				EditorGUILayout.PropertyField(serializedObject.FindProperty("RW_changeAmmunation"), new GUIContent("Change Ammunation"));
////				#endif
////
////				EditorGUILayout.Space();
////
////				EditorGUILayout.EndVertical ();
//
//			}

			EditorGUILayout.BeginVertical(GUI.skin.box);

			GUILayout.Label("Main Controller Settings", EditorStyles.boldLabel);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("units"), new GUIContent("Units"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("useAutomaticGear"), new GUIContent("Use Automatic Gear"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("runEngineAtAwake"), new GUIContent("Engines Are Running At Awake"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("autoReverse"), new GUIContent("Auto Reverse"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("autoReset"), new GUIContent("Auto Reset"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("contactParticles"), new GUIContent("Contact Particles On Collision"));

			EditorGUILayout.EndVertical ();

		EditorGUILayout.EndVertical ();




		}

		EditorGUILayout.Space();

		foldUISettings = EditorGUILayout.Foldout(foldUISettings, "UI Settings");

		if(foldUISettings){
			
			EditorGUILayout.BeginVertical (GUI.skin.box);
			GUILayout.Label("UI Dashboard Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("uiType"), new GUIContent("UI Type"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("useTelemetry"), new GUIContent("Use Telemetry"));
			EditorGUILayout.Space();
			EditorGUILayout.EndVertical ();

		}

		EditorGUILayout.Space();

		foldWheelPhysics = EditorGUILayout.Foldout(foldWheelPhysics, "Wheel Physics Settings");

		if(foldWheelPhysics){

			if(RCC_GroundMaterials.Instance.frictions != null && RCC_GroundMaterials.Instance.frictions.Length > 0){

					EditorGUILayout.BeginVertical (GUI.skin.box);
					GUILayout.Label("Ground Physic Materials", EditorStyles.boldLabel);

					physicMaterials = new PhysicMaterial[RCC_GroundMaterials.Instance.frictions.Length];
					
					for (int i = 0; i < physicMaterials.Length; i++) {
						physicMaterials[i] = RCC_GroundMaterials.Instance.frictions[i].groundMaterial;
						EditorGUILayout.BeginVertical(GUI.skin.box);
						EditorGUILayout.ObjectField("Ground Physic Materials " + i, physicMaterials[i], typeof(PhysicMaterial), false);
						EditorGUILayout.EndVertical();
					}

					EditorGUILayout.Space();

			}

			GUI.color = new Color(.5f, 1f, 1f, 1f);
			
			if(GUILayout.Button("Configure Ground Physic Materials")){
				Selection.activeObject = Resources.Load("RCC Assets/RCC_GroundMaterials") as RCC_GroundMaterials;
			}

			GUI.color = originalGUIColor;

			EditorGUILayout.EndVertical ();

		}

		EditorGUILayout.Space();

		foldSFX = EditorGUILayout.Foldout(foldSFX, "SFX Settings");

		if(foldSFX){

			EditorGUILayout.BeginVertical(GUI.skin.box);

			GUILayout.Label("Sound FX", EditorStyles.boldLabel);

			EditorGUILayout.Space();
			GUI.color = new Color(.5f, 1f, 1f, 1f);
			if(GUILayout.Button("Configure Wheel Slip Sounds")){
				Selection.activeObject = Resources.Load("RCC Assets/RCC_GroundMaterials") as RCC_GroundMaterials;
			}
			GUI.color = originalGUIColor;
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("crashClips"), new GUIContent("Crashing Sounds"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("gearShiftingClips"), new GUIContent("Gear Shifting Sounds"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("indicatorClip"), new GUIContent("Indicator Clip"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustFlameClips"), new GUIContent("Exhaust Flame Clips"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("NOSClip"), new GUIContent("NOS Clip"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("turboClip"), new GUIContent("Turbo Clip"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("blowoutClip"), new GUIContent("Blowout Clip"), true);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("reversingClip"), new GUIContent("Reverse Transmission Sound"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("windClip"), new GUIContent("Wind Sound"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("brakeClip"), new GUIContent("Brake Sound"), true);
			EditorGUILayout.Separator();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxGearShiftingSoundVolume"), new GUIContent("Max Gear Shifting Sound Volume"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxCrashSoundVolume"), new GUIContent("Max Crash Sound Volume"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxWindSoundVolume"), new GUIContent("Max Wind Sound Volume"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxBrakeSoundVolume"), new GUIContent("Max Brake Sound Volume"), true);

			EditorGUILayout.EndVertical();

		}

		EditorGUILayout.Space();

		foldOptimization = EditorGUILayout.Foldout(foldOptimization, "Optimization");

		if(foldOptimization){

			EditorGUILayout.BeginVertical(GUI.skin.box);

			GUILayout.Label("Optimization", EditorStyles.boldLabel);

			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("useLightsAsVertexLights"), new GUIContent("Use Lights As Vertex Lights On Vehicles"));
			GUI.color = new Color(.75f, 1f, .75f);
			EditorGUILayout.HelpBox("Always use vertex lights for mobile platform. Even only one pixel light will drop your performance dramaticaly!", MessageType.Info);
			GUI.color = originalGUIColor;
			EditorGUILayout.PropertyField(serializedObject.FindProperty("useLightProjectorForLightingEffect"), new GUIContent("Use Light Projector For Lighting Effect"));
			GUI.color = new Color(.75f, .75f, 0f);
			EditorGUILayout.HelpBox("Unity's Projector will be used for lighting effect. Be sure it effects to your road only. Select ignored layers below this section. Don't let projectors hits the vehicle itself. It may increase your drawcalls if it hits unnecessary high numbered materials. It should just hit the road, nothing else.", MessageType.Warning);
			GUI.color = originalGUIColor;
			EditorGUILayout.PropertyField(serializedObject.FindProperty("projectorIgnoreLayer"), new GUIContent("Light Projector Ignore Layer"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("useSharedAudioSources"), new GUIContent("Use Shared Audio Sources", "For ex, 4 Audio Sources will be created for each wheel. This option merges them to only 1 Audio Source."), true);
			GUI.color = new Color(.75f, 1f, .75f);
			EditorGUILayout.HelpBox("For ex, 4 Audio Sources will be created for each wheelslip SFX. This option merges them to only 1 Audio Source.", MessageType.Info);
			GUI.color = originalGUIColor;
			EditorGUILayout.PropertyField(serializedObject.FindProperty("dontUseAnyParticleEffects"), new GUIContent("Do Not Use Any Particle Effects"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("dontUseSkidmarks"), new GUIContent("Do Not Use Skidmarks"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("dontUseChassisJoint"), new GUIContent("Do Not Use Chassis Joint"));
			GUI.color = new Color(.75f, 1f, .75f);
			if(!RCCSettingsAsset.dontUseChassisJoint)
				EditorGUILayout.HelpBox("Chassis Joint is a main Configurable Joint for realistic body movements. Script is getting all colliders attached to chassis, and moves them outside to joint. It can be trouble if you are making game about interacting objects inside the vehicle. If you don't want to use it, chassis will be simulated based on rigid velocity and angular velocity, like older versions of RCC.", MessageType.Info);
			GUI.color = originalGUIColor;
			EditorGUILayout.Space();

			EditorGUILayout.EndVertical();

		}

		foldTagsAndLayers = EditorGUILayout.Foldout(foldTagsAndLayers, "Tags & Layers");

		if (foldTagsAndLayers) {

			EditorGUILayout.BeginVertical (GUI.skin.box);

			GUILayout.Label ("Tags & Layers", EditorStyles.boldLabel);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("setTagsAndLayers"), new GUIContent("Set Tags And Layers Auto"), false);

			if (RCCSettingsAsset.setTagsAndLayers) {

				EditorGUILayout.PropertyField (serializedObject.FindProperty ("RCCLayer"), new GUIContent ("Vehicle Layer"), false);
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("RCCTag"), new GUIContent ("Vehicle Tag"), false);
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("tagAllChildrenGameobjects"), new GUIContent ("Tag All Children Gameobjects"), false);
				GUI.color = new Color (.75f, 1f, .75f);
				EditorGUILayout.HelpBox ("Be sure you have that tag and layer in your Tags & Layers", MessageType.Warning);
				EditorGUILayout.HelpBox ("All vehicles powered by Realistic Car Controller are using this layer. What does this layer do? It was used for masking wheel rays, light masks, and projector masks. Just create a new layer for vehicles from Edit --> Project Settings --> Tags & Layers, and select the layer here.", MessageType.Info);
				GUI.color = originalGUIColor;

			}

			EditorGUILayout.Space();
			EditorGUILayout.EndVertical();

		}

		EditorGUILayout.BeginVertical (GUI.skin.box);

		GUILayout.Label ("Resources", EditorStyles.boldLabel);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("headLights"), new GUIContent("Head Lights"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("brakeLights"), new GUIContent("Brake Lights"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("reverseLights"), new GUIContent("Reverse Lights"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("indicatorLights"), new GUIContent("Indicator Lights"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("mirrors"), new GUIContent("Mirrors"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("skidmarksManager"), new GUIContent("Skidmarks Manager"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("projector"), new GUIContent("Light Projector"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustGas"), new GUIContent("Exhaust Gas"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("chassisJoint"), new GUIContent("Chassis Joint"), false);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("RCCMainCamera"), new GUIContent("RCC Main Camera"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("hoodCamera"), new GUIContent("Hood Camera"), false);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("cinematicCamera"), new GUIContent("Cinematic Camera"), false);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("RCCCanvas"), new GUIContent("RCC UI Canvas"), false);

		EditorGUILayout.Space();

		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();

		EditorGUILayout.EndScrollView();
		
		EditorGUILayout.Space();

		EditorGUILayout.BeginVertical (GUI.skin.button);

		GUI.color = new Color(.75f, 1f, .75f);

		GUI.color = new Color(.5f, 1f, 1f, 1f);
		
		if(GUILayout.Button("Reset To Defaults")){
			ResetToDefaults();
			Debug.Log("Resetted To Defaults!");
		}
		
		if(GUILayout.Button("Open PDF Documentation")){
			string url = "http://www.bonecrackergames.com/realistic-car-controller";
			Application.OpenURL(url);
		}

		GUI.color = originalGUIColor;
		
		EditorGUILayout.LabelField("Realistic Car Controller " + RCCSettingsAsset.RCCVersion + " \nBoneCracker Games", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));

		EditorGUILayout.LabelField("Created by Buğra Özdoğanlar", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));

		EditorGUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();
		
		if(GUI.changed)
			EditorUtility.SetDirty(RCCSettingsAsset);

	}

	void ResetToDefaults(){

		RCCSettingsAsset.overrideFixedTimeStep = true;
		RCCSettingsAsset.fixedTimeStep = .02f;
		RCCSettingsAsset.maxAngularVelocity = 6f;
		RCCSettingsAsset.behaviorType = RCC_Settings.BehaviorType.Custom;

		RCCSettingsAsset.verticalInput = "Vertical";
		RCCSettingsAsset.horizontalInput = "Horizontal";
		RCCSettingsAsset.handbrakeKB = KeyCode.Space;
		RCCSettingsAsset.startEngineKB = KeyCode.I;
		RCCSettingsAsset.lowBeamHeadlightsKB = KeyCode.L;
		RCCSettingsAsset.highBeamHeadlightsKB = KeyCode.K;
		RCCSettingsAsset.rightIndicatorKB = KeyCode.E;
		RCCSettingsAsset.leftIndicatorKB = KeyCode.Q;
		RCCSettingsAsset.hazardIndicatorKB = KeyCode.Z;
		RCCSettingsAsset.shiftGearUp = KeyCode.LeftShift;
		RCCSettingsAsset.shiftGearDown = KeyCode.LeftControl;
		RCCSettingsAsset.NGear = KeyCode.N;
		RCCSettingsAsset.boostKB = KeyCode.F;
		RCCSettingsAsset.slowMotionKB = KeyCode.G;
		RCCSettingsAsset.changeCameraKB = KeyCode.C;
		RCCSettingsAsset.recordKB = KeyCode.R;
		RCCSettingsAsset.playbackKB = KeyCode.P;

		RCCSettingsAsset.useAutomaticGear = true;
		RCCSettingsAsset.runEngineAtAwake = true;
		RCCSettingsAsset.autoReverse = true;
		RCCSettingsAsset.autoReset = true;
		RCCSettingsAsset.units = RCC_Settings.Units.KMH;
		RCCSettingsAsset.uiType = RCC_Settings.UIType.UI;
		RCCSettingsAsset.useTelemetry = false;
		RCCSettingsAsset.mobileController = RCC_Settings.MobileController.TouchScreen;
		RCCSettingsAsset.UIButtonSensitivity = 3f;
		RCCSettingsAsset.UIButtonGravity = 5f;
		RCCSettingsAsset.gyroSensitivity = 2f;
		RCCSettingsAsset.useLightsAsVertexLights = true;
		RCCSettingsAsset.useLightProjectorForLightingEffect = false;
		RCCSettingsAsset.setTagsAndLayers = true;
		RCCSettingsAsset.RCCLayer = "RCC";
		RCCSettingsAsset.RCCTag = "Player";
		RCCSettingsAsset.tagAllChildrenGameobjects = false;
		RCCSettingsAsset.dontUseAnyParticleEffects = false;
		RCCSettingsAsset.dontUseChassisJoint = false;
		RCCSettingsAsset.dontUseSkidmarks = false;
		RCCSettingsAsset.useSharedAudioSources = true;
		RCCSettingsAsset.maxGearShiftingSoundVolume = .25f;
		RCCSettingsAsset.maxCrashSoundVolume = 1f;
		RCCSettingsAsset.maxWindSoundVolume = .1f;
		RCCSettingsAsset.maxBrakeSoundVolume = .1f;
		RCCSettingsAsset.foldGeneralSettings = false;
		RCCSettingsAsset.foldControllerSettings = false;
		RCCSettingsAsset.foldUISettings = false;
		RCCSettingsAsset.foldWheelPhysics = false;
		RCCSettingsAsset.foldSFX = false;
		RCCSettingsAsset.foldOptimization = false;
		RCCSettingsAsset.foldTagsAndLayers = false;

	}

	private static BuildTargetGroup[] buildTargetGroups = new BuildTargetGroup[]
	{

		BuildTargetGroup.Standalone,
		BuildTargetGroup.Android,
		BuildTargetGroup.iOS,
		BuildTargetGroup.WebGL,
		BuildTargetGroup.Facebook,
		BuildTargetGroup.N3DS,
		BuildTargetGroup.XboxOne,
		BuildTargetGroup.PS4,
		BuildTargetGroup.PSP2,
		BuildTargetGroup.PSM,
		BuildTargetGroup.tvOS,
		BuildTargetGroup.SamsungTV,
		BuildTargetGroup.Tizen,
		BuildTargetGroup.Switch,
		BuildTargetGroup.WiiU,
		BuildTargetGroup.WSA

	};

	private static void SetEnabled(string defineName, bool enable)
	{
		//Debug.Log("setting "+defineName+" to "+enable);
		foreach (var group in buildTargetGroups)
		{
			var defines = GetDefinesList(group);
			if (enable)
			{
				if (defines.Contains(defineName))
				{
					return;
				}
				defines.Add(defineName);
			}
			else
			{
				if (!defines.Contains(defineName))
				{
					return;
				}
				while (defines.Contains(defineName))
				{
					defines.Remove(defineName);
				}
			}
			string definesString = string.Join(";", defines.ToArray());
			PlayerSettings.SetScriptingDefineSymbolsForGroup(group, definesString);
		}
	}

	private static List<string> GetDefinesList(BuildTargetGroup group)
	{
		return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';'));
	}

}
