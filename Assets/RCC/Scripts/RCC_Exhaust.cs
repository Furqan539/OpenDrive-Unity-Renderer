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
/// Exhaust based on Particle System. Based on vehicle controller's throttle situation.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Exhaust")]
public class RCC_Exhaust : MonoBehaviour {

	// Getting an Instance of Main Shared RCC Settings.
	#region RCC Settings Instance

	private RCC_Settings RCCSettingsInstance;
	private RCC_Settings RCCSettings {
		get {
			if (RCCSettingsInstance == null) {
				RCCSettingsInstance = RCC_Settings.Instance;
			}
			return RCCSettingsInstance;
		}
	}

	#endregion

	private RCC_CarControllerV3 carController;
	private ParticleSystem particle;
	private ParticleSystem.EmissionModule emission;
	public ParticleSystem flame;
	private ParticleSystem.EmissionModule subEmission;

	private Light flameLight;
	private LensFlare lensFlare;

	public float flareBrightness = 1f;
	private float finalFlareBrightness;

	public float flameTime = 0f;
	private AudioSource flameSource;

	public Color flameColor = Color.red;
	public Color boostFlameColor = Color.blue;

	public bool previewFlames = false;

	public float minEmission = 5f;
	public float maxEmission = 50f;

	public float minSize = 2.5f;
	public float maxSize = 5f;

	public float minSpeed = .5f;
	public float maxSpeed = 5f;

	void Start () {

		if (RCCSettings.dontUseAnyParticleEffects) {
			Destroy (gameObject);
			return;
		}

		carController = GetComponentInParent<RCC_CarControllerV3>();
		particle = GetComponent<ParticleSystem>();
		emission = particle.emission;

		if(flame){

			subEmission = flame.emission;
			flameLight = flame.GetComponentInChildren<Light>();
			flameSource = RCC_CreateAudioSource.NewAudioSource(gameObject, "Exhaust Flame AudioSource", 10f, 25f, 1f, RCCSettings.exhaustFlameClips[0], false, false, false);
			flameLight.renderMode = RCCSettings.useLightsAsVertexLights ? LightRenderMode.ForceVertex : LightRenderMode.ForcePixel;

		}

		lensFlare = GetComponentInChildren<LensFlare> ();

		if (flameLight) {

			if (flameLight.flare != null)
				flameLight.flare = null;

		}

	}

	void Update () {

		if(!carController || !particle)
			return;

		Smoke ();
		Flame ();

		if (lensFlare)
			LensFlare ();

	}

	void Smoke(){

		if (carController.engineRunning) {

			var main = particle.main;

			if (carController.speed < 50) {

				if (!emission.enabled)
					emission.enabled = true;

				if (carController._gasInput > .35f) {

					emission.rateOverTime = maxEmission;
					main.startSpeed = maxSpeed;
					main.startSize = maxSize;

				} else {
					
					emission.rateOverTime = minEmission;
					main.startSpeed = minSpeed;
					main.startSize = minSize;

				}

			} else {

				if (emission.enabled)
					emission.enabled = false;

			}

		} else {

			if (emission.enabled)
				emission.enabled = false;

		}

	}

	void Flame(){

		if(carController.engineRunning){

			var main = flame.main;

			if(carController._gasInput >= .25f)
				flameTime = 0f;

			if(((carController.useExhaustFlame && carController.engineRPM >= 5000 && carController.engineRPM <= 5500 && carController._gasInput <= .25f && flameTime <= .5f) || carController._boostInput >= 1.5f) || previewFlames){

				flameTime += Time.deltaTime;
				subEmission.enabled = true;

				if(flameLight)
					flameLight.intensity = flameSource.pitch * 3f * Random.Range(.25f, 1f) ;

				if(carController._boostInput >= 1.5f && flame){
					main.startColor = boostFlameColor;
					flameLight.color = main.startColor.color;
				}else{
					main.startColor = flameColor;
					flameLight.color = main.startColor.color;
				}

				if(!flameSource.isPlaying){
					flameSource.clip = RCCSettings.exhaustFlameClips[Random.Range(0, RCCSettings.exhaustFlameClips.Length)];
					flameSource.Play();
				}

			}else{

				subEmission.enabled = false;

				if(flameLight)
					flameLight.intensity = 0f;
				if(flameSource.isPlaying)
					flameSource.Stop();

			}

		}else{

			if(emission.enabled)
				emission.enabled = false;

			subEmission.enabled = false;

			if(flameLight)
				flameLight.intensity = 0f;
			if(flameSource.isPlaying)
				flameSource.Stop();

		}

	}

	private void LensFlare(){

		if (!RCC_SceneManager.Instance.activePlayerCamera)
			return;

		float distanceTocam = Vector3.Distance(transform.position, RCC_SceneManager.Instance.activePlayerCamera.thisCam.transform.position);
		float angle = Vector3.Angle(transform.forward,  RCC_SceneManager.Instance.activePlayerCamera.thisCam.transform.position - transform.position);

		if(angle != 0)
			finalFlareBrightness = flareBrightness * (4 / distanceTocam) * ((100f - (1.11f * angle)) / 100f) / 2f;

		lensFlare.brightness = finalFlareBrightness * flameLight.intensity;
		lensFlare.color = flameLight.color;

	}

}
