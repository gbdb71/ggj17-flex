using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour {

	bool isBursting;

	public Action OnDestroy;

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

		iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "delay", 1f, "oncomplete", "BurstComplete"));
	}

	void BurstComplete() {
	
		if (OnDestroy != null)
			OnDestroy ();

		GameObject.Destroy (gameObject);

	}

}
