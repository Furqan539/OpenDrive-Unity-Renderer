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
/// It must be attached to external camera. This external camera will be used as mirror.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Mirror")]
public class RCC_Mirror : MonoBehaviour {

	private Camera cam;
	private RCC_CarControllerV3 carController;
	
	void InvertCamera () {

		cam = GetComponent<Camera>();
		cam.ResetWorldToCameraMatrix ();
		cam.ResetProjectionMatrix ();
		cam.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
		carController = GetComponentInParent<RCC_CarControllerV3>();

	}
	
	void OnPreRender () {
		GL.invertCulling = true;
	}
	
	void OnPostRender () {
		GL.invertCulling = false;
	}

	void Update(){

		if(!cam){
			InvertCamera();
			return;
		}

		cam.enabled = carController.canControl;

	}

}
