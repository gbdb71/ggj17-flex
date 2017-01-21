using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour {

	public Text LevelText;
	public Text PushText;
	public Text CountText;
	public Prompt Prompt;

	public string Level {
		get {
			return LevelText.text;
		}
		set {
			LevelText.text = value;
		}
	}

	public int PushLeft {
		get {
			return _pushLeft;
		}
		set {
			_pushLeft = value;
			PushText.text = "Push Left: " + _pushLeft;
		}
	}

	public int BallCount {
		get {
			return _ballCount;
		}
		set {
			_ballCount = value;
			CountText.text = "Count: " + _ballCount;
		}
	}
		
	int _pushLeft;
	int _ballCount;

	void Start () {
		PushLeft = 5;
		BallCount = 0;
	}

	public void Alert(string text, Action onButton) {
	
		if (Prompt == null) {
			Debug.Log ("No prompt");
			return;
		}

		Prompt.Set (text, "Okay", onButton);
		Prompt.SetActive (true);

	}

}
