using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Board : MonoBehaviour {

	public enum Neighbour {
		North,
		East,
		South,
		West
	};

	public const int Row = 20;
	public const int Column = 20;

	public float AditionDelay = 0.2f;


	public Cube[,] Cubes;

	public GameObject MasterCube;
	public Level Level;

	// Use this for initialization
	void Start () {
		
		CreateBoard ();
		HighlightGoalArea ();

	}

	void CreateBoard() {
		
		Cubes = new Cube[Row, Column];

		for (int i = 0; i < Row; i++) {
			for (int j = 0; j < Column; j++) {
				
				var clone = GameObject.Instantiate (MasterCube);
				clone.transform.parent = MasterCube.transform.parent;
				float beginX = -Row * 0.5f;
				float beginZ = -Column * 0.5f;
				clone.transform.position = new Vector3 (beginX + j * 1f, 0, beginZ + i * 1f);

				var cube = clone.GetComponent<Cube> ();
				cube.OnSelected = OnSelected;
				cube.SetCoordinate (j, i);

				Cubes [j, i] = cube;

			}
		}

		MasterCube.SetActive (false);

	}

	void HighlightGoalArea() {

		for (int i = 0; i < Row; i++) {
			for (int j = 0; j < Column; j++) {

				var cube = Cubes [j, i];
				if (cube == null)
					continue;
				
				var bounds1 = cube.gameObject.GetComponent<Renderer>().bounds;
				var bounds2 = Level.Goal.gameObject.GetComponent<Renderer>().bounds;
				if (!bounds1.Intersects (bounds2))
					continue;

				cube.SetAsGoal ();
			}
		}

	}

	void OnSelected(Cube cube) {

		Push (cube);

	}

	void Push(Cube cube) {
		var already = new List<Cube> ();
		Push (new List<Cube> { cube }, already, 0.0f);
	}

	void Push(List<Cube> cubes, List<Cube> already, float delay) {

		var neigbourCubes = new List<Cube> ();

		foreach (var cube in cubes) {
			if (already.Contains(cube))
				continue;
			cube.Wave (delay);
			already.Add (cube);

			var nbs = GetNeighbours (cube);
			foreach (var nb in nbs) {
				if (already.Contains(nb))
					continue;
				neigbourCubes.Add(nb);
			}
		}

		if (neigbourCubes.Count == 0)
			return;
		
		delay += AditionDelay;
		Push (neigbourCubes, already, delay);

	}

	List<Cube> GetNeighbours(Cube cube) {
	
		var ret = new List<Cube> ();

		foreach (Neighbour n in Enum.GetValues(typeof(Neighbour))) {
			int neighbourRow, neighbourColumn;
			switch (n) {
			default:
			case Neighbour.North:
				neighbourRow = cube.Row + 1;
				neighbourColumn = cube.Column;
				break;
			case Neighbour.East:
				neighbourRow = cube.Row;
				neighbourColumn = cube.Column + 1;
				break;
			case Neighbour.South:
				neighbourRow = cube.Row - 1;
				neighbourColumn = cube.Column;
				break;
			case Neighbour.West:
				neighbourRow = cube.Row;
				neighbourColumn = cube.Column-1;
				break;
			}
			if (neighbourRow < 0 
				|| neighbourRow >= Row 
				|| neighbourColumn < 0 
				|| neighbourColumn >= Column)
				continue;
			var neighbourCube = Cubes [neighbourColumn, neighbourRow];
			if (neighbourCube == null)
				continue;
			ret.Add (neighbourCube);
		}

		return ret;

	}

}
