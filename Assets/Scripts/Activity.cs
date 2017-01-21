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
			Invoke ("GameOver", 1.0f);
			return;
		}
		
		if (Level != null)
			GameObject.Destroy (Level.gameObject);
	
		Level = Instantiate (RefLevels [_level++], Vector3.zero, Quaternion.identity);
		Level.gameObject.transform.parent = gameObject.transform;

		foreach (var ball in Level.Balls) {
			ball.OnDestroy += BallDestroyed;
		}

		HUD.Level = Level.name.Replace("(Clone)", "");
		HUD.PushLeft = Level.PushMax;

		Board.Level = Level;
		Board.HUD = HUD;
		Board.CreateBoard ();
		Board.SetGoalArea ();

		Board.OnLastPush += () => {
			Invoke ("YouLose", 10);
		};
	}

	void BallDestroyed() {

		HUD.BallCount++;
		if (Level.Balls.Count == 1) {
			HUD.Alert("You win!", () => {
				Start();
			});
		}

	}

	void YouLose() {
	
		if (HUD.PushLeft > 0)
			return;

		GameOver ();

	}

	void GameOver() {
		HUD.Alert ("GameOver!", () => {
			_level = 0;
			Start();
		});
	}

}
