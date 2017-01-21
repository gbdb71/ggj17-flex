using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

	public HUD HUD;
	public Board Board;
	public Level Level;

	public List<Level> RefLevels;
	public int _level;

	void Start () {

		if (_level >= RefLevels.Count) {
			Debug.Log ("GameOver");
			Invoke ("GameOver", 1.0f);

			return;
		}
		
		if (Level != null)
			GameObject.Destroy (Level.gameObject);
	
		Level = Instantiate (RefLevels [_level++], Vector3.zero, Quaternion.identity);
		Level.gameObject.transform.parent = gameObject.transform;

		foreach (var ball in Level.Balls) {
			ball.OnDestroy += () => {
				HUD.BallCount++;
				Debug.Log("Level.Balls.Count="+Level.Balls.Count);
				if (Level.Balls.Count == 1) {
					HUD.Alert("You win!", () => {
						Start();
					});
				}
			};
		}

		Board.Level = Level;
		Board.SetGoalArea ();

	}

	void BallDestroyed() {


	}

	void GameOver() {
		HUD.Alert ("GameOver!", () => {
			_level = 0;
			Start();
		});
	}

}
