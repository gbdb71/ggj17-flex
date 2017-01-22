using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

	public List<Material> SkyboxMaterials;

	public CameraTarget CameraTarget;
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
		HUD.BallCount = 0;

		Board.Level = Level;
		Board.HUD = HUD;
		Board.CreateBoard ();
		Board.SetGoalArea ();

		Board.OnLastPush += () => {
			Invoke ("YouLose", 10);
		};

		RandomSkybox ();
		CameraTarget.BackToDefault ();

		HUD.ShowTutorial ();

			
	}

	void RandomSkybox() {
		if (SkyboxMaterials.Count == 0)
			return;
		RenderSettings.skybox =  SkyboxMaterials[Random.Range(0, SkyboxMaterials.Count)];
	}

	void BallDestroyed() {

		HUD.BallCount++;
		if (Level.Balls.Count == 1) {
			CameraTarget.BackToDefault ();
			HUD.Alert("Good job!", "Next", () => {
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
		CameraTarget.BackToDefault ();
		HUD.Alert ("GameOver.", "Play again.", () => {
			_level = 0;
			Start();
		});
	}

}
