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
/// Gets total bound size of a gameobject.
/// </summary>
public class RCC_GetBounds : MonoBehaviour {
	
	public static Vector3 GetBoundsCenter(Transform obj){
		
		// get the center bounds extent of object, including all child renderers,
		// but excluding particles and trails, for FOV zooming effect.

		var renderers = obj.GetComponentsInChildren<Renderer>();

		Bounds bounds = new Bounds();
		bool initBounds = false;
		foreach (Renderer r in renderers){
			
			if (!((r is TrailRenderer) || (r is ParticleSystemRenderer))){
				
				if (!initBounds){
					
					initBounds = true;
					bounds = r.bounds;

				}else{
					
					bounds.Encapsulate(r.bounds);

				}

			}

		}

		Vector3 center = bounds.center;
		return center;

	}

	public static float MaxBoundsExtent(Transform obj){

		// get the maximum bounds extent of object, including all child renderers,
		// but excluding particles and trails, for FOV zooming effect.

		var renderers = obj.GetComponentsInChildren<Renderer>();

		Bounds bounds = new Bounds();
		bool initBounds = false;
		foreach (Renderer r in renderers)
		{
			if (!((r is TrailRenderer) || (r is ParticleSystemRenderer)))
			{
				if (!initBounds)
				{
					initBounds = true;
					bounds = r.bounds;
				}
				else
				{
					bounds.Encapsulate(r.bounds);
				}
			}
		}

		float max = Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
		return max;

	}

}
