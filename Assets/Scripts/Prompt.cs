using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Prompt : MonoBehaviour {

	public Text Text;
	public GameObject Button;
	public AudioSource Sound;

	void Start () {
		SetActive (false);
	}
		
	public void SetActive(bool active) {
		gameObject.SetActive (active);
	}

	public void Set(string text, string buttonText, Action onButton) {
		Text.text = text;

		Button.GetComponentInChildren<Text>().text = buttonText;

		var button = Button.GetComponent<Button> ();

		button.onClick.RemoveAllListeners ();
		button.onClick.AddListener (() => {
			
			onButton();
			SetActive(false);
		});

	}

}
