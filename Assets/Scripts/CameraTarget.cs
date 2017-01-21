using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {

	Vector3 offset;

	Vector3 initialMousePosition;
	Vector3 initialRotation;

	bool controllingEnabled = true;

	void Update () {

		if (controllingEnabled) {
			
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

			}

		}

	}

	public void BackToDefault() {
		
		iTween.RotateTo (gameObject, iTween.Hash("rotation", Vector3.zero, "time", 2.0f, "oncomplete", "CompleteBackToDefault"));

	}

	void CompleteBackToDefault() {
		
		controllingEnabled = true;

	}

}
