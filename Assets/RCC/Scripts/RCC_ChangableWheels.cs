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
/// Changes wheels (visual only) at runtime. It holds changable wheels as prefab in an array.
/// </summary>
[System.Serializable]
public class RCC_ChangableWheels : ScriptableObject {
	
	#region singleton
	private static RCC_ChangableWheels instance;
	public static RCC_ChangableWheels Instance{	get{if(instance == null) instance = Resources.Load("RCC Assets/RCC_ChangableWheels") as RCC_ChangableWheels; return instance;}}
	#endregion

	[System.Serializable]
	public class ChangableWheels{
		
		public GameObject wheel;

	}
		
	public ChangableWheels[] wheels;

}


