using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	bool isBursting;

	void OnCollisionEnter(Collision col) {

		if (isBursting)
			return;
		
		var cube = col.gameObject.GetComponent<Cube>();
		if (cube == null || !cube.IsGoal)
			return;
		
		Burst ();

	}

	void Burst() {
	
		isBursting = true;
		Debug.Log ("Burst");

		iTween.ScaleTo (gameObject, new Vector3 (0.0f, 0.0f, 0.0f), 1.0f);

	}
}
