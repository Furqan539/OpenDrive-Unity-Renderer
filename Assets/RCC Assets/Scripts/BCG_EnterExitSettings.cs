//----------------------------------------------
//            Realistic Tank Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// Stored all general shared Enter-Exit settings here.
/// </summary>
[System.Serializable]
public class BCG_EnterExitSettings : ScriptableObject {

	public string BCGVersion = "0.01b";
	
	#region singleton
	public static BCG_EnterExitSettings instance;
	public static BCG_EnterExitSettings Instance{	get{if(instance == null) instance = Resources.Load("BCG_EnterExitSettings") as BCG_EnterExitSettings; return instance;}}
	#endregion

	public bool enableEnterExit = false;

	// Unity Inputs
	public KeyCode enterExitVehicleKB = KeyCode.E;

	public bool keepEnginesAlive = true;
	public float enterExitSpeedLimit = 20f;

}
