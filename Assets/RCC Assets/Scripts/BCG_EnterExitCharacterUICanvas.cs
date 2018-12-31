using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enter Exit for BCG Vehicles. UI canvas of the FPS / TPS character controller.
/// </summary>
[AddComponentMenu("BoneCracker Games/Shared Assets/Enter-Exit/BCG Enter Exit Script For UI Canvas")]
public class BCG_EnterExitCharacterUICanvas : MonoBehaviour {

	public DisplayType displayType = DisplayType.OnFoot;
	public enum DisplayType{OnFoot, InVehicle}

	public delegate void onBCGPlayerCanvasSpawned(BCG_EnterExitCharacterUICanvas canvas);
	public static event onBCGPlayerCanvasSpawned OnBCGPlayerCanvasSpawned;

	public GameObject UisInVehicle;
	public GameObject UisOnFoot;

	void OnEnable () {

		if(OnBCGPlayerCanvasSpawned != null)
			OnBCGPlayerCanvasSpawned (this);
		
	}

	void Start(){

		#if MOBILE_INPUT
		gameObject.SetActive(true);
		#else
		gameObject.SetActive(false);
		#endif

		#if UNITY_EDITOR
		if (Application.isPlaying) //if in the editor, need to check if we are playing, as start is also called just after exiting play
		#endif
		{
			UnityEngine.EventSystems.EventSystem system = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();

			if (system == null)
			{//the scene have no event system, spawn one
				GameObject o = new GameObject("EventSystem");

				o.AddComponent<UnityEngine.EventSystems.EventSystem>();
				o.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
			}
		}

	}

	void Update () {

		switch (displayType) {

		case DisplayType.InVehicle:

			if(!UisInVehicle.activeInHierarchy)
				UisInVehicle.SetActive (true);

			if(UisOnFoot.activeInHierarchy)
				UisOnFoot.SetActive (false);

			break;

		case DisplayType.OnFoot:

			if(UisInVehicle.activeInHierarchy)
				UisInVehicle.SetActive (false);

			if(!UisOnFoot.activeInHierarchy)
				UisOnFoot.SetActive (true);

			break;

		}
		
	}

}
