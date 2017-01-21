using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public Goal Goal;
	public List<Ball> Balls;

	void Start () {
		
		foreach (var ball in Balls) {
			ball.OnDestroy += () => Balls.Remove (ball);
		}

	}

}
