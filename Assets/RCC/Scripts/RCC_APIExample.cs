using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// An example script to show how the RCC API works.
///</summary>
public class RCC_APIExample : MonoBehaviour {

	public RCC_CarControllerV3 spawnVehiclePrefab;			// Vehicle prefab we gonna spawn.
	private RCC_CarControllerV3 currentVehiclePrefab;		// Spawned vehicle.
	public Transform spawnTransform;								// Spawn transform.

	public bool playerVehicle;			// Spawn as a player vehicle?
	public bool controllable;			// Spawn as controllable vehicle?
	public bool engineRunning;		// Spawn with running engine?

	public void Spawn(){

		// Spawning the vehicle with given settings.
		currentVehiclePrefab = RCC.SpawnRCC (spawnVehiclePrefab, spawnTransform.position, spawnTransform.rotation, playerVehicle, controllable, engineRunning);

	}

	public void SetPlayer(){

		// Registers the vehicle as player vehicle.
		RCC.RegisterPlayerVehicle (currentVehiclePrefab);

	}

	public void SetControl(bool control){

		// Enables / disables controllable state of the vehicle.
		RCC.SetControl (currentVehiclePrefab, control);

	}

	public void SetEngine(bool engine){

		// Starts / kills engine of the vehicle.
		RCC.SetEngine (currentVehiclePrefab, engine);

	}

	public void DeRegisterPlayer(){

		// Deregisters the vehicle from as player vehicle.
		RCC.DeRegisterPlayerVehicle ();

	}

}
