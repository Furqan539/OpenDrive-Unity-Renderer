using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCC_PoliceSiren : MonoBehaviour {

	private RCC_AICarController AI;

	public SirenMode sirenMode;
	public enum SirenMode{Off, On}

	public Light[] redLights;
	public Light[] blueLights;

	void Start () {

		AI = GetComponentInParent<RCC_AICarController> ();
		
	}

	void Update () {

		switch (sirenMode) {

		case SirenMode.Off:

			for (int i = 0; i < redLights.Length; i++)
				redLights[i].intensity = Mathf.Lerp (redLights[i].intensity, 0f, Time.deltaTime * 50f);

			for (int i = 0; i < blueLights.Length; i++)
				blueLights[i].intensity = Mathf.Lerp (blueLights[i].intensity, 0f, Time.deltaTime * 50f);

			break;

		case SirenMode.On:

			if(Mathf.Approximately((int)(Time.time)%2, 0) && Mathf.Approximately((int)(Time.time * 20)%3, 0)){

				for (int i = 0; i < redLights.Length; i++)
					redLights[i].intensity = Mathf.Lerp (redLights[i].intensity, 1f, Time.deltaTime * 50f);
				
			}else{

				for (int i = 0; i < redLights.Length; i++)
					redLights[i].intensity = Mathf.Lerp (redLights[i].intensity, 0f, Time.deltaTime * 10f);

				if(Mathf.Approximately((int)(Time.time * 20)%3, 0)){
					
					for (int i = 0; i < blueLights.Length; i++)
						blueLights[i].intensity = Mathf.Lerp (blueLights[i].intensity, 1f, Time.deltaTime * 50f);
					
				}else{
					
					for (int i = 0; i < blueLights.Length; i++)
						blueLights[i].intensity = Mathf.Lerp (blueLights[i].intensity, 0f, Time.deltaTime * 10f);
					
				}

			}

			break;

		}

		if (AI) {

			if (AI.targetChase != null)
				sirenMode = SirenMode.On;
			else
				sirenMode = SirenMode.Off;

		}
		
	}

	public void SetSiren(bool state){

		if (state)
			sirenMode = SirenMode.On;
		else
			sirenMode = SirenMode.Off;

	}

}
