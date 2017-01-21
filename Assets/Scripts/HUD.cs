using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour {

	public Text CountText;

	public Prompt Prompt;

	public int BallCount {
		get {
			return _ballCount;
		}
		set {
			_ballCount = value;
			CountText.text = "Count: " + _ballCount;
		}
	}

	int _ballCount;

	// Use this for initialization
	void Start () {
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
