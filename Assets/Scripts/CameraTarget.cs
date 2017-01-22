using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {

	public Vector3 DefaultAngle;
	public HUD HUD;

	Vector3 initialMousePosition;
	Vector3 initialRotation;

	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			initialMousePosition = Input.mousePosition;
			initialRotation = gameObject.transform.localRotation.eulerAngles;
		}

		if (Input.GetMouseButton (0)) {
	
			var diff = initialMousePosition - Input.mousePosition;
			var rotation = initialRotation + new Vector3 (diff.y * 0.5f, -diff.x * 0.1f, 0.0f);

			rotation.x = rotation.x > 45.0f ? 45.0f : rotation.x;
			rotation.x = rotation.x < 0.0f ? 0.0f : rotation.x;

			gameObject.transform.localRotation = Quaternion.Euler (rotation);
			HUD.DismissTutorial ();
		}

	}

	public void BackToDefault(bool animated = true) {

		if (gameObject.transform.localRotation.Equals (DefaultAngle))
			return;
		
		if (animated == false)
			gameObject.transform.localRotation = Quaternion.Euler (DefaultAngle);
		else 
			iTween.RotateTo (gameObject, iTween.Hash ("rotation", DefaultAngle, "time", 3.0f));
		
	}

}
