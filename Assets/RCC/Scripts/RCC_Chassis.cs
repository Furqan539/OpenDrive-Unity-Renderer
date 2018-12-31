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
/// Simulates chassis movement based on vehicle rigidbody velocity.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Chassis")]
public class RCC_Chassis : MonoBehaviour {

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
	private Rigidbody rigid;

	private float chassisVerticalLean = 4f;			// Chassis Vertical Lean Sensitivity.
	private float chassisHorizontalLean = 4f;		// Chassis Horizontal Lean Sensitivity.

	private float horizontalLean = 0f;
	private float verticalLean = 0f;

	void Start () {

		carController = GetComponentInParent<RCC_CarControllerV3> ();
		rigid = carController.GetComponent<Rigidbody> ();

		if (!RCCSettings.dontUseChassisJoint)
			ChassisJoint ();

	}

	void OnEnable(){

		if (!RCCSettings.dontUseChassisJoint)
			StartCoroutine ("ReEnable");

	}

	IEnumerator ReEnable(){
		
		if (!transform.parent.GetComponent<ConfigurableJoint> ())
			yield break;

		GameObject _joint = GetComponentInParent<ConfigurableJoint>().gameObject;

		_joint.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
		yield return new WaitForFixedUpdate();
		_joint.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

	}

	void ChassisJoint(){

		GameObject colliders = new GameObject("Colliders");
		colliders.transform.SetParent(GetComponentInParent<RCC_CarControllerV3> ().transform, false);

		GameObject chassisJoint;

		Transform[] childTransforms = GetComponentInParent<RCC_CarControllerV3> ().chassis.GetComponentsInChildren<Transform>();

		foreach(Transform t in childTransforms){

			if(t.gameObject.activeSelf && t.GetComponent<Collider>()){

				if (t.childCount >= 1) {
					Transform[] childObjects = t.GetComponentsInChildren<Transform> ();
					foreach (Transform c in childObjects) {
						if (c != t) {
							c.SetParent (transform);
						}
					}
				}

				GameObject newGO = (GameObject)Instantiate(t.gameObject, t.transform.position, t.transform.rotation);
				newGO.transform.SetParent(colliders.transform, true);
				newGO.transform.localScale = t.lossyScale;

				Component[] components = newGO.GetComponents(typeof(Component));

				foreach(Component comp  in components){
					if(!(comp is Transform) && !(comp is Collider)){
						Destroy(comp);
					}
				}

			}

		}

		chassisJoint = (GameObject)Instantiate((RCCSettings.chassisJoint), Vector3.zero, Quaternion.identity);
		chassisJoint.transform.SetParent(rigid.transform, false);
		chassisJoint.GetComponent<ConfigurableJoint> ().connectedBody = rigid;
		chassisJoint.GetComponent<ConfigurableJoint> ().autoConfigureConnectedAnchor = false;

		transform.SetParent(chassisJoint.transform, false);

		Collider[] collidersInChassis = GetComponentsInChildren<Collider>();

		foreach(Collider c in collidersInChassis)
			Destroy(c);

		GetComponentInParent<Rigidbody> ().centerOfMass = new Vector3 (rigid.centerOfMass.x, rigid.centerOfMass.y + 1f, rigid.centerOfMass.z);

	}

	void Update(){

		chassisVerticalLean = carController.chassisVerticalLean;
		chassisHorizontalLean = carController.chassisHorizontalLean;

	}

	void FixedUpdate () {

		if (RCCSettings.dontUseChassisJoint)
			LegacyChassis ();

	}

	void LegacyChassis (){

		Vector3 localVelocity = rigid.transform.InverseTransformDirection (rigid.angularVelocity);

		verticalLean = Mathf.Clamp(Mathf.Lerp (verticalLean, rigid.angularVelocity.x * chassisVerticalLean, Time.fixedDeltaTime * 5f), -5f, 5f);
		horizontalLean = Mathf.Clamp(Mathf.Lerp (horizontalLean, localVelocity.y * chassisHorizontalLean, Time.fixedDeltaTime * 5f), -5f, 5f);

		if(float.IsNaN(verticalLean) || float.IsNaN(horizontalLean) || float.IsInfinity(verticalLean) || float.IsInfinity(horizontalLean) || Mathf.Approximately(verticalLean, 0f) || Mathf.Approximately(horizontalLean, 0f))
			return;

		Quaternion target = Quaternion.Euler(verticalLean, transform.localRotation.y, horizontalLean);
		transform.localRotation = target;

	}

}
