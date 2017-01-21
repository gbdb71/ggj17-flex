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

		if (_level >= RefLevels.Count)
			HUD.Alert("GameOver!", () => {
				Debug.Log("GameOver");
			});
		
		if (Level != null)
			GameObject.Destroy (Level.gameObject);
	
		Level = Instantiate (RefLevels [_level++], Vector3.zero, Quaternion.identity);
		Level.gameObject.transform.parent = gameObject.transform;

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
