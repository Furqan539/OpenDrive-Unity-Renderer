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
/// Used for holding a list for brake zones, and drawing gizmos for all of them.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/AI/RCC AI Brake Zones Container")]
public class RCC_AIBrakeZonesContainer : MonoBehaviour {
	
	public List<Transform> brakeZones = new List<Transform>();		// Brake Zones list.

	// Used for drawing gizmos on Editor.
	void OnDrawGizmos() {
		
		for(int i = 0; i < brakeZones.Count; i ++){

			Gizmos.matrix = brakeZones[i].transform.localToWorldMatrix;
			Gizmos.color = new Color(1f, 0f, 0f, .25f);
			Vector3 colliderBounds = brakeZones[i].GetComponent<BoxCollider>().size;

			Gizmos.DrawCube(Vector3.zero, colliderBounds);

		}
		
	}
	
}
