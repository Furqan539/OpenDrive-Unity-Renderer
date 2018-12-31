//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RCC_LevelLoader : MonoBehaviour {

	public void LoadLevel (string levelName) {

		SceneManager.LoadScene (levelName);
		
	}

}
