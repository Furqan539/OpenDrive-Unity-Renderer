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

/// <summary>
/// Record / Replay system. Saves player's input on record, and replays it when on playback.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Recorder")]
public class RCC_Recorder : MonoBehaviour {

	public RCC_CarControllerV3 carController;

	public List <PlayerInput> Inputs = new List<PlayerInput>();
	public List <PlayerTransform> Transforms = new List<PlayerTransform>();
	public List <PlayerRigidBody> RigidBodies = new List<PlayerRigidBody>();

	public class PlayerInput{

		public float gasInput = 0f;
		public float brakeInput = 0f;
		public float steerInput = 0f;
		public float handbrakeInput = 0f;
		public float clutchInput = 0f;
		public float boostInput = 0f;
		public float idleInput = 0f;
		public float fuelInput = 0f;
		public int direction = 1;
		public bool canGoReverse = false;
		public int currentGear = 0;
		public bool changingGear = false;

		public RCC_CarControllerV3.IndicatorsOn indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;
		public bool lowBeamHeadLightsOn = false;
		public bool highBeamHeadLightsOn = false;

		public  PlayerInput(float _gasInput, float _brakeInput, float _steerInput, float _handbrakeInput, float _clutchInput, float _boostInput, float _idleInput, float _fuelInput, int _direction, bool _canGoReverse, int _currentGear, bool _changingGear, RCC_CarControllerV3.IndicatorsOn _indicatorsOn, bool _lowBeamHeadLightsOn, bool _highBeamHeadLightsOn){
			
			gasInput = _gasInput;
			brakeInput = _brakeInput;
			steerInput = _steerInput;
			handbrakeInput = _handbrakeInput;
			clutchInput = _clutchInput;
			boostInput = _boostInput;
			idleInput = _idleInput;
			fuelInput = _fuelInput;
			direction = _direction;
			canGoReverse = _canGoReverse;
			currentGear = _currentGear;
			changingGear = _changingGear;

			indicatorsOn = _indicatorsOn;
			lowBeamHeadLightsOn = _lowBeamHeadLightsOn;
			highBeamHeadLightsOn = _highBeamHeadLightsOn;

		}

	}

	public class PlayerTransform{

		public Vector3 position;
		public Quaternion rotation;

		public PlayerTransform(Vector3 _pos, Quaternion _rot){

			position = _pos;
			rotation = _rot;

		}

	}

	public class PlayerRigidBody{

		public Vector3 velocity;
		public Vector3 angularVelocity;

		public PlayerRigidBody(Vector3 _vel, Vector3 _angVel){

			velocity = _vel;
			angularVelocity = _angVel;

		}

	}

	public enum Mode{Neutral, Play, Record}
	public Mode mode;

	public void Record(){
		
		if (mode != Mode.Record)
			mode = Mode.Record;
		else
			mode = Mode.Neutral;

		if(mode == Mode.Record){

			Inputs.Clear();
			Transforms.Clear ();
			RigidBodies.Clear ();

		}

	}

	public void Play(){

		if (Inputs == null || Transforms  == null || RigidBodies  == null)
			return;

		if (mode != Mode.Play)
			mode = Mode.Play;
		else
			mode = Mode.Neutral;

		if (mode == Mode.Play)
			carController.externalController = true;
		else
			carController.externalController = false;

		if(mode == Mode.Play){

			StartCoroutine(Replay());
			if (Transforms.Count > 0) {
				carController.transform.position = Transforms [0].position;
				carController.transform.rotation = Transforms [0].rotation;
			}
			StartCoroutine(Revel());

		}

	}

	public void Stop(){

		mode = Mode.Neutral;
		carController.externalController = false;

	}

	private IEnumerator Replay(){
		
		for(int i = 0; i<Inputs.Count && mode == Mode.Play; i++){
			
			carController.externalController = true;
			carController.gasInput = Inputs[i].gasInput;
			carController.brakeInput = Inputs[i].brakeInput;
			carController.steerInput = Inputs[i].steerInput;
			carController.handbrakeInput = Inputs[i].handbrakeInput;
			carController.clutchInput = Inputs[i].clutchInput;
			carController.boostInput = Inputs[i].boostInput;
			carController.idleInput = Inputs[i].idleInput;
			carController.fuelInput = Inputs[i].fuelInput;
			carController.direction = Inputs[i].direction;
			carController.canGoReverseNow = Inputs[i].canGoReverse;
			carController.currentGear = Inputs[i].currentGear;
			carController.changingGear = Inputs[i].changingGear;

			carController.indicatorsOn = Inputs[i].indicatorsOn;
			carController.lowBeamHeadLightsOn = Inputs[i].lowBeamHeadLightsOn;
			carController.highBeamHeadLightsOn = Inputs[i].highBeamHeadLightsOn;

			yield return new WaitForFixedUpdate();

		}
			
		mode = Mode.Neutral;

		carController.externalController = false;

	}

	private IEnumerator Repos(){

		for(int i = 0; i<Transforms.Count && mode == Mode.Play; i++){

			carController.transform.position = Transforms [i].position;
			carController.transform.rotation = Transforms [i].rotation;

			yield return new WaitForEndOfFrame();

		}

		mode = Mode.Neutral;

		carController.externalController = false;

	}

	private IEnumerator Revel(){

		for(int i = 0; i<RigidBodies.Count && mode == Mode.Play; i++){

			carController.rigid.velocity = RigidBodies [i].velocity;
			carController.rigid.angularVelocity = RigidBodies [i].angularVelocity;

			yield return new WaitForFixedUpdate();

		}

		mode = Mode.Neutral;

		carController.externalController = false;

	}
		
	void FixedUpdate () {

		carController = RCC_SceneManager.Instance.activePlayerVehicle;

		if (!carController)
			return;

		switch (mode) {

		case Mode.Neutral:
			
			break;

		case Mode.Play:

			carController.externalController = true;

			break;

		case Mode.Record:

			Inputs.Add(new PlayerInput(carController.gasInput, carController.brakeInput, carController.steerInput, carController.handbrakeInput, carController.clutchInput, carController.boostInput, carController.idleInput, carController.fuelInput, carController.direction, carController.canGoReverseNow, carController.currentGear, carController.changingGear, carController.indicatorsOn, carController.lowBeamHeadLightsOn, carController.highBeamHeadLightsOn));
			Transforms.Add (new PlayerTransform(carController.transform.position, carController.transform.rotation));
			RigidBodies.Add(new PlayerRigidBody(carController.rigid.velocity, carController.rigid.angularVelocity));

			break;

		}

	}

}
