using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level : MonoBehaviour {

	public Goal Goal;
	public List<Ball> Balls;
	public int PushMax = 5;

	void Start () {

		gameObject.SetActive (true);

		foreach (var ball in Balls) {
			ball.OnDestroy += () => Balls.Remove (ball);
		}

	}
		
}
