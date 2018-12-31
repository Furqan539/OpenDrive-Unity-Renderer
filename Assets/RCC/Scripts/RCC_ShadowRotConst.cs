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
/// Locks rotation of the shadow projector to avoid stretching.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Shadow")]
public class RCC_ShadowRotConst : MonoBehaviour {

	private Transform root;

	void Start () {

		root = GetComponentInParent<RCC_CarControllerV3>().transform;
	
	}

	void Update () {

		transform.rotation = Quaternion.Euler(90, root.eulerAngles.y, 0);
	
	}

}
