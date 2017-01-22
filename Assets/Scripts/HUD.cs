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

	public GameObject Clicking;
	public GameObject Moving;

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
			PushText.text = "Move: " + _pushLeft;
		}
	}

	public int BallCount {
		get {
			return _ballCount;
		}
		set {
			_ballCount = value;
			CountText.text = "Ball: " + _ballCount;
		}
	}
		
	int _pushLeft;
	int _ballCount;

	void Start () {
		PushLeft = 5;
		BallCount = 0;

		Clicking.SetActive (false);
		Moving.SetActive (false);
	}

	public void ShowTutorial() {
		if (Level.Equals ("Level 1"))
			Clicking.SetActive (true);

		if (Level.Equals ("Level 2"))
			Moving.SetActive (true);
	}

	public void DismissTutorial() {
		if (Level.Equals ("Level 1"))
			Clicking.SetActive (false);
		if (Level.Equals ("Level 2"))
			Moving.SetActive (false);
	}

	public void Alert(string text, string buttonText, Action onButton) {
	
		if (Prompt == null) {
			Debug.Log ("No prompt");
			return;
		}

		Prompt.Set (text, buttonText, onButton);
		Prompt.SetActive (true);

	}

}
