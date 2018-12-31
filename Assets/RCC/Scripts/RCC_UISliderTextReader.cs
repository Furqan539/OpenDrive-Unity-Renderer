//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Receives float from UI Slider, and displays the value as a text.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Slider Text Reader")]
public class RCC_UISliderTextReader : MonoBehaviour {

	public Slider slider;
	public Text text;

	void Awake () {

		if(!slider)
			slider = GetComponentInParent<Slider> ();
		
		if(!text)
			text = GetComponentInChildren<Text> (); 
	
	}

	void Update () {

		if (!slider || !text)
			return;
		
		text.text = slider.value.ToString ("F1");

	}

}
