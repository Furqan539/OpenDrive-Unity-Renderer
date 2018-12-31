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

/// <summary>
/// Animates Driver Sofie (Credits to 3DMaesen). Simply feeds floats and bools of Sofie's animator component.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Animator Controller")]
public class RCC_CharacterController : MonoBehaviour {

	private RCC_CarControllerV3 carController;
	private Rigidbody carRigid;
	public Animator animator;

	// String parameters of animator.
	public string driverSteeringParameter;
	public string driverShiftingGearParameter;
	public string driverDangerParameter;
	public string driverReversingParameter;

	// Inputs for feeding animator.
	public float steerInput = 0f;
	public float directionInput = 0f;
	public bool reversing = false;
	public float impactInput = 0f;
	public float gearInput = 0f;

	void Start () {

		if(!animator)
			animator = GetComponentInChildren<Animator>();
		
		carController = GetComponent<RCC_CarControllerV3>();
		carRigid = GetComponent<Rigidbody>();
		
	}

	void Update () {

		steerInput = Mathf.Lerp(steerInput, carController.steerInput, Time.deltaTime * 5f);
		directionInput = carRigid.transform.InverseTransformDirection(carRigid.velocity).z;
		impactInput -= Time.deltaTime * 5f;

		if(impactInput < 0)
			impactInput = 0f;
		if(impactInput > 1)
			impactInput = 1f;

		if(directionInput <= -2f)
			reversing = true;
		else if(directionInput > -1f)
			reversing = false;

		if(carController.changingGear)
			gearInput = 1f;
		else
			gearInput -= Time.deltaTime * 5f;

		if(gearInput < 0)
			gearInput = 0f;
		if(gearInput > 1)
			gearInput = 1f;

		if(!reversing){
			animator.SetBool(driverReversingParameter, false);
		}else{
			animator.SetBool(driverReversingParameter, true);
		}

		if(impactInput > .5f){
			animator.SetBool(driverDangerParameter, true);
		}else{
			animator.SetBool(driverDangerParameter, false);
		}

		if(gearInput > .5f){
			animator.SetBool(driverShiftingGearParameter, true);
		}else{
			animator.SetBool(driverShiftingGearParameter, false);
		}

		animator.SetFloat(driverSteeringParameter, steerInput);
		
	}

	void OnCollisionEnter(Collision col){

		if(col.relativeVelocity.magnitude < 2.5f)
			return;

		impactInput = 1f;

	}

}
