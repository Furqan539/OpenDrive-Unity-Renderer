using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles RCC Canvas dashboard elements.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Info Displayer")]
[RequireComponent (typeof(Text))]
public class RCC_InfoLabel : MonoBehaviour {

	#region singleton
	private static RCC_InfoLabel instance;
	public static RCC_InfoLabel Instance{

		get{

			if (instance == null) {

				if (GameObject.FindObjectOfType<RCC_InfoLabel> ())
					instance = GameObject.FindObjectOfType<RCC_InfoLabel> ();

			}

			return instance;

		}

	}
	#endregion

	private Text text;
	private float timer = 1f;

	void Start () {

		text = GetComponent<Text> ();
		text.enabled = false;
		
	}

	void Update(){

		if (timer < 1f) {
			
			if (!text.enabled)
				text.enabled = true;
			
		} else {
			
			if (text.enabled)
				text.enabled = false;
			
		}

		timer += Time.deltaTime;

	}

	public void ShowInfo (string info) {

		if (!text)
			return;

		text.text = info;
		timer = 0f;

//		StartCoroutine (ShowInfoCo(info, time));
		
	}

	IEnumerator ShowInfoCo(string info, float time){

		text.enabled = true;
		text.text = info;
		yield return new WaitForSeconds (time);
		text.enabled = false;

	}

}
