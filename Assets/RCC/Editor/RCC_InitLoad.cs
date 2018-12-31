//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class RCC_InitLoad : MonoBehaviour {

	[InitializeOnLoad]
	public class InitOnLoad {

		static InitOnLoad(){

			SetEnabled("BCG_RCC", true);
			
			if(!EditorPrefs.HasKey("RCC" + "V3.2b" + "Installed")){
				
				EditorPrefs.SetInt("RCC" + "V3.2b" + "Installed", 1);
				EditorUtility.DisplayDialog("Regards from BoneCracker Games", "Thank you for purchasing and using Realistic Car Controller. Please read the documentation before use. Also check out the online documentation for updated info. Have fun :)", "Let's get started");

				if(EditorUtility.DisplayDialog("Importing BoneCracker Games Shared Assets", "Do you want to import ''BoneCracker Games Shared Assets'' to your project? It will be used for enter / exit on all vehicles created by BoneCracker Games in future.", "Import it", "No"))
					AssetDatabase.ImportPackage("Assets/RealisticCarControllerV3/For BCG Shared Assets/BCG Shared Assets.unitypackage", true);

				Selection.activeObject = RCC_Settings.Instance;

			}

		}

		private static BuildTargetGroup[] buildTargetGroups = new BuildTargetGroup[]
		{

			BuildTargetGroup.Standalone,
			BuildTargetGroup.Android,
			BuildTargetGroup.iOS,
			BuildTargetGroup.WebGL,
			BuildTargetGroup.Facebook,
			BuildTargetGroup.N3DS,
			BuildTargetGroup.XboxOne,
			BuildTargetGroup.PS4,
			BuildTargetGroup.PSP2,
			BuildTargetGroup.PSM,
			BuildTargetGroup.tvOS,
			BuildTargetGroup.SamsungTV,
			BuildTargetGroup.Tizen,
			BuildTargetGroup.Switch,
			BuildTargetGroup.WiiU,
			BuildTargetGroup.WSA

		};

		private static void SetEnabled(string defineName, bool enable)
		{
			//Debug.Log("setting "+defineName+" to "+enable);
			foreach (var group in buildTargetGroups)
			{
				var defines = GetDefinesList(group);
				if (enable)
				{
					if (defines.Contains(defineName))
					{
						return;
					}
					defines.Add(defineName);
				}
				else
				{
					if (!defines.Contains(defineName))
					{
						return;
					}
					while (defines.Contains(defineName))
					{
						defines.Remove(defineName);
					}
				}
				string definesString = string.Join(";", defines.ToArray());
				PlayerSettings.SetScriptingDefineSymbolsForGroup(group, definesString);
			}
		}

		private static List<string> GetDefinesList(BuildTargetGroup group){
			
			return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';'));

		}

	}

}
