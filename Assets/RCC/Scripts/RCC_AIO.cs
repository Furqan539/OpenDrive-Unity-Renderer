using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RCC_AIO : MonoBehaviour {

	static RCC_AIO instance;

	public GameObject levels;
	public GameObject back;

	private AsyncOperation async;
	public Slider slider;

	void Start () {

		if (instance) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}

	}

	void Update(){

		if (async != null && !async.isDone) {

			slider.gameObject.SetActive (true);
			slider.value = async.progress;

		} else {

			slider.gameObject.SetActive (false);

		}

	}

	public void LoadLevel (string levelName) {

		async = SceneManager.LoadSceneAsync (levelName);

	}

	public void ToggleMenu (GameObject menu) {

		levels.SetActive (false);
		back.SetActive (false);

		menu.SetActive (true);

	}

}
