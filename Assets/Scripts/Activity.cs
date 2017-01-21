using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

	public HUD HUD;
	public Board Board;
	public Level Level;

	void Start () {
	
		foreach (var ball in Level.Balls) {
			ball.OnDestroy += BallDestroyed;
		}


	}

	void BallDestroyed() {

		HUD.BallCount++;
		if (Level.Balls.Count == 0) {
			HUD.Alert("You win!", () => {
				Debug.Log("Restart");
			});
		}
	}

}
