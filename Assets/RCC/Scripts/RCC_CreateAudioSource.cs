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
/// Creates new audiosources at runtime.
/// </summary>
public class RCC_CreateAudioSource : MonoBehaviour {

	/// <summary>
	/// Creates new audiosource with specified settings.
	/// </summary>
	public static AudioSource NewAudioSource(GameObject go, string audioName, float minDistance, float maxDistance, float volume, AudioClip audioClip, bool loop, bool playNow, bool destroyAfterFinished){
		
		GameObject audioSourceObject = new GameObject(audioName);

		if (go.transform.Find ("All Audio Sources")) {
			audioSourceObject.transform.SetParent (go.transform.Find ("All Audio Sources"));
		} else {
			GameObject allAudioSources = new GameObject ("All Audio Sources");
			allAudioSources.transform.SetParent (go.transform, false);
			audioSourceObject.transform.SetParent (allAudioSources.transform, false);
		}

		audioSourceObject.transform.position = go.transform.position;
		audioSourceObject.transform.rotation = go.transform.rotation;

		audioSourceObject.AddComponent<AudioSource>();
		AudioSource source = audioSourceObject.GetComponent<AudioSource> ();

		//audioSource.GetComponent<AudioSource>().priority =1;
		source.minDistance = minDistance;
		source.maxDistance = maxDistance;
		source.volume = volume;
		source.clip = audioClip;
		source.loop = loop;
		source.dopplerLevel = .5f;

		if(minDistance == 0 && maxDistance == 0)
			source.spatialBlend = 0f;
		else
			source.spatialBlend = 1f;

		if (playNow) {
			source.playOnAwake = true;
			source.Play ();
		} else {
			source.playOnAwake = false;
		}

		if(destroyAfterFinished){
			if(audioClip)
				Destroy(audioSourceObject, audioClip.length);
			else
				Destroy(audioSourceObject);
		}

		return source;

	}

	/// <summary>
	/// Adds High Pass Filter to audiosource. Used for turbo.
	/// </summary>
	public static void NewHighPassFilter(AudioSource source, float freq, int level){

		if(source == null)
			return;

		AudioHighPassFilter highFilter = source.gameObject.AddComponent<AudioHighPassFilter>();
		highFilter.cutoffFrequency = freq;
		highFilter.highpassResonanceQ = level;

	}

	/// <summary>
	/// Adds Low Pass Filter to audiosource. Used for engine off sounds.
	/// </summary>
	public static void NewLowPassFilter(AudioSource source, float freq){

		if(source == null)
			return;

		AudioLowPassFilter lowFilter = source.gameObject.AddComponent<AudioLowPassFilter>();
		lowFilter.cutoffFrequency = freq;
//		lowFilter.highpassResonanceQ = level;

	}

}
