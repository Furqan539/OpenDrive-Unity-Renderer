//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AI Controller of RCC. It's not professional, but it does the job. Follows all waypoints, or chases the target gameobject.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/AI/RCC AI Car Controller")]
public class RCC_AICarController : MonoBehaviour {

	internal RCC_CarControllerV3 carController;		// Main RCC of this vehicle.
	private Rigidbody rigid;									// Rigidbody of this vehicle.

	public RCC_AIWaypointsContainer waypointsContainer;	// Waypoints Container.
	public int currentWaypoint = 0;											// Current index in Waypoint Container.
	public Transform targetChase;											// Target Gameobject for chasing.
	public string targetTag = "Player";									// Search and chase Gameobjects with tags.

	// AI Type
	public AIType _AIType;
	public enum AIType {FollowWaypoints, ChaseTarget}
	
	// Raycast distances used for detecting obstacles at front of our AI vehicle.
	public int wideRayLength = 20;
	public int tightRayLength = 20;
	public int sideRayLength = 3;
	public LayerMask obstacleLayers = -1;

	private float rayInput = 0f;				// Total ray input affected by raycast distances.
	private bool  raycasting = false;		// Raycasts hits an obstacle now?
	private float resetTime = 0f;				// This timer was used for deciding go back or not, after crashing.
	
	// Steer, Motor, And Brake inputs. Will feed RCC_CarController with these inputs.
	private float steerInput = 0f;
	private float gasInput = 0f;
	private float brakeInput = 0f;

	// Limit speed.
	public bool limitSpeed = false;
	public float maximumSpeed = 100f;

	// Smoothed steering.
	public bool smoothedSteer = true;
	
	// Brake zone.
	private float maximumSpeedInBrakeZone = 0f;
	private bool inBrakeZone = false;
	
	// Counts laps and how many waypoints were passed.
	public int lap = 0;
	public int totalWaypointPassed = 0;
	public int nextWaypointPassRadius = 40;
	public bool ignoreWaypointNow = false;
	
	// Unity's Navigator.
	private NavMeshAgent navigator;

	// Detector with Sphere Collider. Used for finding target Gameobjects in chasing mode.
	private GameObject detector;
	public float detectorRadius = 100f;
	public List<RCC_CarControllerV3> targetsInZone = new List<RCC_CarControllerV3> ();

	// Firing an event when each RCC AI vehicle spawned / enabled.
	public delegate void onRCCAISpawned(RCC_AICarController RCCAI);
	public static event onRCCAISpawned OnRCCAISpawned;

	// Firing an event when each RCC AI vehicle disabled / destroyed.
	public delegate void onRCCAIDestroyed(RCC_AICarController RCCAI);
	public static event onRCCAIDestroyed OnRCCAIDestroyed;

	void Start() {

		// Getting main controller.
		carController = GetComponent<RCC_CarControllerV3>();
		carController.externalController = true;

		// Getting main Rigidbody.
		rigid = GetComponent<Rigidbody>();

		// If Waypoints Container is not selected in Inspector Panel, find it on scene.
		if(!waypointsContainer)
			waypointsContainer = FindObjectOfType(typeof(RCC_AIWaypointsContainer)) as RCC_AIWaypointsContainer;

		// Creating our Navigator and setting properties.
		GameObject navigatorObject = new GameObject("Navigator");
		navigatorObject.transform.SetParent (transform, false);
		navigator = navigatorObject.AddComponent<NavMeshAgent>();
		navigator.radius = 1;
		navigator.speed = 1;
		navigator.angularSpeed = 100000f;
		navigator.acceleration = 100000f;
		navigator.height = 1;
		navigator.avoidancePriority = 99;

		// Creating our Detector and setting properties. Used for getting nearest target gameobjects.
		detector = new GameObject ("Detector");
		detector.transform.SetParent (transform, false);
		detector.gameObject.AddComponent<SphereCollider> ();
		detector.GetComponent<SphereCollider> ().isTrigger = true;
		detector.GetComponent<SphereCollider> ().radius = detectorRadius;

	}

	void OnEnable(){

		// Calling this event when AI vehicle spawned.
		if (OnRCCAISpawned != null)
			OnRCCAISpawned (this);

	}
	
	void Update(){

		// If not controllable, no need to go further.
		if(!carController.canControl)
			return;

		// Assigning navigator's position to front wheels of the vehicle.
		//navigator.transform.localPosition = new Vector3(0f, carController.FrontLeftWheelCollider.transform.localPosition.y, carController.FrontLeftWheelCollider.transform.localPosition.z);
		navigator.transform.position = transform.position;
		navigator.transform.position += transform.forward * carController.FrontLeftWheelCollider.transform.localPosition.z;

		// Removing unnecessary targets in list.
		for (int i = 0; i < targetsInZone.Count; i++) {

			if(targetsInZone [i] == null)
				targetsInZone.RemoveAt (i);

			if (!targetsInZone [i].gameObject.activeInHierarchy)
				targetsInZone.RemoveAt (i);
			else {

				if(Vector3.Distance(transform.position, targetsInZone[i].transform.position) > (detectorRadius * 1.25f))
					targetsInZone.RemoveAt (i);

			}

		}

		// If there is a target, get closest enemy.
		if (targetsInZone.Count > 0)
			targetChase = GetClosestEnemy(targetsInZone.ToArray());
		else
			targetChase = null;

	}
	
	void FixedUpdate (){

		// If not controllable, no need to go further.
		if(!carController.canControl)
			return;

		Navigation();			// Feeds steerInput based on navigator.
		FixedRaycasts();	// Affects steerInput if one of raycasts detects an object front of our AI vehicle.
		FeedRCC();			// Feeds motorInput.
		Resetting();			// Was used for deciding go back or not after crashing.

	}
	
	void Navigation (){

		// Navigator Input is multiplied by 1.5f for fast reactions.
		float navigatorInput = Mathf.Clamp(transform.InverseTransformDirection(navigator.desiredVelocity).x * 1.5f, -1f, 1f);

		switch (_AIType) {

		case AIType.FollowWaypoints:

			// If our scene doesn't have a Waypoint Container, return with error.
			if(!waypointsContainer){

				Debug.LogError("Waypoints Container Couldn't Found!");
				Stop();
				return;

			}

			// If our scene has Waypoints Container and it doesn't have any waypoints, return with error.
			if(waypointsContainer && waypointsContainer.waypoints.Count < 1){

				Debug.LogError("Waypoints Container Doesn't Have Any Waypoints!");
				Stop();
				return;

			}

			// Next waypoint's position.
			Vector3 nextWaypointPosition = transform.InverseTransformPoint(new Vector3(waypointsContainer.waypoints[currentWaypoint].position.x, transform.position.y, waypointsContainer.waypoints[currentWaypoint].position.z));

			// Setting destination of the Navigator. 
			if(navigator.isOnNavMesh)
				navigator.SetDestination (waypointsContainer.waypoints [currentWaypoint].position);

			// Checks for the distance to next waypoint. If it is less than written value, then pass to next waypoint.
			if (nextWaypointPosition.magnitude < nextWaypointPassRadius) {

				currentWaypoint++;
				totalWaypointPassed++;

				// If all waypoints were passed, sets the current waypoint to first waypoint and increase lap.
				if (currentWaypoint >= waypointsContainer.waypoints.Count) {
					
					currentWaypoint = 0;
					lap++;

				}

			}

			break;

		case AIType.ChaseTarget:

			// If our scene doesn't have a Waypoints Container, return with error.
			if(!targetChase){
				
				//Debug.LogError("Target Chase Couldn't Found!");
				Stop();
				return;
	
			}

			// Setting destination of the Navigator. 
			if(navigator.isOnNavMesh)
				navigator.SetDestination (targetChase.position);

			break;

		}

		// Gas Input.
		if(!inBrakeZone){

			if(carController.speed >= 10){

				if(!carController.changingGear)
					gasInput = Mathf.Clamp(1f - (Mathf.Abs(navigatorInput / 10f)  - Mathf.Abs(rayInput / 10f)), .75f, 1f);
				else
					gasInput = 0f;

			}else{

				if(!carController.changingGear)
					gasInput = 1f;
				else
					gasInput = 0f;

			}

		}else{

			if(!carController.changingGear)
				gasInput = Mathf.Lerp(1f, 0f, (carController.speed) / maximumSpeedInBrakeZone);
			else
				gasInput = 0f;

		}

		// Steer Input.
		steerInput = Mathf.Clamp((ignoreWaypointNow ? rayInput : navigatorInput + rayInput), -1f, 1f) * carController.direction;

		// Brake Input.
		if(!inBrakeZone){
			
			if(carController.speed >= 25)
				brakeInput = Mathf.Lerp(0f, .25f, (Mathf.Abs(steerInput)));
			else
				brakeInput = 0f;

		}else{
			
			brakeInput = Mathf.Lerp(0f, 1f, (carController.speed - maximumSpeedInBrakeZone) / maximumSpeedInBrakeZone);

		}
			
	}
		
	void Resetting (){

		// If unable to move forward, puts the gear to R.
		if(carController.speed <= 5 && transform.InverseTransformDirection(rigid.velocity).z < 1f)
			resetTime += Time.deltaTime;
		
		if(resetTime >= 2)
			carController.direction = -1;

		if(resetTime >= 4 || carController.speed >= 25){
			
			carController.direction = 1;
			resetTime = 0;

		}
		
	}
	
	void FixedRaycasts(){

		// Ray pivot position.
		Vector3 pivotPos = transform.position;
		pivotPos += transform.forward * carController.FrontLeftWheelCollider.transform.localPosition.z;

		RaycastHit hit;
		
		// New bools effected by fixed raycasts.
		bool  tightTurn = false;
		bool  wideTurn = false;
		bool  sideTurn = false;
		bool  tightTurn1 = false;
		bool  wideTurn1 = false;
		bool  sideTurn1 = false;
		
		// New input steers effected by fixed raycasts.
		float newinputSteer1 = 0f;
		float newinputSteer2 = 0f;
		float newinputSteer3 = 0f;
		float newinputSteer4 = 0f;
		float newinputSteer5 = 0f;
		float newinputSteer6 = 0f;
		
		// Drawing Rays.
		Debug.DrawRay (pivotPos, Quaternion.AngleAxis(25, transform.up) * transform.forward * wideRayLength, Color.white);
		Debug.DrawRay (pivotPos, Quaternion.AngleAxis(-25, transform.up) * transform.forward * wideRayLength, Color.white);
		
		Debug.DrawRay (pivotPos, Quaternion.AngleAxis(7, transform.up) * transform.forward * tightRayLength, Color.white);
		Debug.DrawRay (pivotPos, Quaternion.AngleAxis(-7, transform.up) * transform.forward * tightRayLength, Color.white);

		Debug.DrawRay (pivotPos, Quaternion.AngleAxis(90, transform.up) * transform.forward * sideRayLength, Color.white);
		Debug.DrawRay (pivotPos, Quaternion.AngleAxis(-90, transform.up) * transform.forward * sideRayLength, Color.white);
		
		// Wide Raycasts.
		if (Physics.Raycast (pivotPos, Quaternion.AngleAxis(25, transform.up) * transform.forward, out hit, wideRayLength, obstacleLayers) && !hit.collider.isTrigger && hit.transform.root != transform) {
			
			Debug.DrawRay (pivotPos, Quaternion.AngleAxis(25, transform.up) * transform.forward * wideRayLength, Color.red);
			newinputSteer1 = Mathf.Lerp (-.5f, 0f, (hit.distance / wideRayLength));
			wideTurn = true;

		}
		
		else{
			
			newinputSteer1 = 0f;
			wideTurn = false;

		}
		
		if (Physics.Raycast (pivotPos, Quaternion.AngleAxis(-25, transform.up) * transform.forward, out hit, wideRayLength, obstacleLayers) && !hit.collider.isTrigger && hit.transform.root != transform) {
			
			Debug.DrawRay (pivotPos, Quaternion.AngleAxis(-25, transform.up) * transform.forward * wideRayLength, Color.red);
			newinputSteer4 = Mathf.Lerp (.5f, 0f, (hit.distance / wideRayLength));
			wideTurn1 = true;

		}else{
			
			newinputSteer4 = 0f;
			wideTurn1 = false;

		}
		
		// Tight Raycasts.
		if (Physics.Raycast (pivotPos, Quaternion.AngleAxis(7, transform.up) * transform.forward, out hit, tightRayLength, obstacleLayers) && !hit.collider.isTrigger && hit.transform.root != transform) {
			
			Debug.DrawRay (pivotPos, Quaternion.AngleAxis(7, transform.up) * transform.forward * tightRayLength , Color.red);
			newinputSteer3 = Mathf.Lerp (-1f, 0f, (hit.distance / tightRayLength));
			tightTurn = true;

		}else{
			
			newinputSteer3 = 0f;
			tightTurn = false;

		}
		
		if (Physics.Raycast (pivotPos, Quaternion.AngleAxis(-7, transform.up) * transform.forward, out hit, tightRayLength, obstacleLayers) && !hit.collider.isTrigger && hit.transform.root != transform) {
			
			Debug.DrawRay (pivotPos, Quaternion.AngleAxis(-7, transform.up) * transform.forward * tightRayLength, Color.red);
			newinputSteer2 = Mathf.Lerp (1f, 0f, (hit.distance / tightRayLength));
			tightTurn1 = true;

		}else{
			
			newinputSteer2 = 0f;
			tightTurn1 = false;

		}

		// Side Raycasts.
		if (Physics.Raycast (pivotPos, Quaternion.AngleAxis(90, transform.up) * transform.forward, out hit, sideRayLength, obstacleLayers) && !hit.collider.isTrigger && hit.transform.root != transform) {
			
			Debug.DrawRay (pivotPos, Quaternion.AngleAxis(90, transform.up) * transform.forward * sideRayLength , Color.red);
			newinputSteer5 = Mathf.Lerp (-1f, 0f, (hit.distance / sideRayLength));
			sideTurn = true;

		}else{
			
			newinputSteer5 = 0f;
			sideTurn = false;

		}
		
		if (Physics.Raycast (pivotPos, Quaternion.AngleAxis(-90, transform.up) * transform.forward, out hit, sideRayLength, obstacleLayers) && !hit.collider.isTrigger && hit.transform.root != transform) {
			
			Debug.DrawRay (pivotPos, Quaternion.AngleAxis(-90, transform.up) * transform.forward * sideRayLength, Color.red);
			newinputSteer6 = Mathf.Lerp (1f, 0f, (hit.distance / sideRayLength));
			sideTurn1 = true;

		}else{
			
			newinputSteer6 = 0f;
			sideTurn1 = false;

		}

		// Raycasts hits an obstacle now?
		if(wideTurn || wideTurn1 || tightTurn || tightTurn1 || sideTurn || sideTurn1)
			raycasting = true;
		else
			raycasting = false;

		// If raycast hits a collider, feed rayInput.
		if(raycasting)
			rayInput = (newinputSteer1 + newinputSteer2 + newinputSteer3 + newinputSteer4 + newinputSteer5 + newinputSteer6);
		else
			rayInput = 0f;

		// If rayInput is too much, ignore navigator input.
		if(raycasting && Mathf.Abs(rayInput) > .5f)
			ignoreWaypointNow = true;
		else
			ignoreWaypointNow = false;
		
	}

	void FeedRCC(){

		// Feeding gasInput of the RCC.
		if(carController.direction == 1){
			if(!limitSpeed){
				carController.gasInput = gasInput;
			}else{
				carController.gasInput = gasInput * Mathf.Clamp01(Mathf.Lerp(10f, 0f, (carController.speed) / maximumSpeed));
			}
		}else{
			carController.gasInput = 0f;
		}

		// Feeding steerInput of the RCC.
		if(smoothedSteer)
			carController.steerInput = Mathf.Lerp(carController.steerInput, steerInput, Time.deltaTime * 20f);
		else
			carController.steerInput = steerInput;

		// Feeding brakeInput of the RCC.
		if(carController.direction == 1)
			carController.brakeInput = brakeInput;
		else
			carController.brakeInput = gasInput;

	}

	void Stop(){

		gasInput = 0f;
		steerInput = 0f;
		brakeInput = 1f;

	}
	
	void OnTriggerEnter (Collider col){
		
		// Checking if vehicle is in any brake zone on scene.
		if(col.gameObject.GetComponent<RCC_AIBrakeZone>()){
			
			inBrakeZone = true;
			maximumSpeedInBrakeZone = col.gameObject.GetComponent<RCC_AIBrakeZone>().targetSpeed;

		}

		if(col.attachedRigidbody != null && col.gameObject.GetComponentInParent<RCC_CarControllerV3>() && col.gameObject.GetComponentInParent<RCC_CarControllerV3>().transform.CompareTag(targetTag)){
			
			if (!targetsInZone.Contains (col.gameObject.GetComponentInParent<RCC_CarControllerV3> ()))
				targetsInZone.Add (col.gameObject.GetComponentInParent<RCC_CarControllerV3> ());

		}

	}

	void OnTriggerExit (Collider col){

		if(col.gameObject.GetComponent<RCC_AIBrakeZone>()){
			
			inBrakeZone = false;
			maximumSpeedInBrakeZone = 0;

		}

	}

	Transform GetClosestEnemy (RCC_CarControllerV3[] enemies){

		Transform bestTarget = null;

		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;

		foreach(RCC_CarControllerV3 potentialTarget in enemies){

			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;

			if(dSqrToTarget < closestDistanceSqr){

				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget.transform;

			}

		}

		return bestTarget;

	}

	void OnDestroy(){

		// Calling this event when AI vehicle is destroyed.
		if (OnRCCAIDestroyed != null)
			OnRCCAIDestroyed (this);

	}
	
}