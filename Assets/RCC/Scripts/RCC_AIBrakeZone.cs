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
/// Brake Zones are meant to be used for slowing AI vehicles. If you have a sharp turn on your scene, you can simply use one of these Brake Zones. It has a target speed. AI will adapt its speed to this target speed while in this Brake Zone. It's simple.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/AI/RCC AI Brake Zone")]
public class RCC_AIBrakeZone : MonoBehaviour {
	
	public float targetSpeed = 50;
	
}
