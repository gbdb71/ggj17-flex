using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public GameObject Cube;

	// Use this for initialization
	void Start () {
		CreateBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateBoard() {
	

		for (int i = 0, row = 10; i < row; i++) {
			for (int j = 0, column = 10; j < column; j++) {
				var cube = GameObject.Instantiate (Cube);
				cube.transform.parent = Cube.transform.parent;
				cube.transform.position = new Vector3 (j, 0, i);
			}
		}

		Cube.SetActive (false);

	}

}
