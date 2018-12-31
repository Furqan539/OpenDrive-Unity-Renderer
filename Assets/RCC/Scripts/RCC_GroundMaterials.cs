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

[System.Serializable]
public class RCC_GroundMaterials : ScriptableObject {
	
	#region singleton
	private static RCC_GroundMaterials instance;
	public static RCC_GroundMaterials Instance{	get{if(instance == null) instance = Resources.Load("RCC Assets/RCC_GroundMaterials") as RCC_GroundMaterials; return instance;}}
	#endregion

	[System.Serializable]
	public class GroundMaterialFrictions{
		
		public PhysicMaterial groundMaterial;
		public float forwardStiffness = 1f;
		public float sidewaysStiffness = 1f;
		public float slip = .25f;
		public float damp = 1f;
		public GameObject groundParticles;
		public AudioClip groundSound;

	}
		
	public GroundMaterialFrictions[] frictions;

	public bool useTerrainSplatMapForGroundFrictions = false;
	public PhysicMaterial terrainPhysicMaterial;

}


