using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cube : MonoBehaviour {

	public float WaveMax = 1.0f;
	public float WavePower = 5f;

	public Material CubeMaterial;
	public Material GoalMaterial;

	public Action<Cube> OnSelected;

	public int Column { get; private set; }
	public int Row { get; private set; }
	public bool IsGoal { get; private set; }

//	void Update() {
//		
//		if ( Input.GetMouseButtonDown (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){ 
//			RaycastHit hit; 
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
//			if (Physics.Raycast (ray,out hit,100.0f)) {
//				if (hit.transform.gameObject == gameObject && OnSelected != null)			
//					OnSelected (this);
//			}
//		}
//	}

	public void SetCoordinate(int column, int row) {
		Column = column;
		Row = row;
	}

	public void Wave(float delay) {
		
		var position = gameObject.transform.position;

		var index = 0;
		var path = new Vector3[5];

		var power = WavePower;
		var down = true;

		while (power > 0f && index < path.Length) {

			if (down) {
				if (position.y - power < -WaveMax) {
					power -= -WaveMax - position.y;
					position.y = -WaveMax;
				} else {
					position.y -= power;
					power = 0;
				}
			} else { //up
				if (position.y + power > WaveMax) {
					power -= WaveMax - position.y;
					position.y = WaveMax;
				}else {
					position.y += power;
					power = 0f;
				}
			}

			path [index] = position;
			index++;
			down = !down;
		}

		while (index < path.Length) {
			path [index] = position;
			index++;
		}

		iTween.MoveTo (gameObject, iTween.Hash("path", path, "easetype", iTween.EaseType.linear, "delay", delay, "time", WavePower * 0.5f));

	}

	public void SetAsGoal(bool goal) {

		IsGoal = goal;
		gameObject.GetComponent<Renderer>().material = IsGoal ? GoalMaterial : CubeMaterial;

	}

	void OnMouseOver() {
		if(Input.GetMouseButtonDown(0) 
			&& !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() 
			&& OnSelected != null){
				OnSelected (this);
		}
	}

}
