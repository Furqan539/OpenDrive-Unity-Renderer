//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class RCC_CameraCarSelection : MonoBehaviour{

	public Transform target;
	public float distance= 10.0f;

	public float xSpeed= 250f;
	public float  ySpeed= 120f;

	public float yMinLimit= -20f;
	public float yMaxLimit= 80f;

	private float x= 0f;
	private float y= 0f;

	private bool selfTurn = true;
	private float selfTurnTime = 0f;

	void Start (){

		Vector3 angles= transform.eulerAngles;
		x = angles.y;
		y = angles.x;

	}

	void LateUpdate (){

		if (target) {

			if(selfTurn)
				x += xSpeed / 2f * Time.deltaTime;

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation= Quaternion.Euler(y, x, 0);
			Vector3 position= rotation * new Vector3(0f, 0f, -distance) + target.position;

			transform.rotation = rotation;
			transform.position = position;

			if (selfTurnTime <= 1f)
				selfTurnTime += Time.deltaTime;

			if (selfTurnTime >= 1f)
				selfTurn = true;

		}

	}

	static float ClampAngle ( float angle ,   float min ,   float max  ){

		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);

	}

	public void OnDrag(BaseEventData data){
		
		PointerEventData pointerData = data as PointerEventData;

		x += pointerData.delta.x * xSpeed * 0.02f;
		y -= pointerData.delta.y * ySpeed * 0.02f;

		y = ClampAngle(y, yMinLimit, yMaxLimit);

		Quaternion rotation= Quaternion.Euler(y, x, 0);
		Vector3 position= rotation * new Vector3(0f, 0f, -distance) + target.position;

		transform.rotation = rotation;
		transform.position = position;

		selfTurn = false;
		selfTurnTime = 0f;

	}

}