using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControllerScript : MonoBehaviour {

	public int boardSize = 1;
	public GameObject cell;
	public Color quad1color = Color.green;
	public Color quad2color = Color.white;
	public Color quad3color = Color.red;
	public Color quad4color = Color.yellow;

	private int p_boardSize = 1;
	private float p_cellSize = 0;
	private GameObject[,] cells;
	private Color[] colors;
	private int[] colorCount = new int[4];
	private bool pass = false;


	// Use this for initialization
	void Start () {
		p_boardSize = (int) Mathf.Pow(2f, boardSize);
		//print ("Board size: " + p_boardSize);
		p_cellSize = cell.GetComponent<SpriteRenderer> ().size.x;
		//print ("Cell size: " + p_cellSize);
		//cells = new GameObject[p_boardSize,p_boardSize];
		colors = new Color[4];
		colors [0] = quad1color;
		colors [1] = quad2color;
		colors [2] = quad3color;
		colors [3] = quad4color;

		GenBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenBoard () {
		Vector3 pos = new Vector3 (0f, 0f, 0f);
		cells = new GameObject[p_boardSize,p_boardSize];
		colorCount = new int[4];
		for (int i = 0; i < p_boardSize; i++) {
			for (int j = 0; j < p_boardSize; j++) {
				GameObject c = Instantiate (cell, pos, Quaternion.identity);
				//print (c.GetComponent<SpriteRenderer>().color);
				int randNum = 0;
				//print ((int) Mathf.Pow(2f, p_boardSize) / 4);
				int passCount = 0;
				while (pass == false && passCount < 100) {
					passCount++;
					randNum = Random.Range (0, 4);
					//print ("Rand num: " + randNum + ", Color count: " + colorCount[randNum]);
					if (colorCount [randNum] < (int) Mathf.Pow(2f, p_boardSize) / 4) {		// divide by # of quadrants
						colorCount [randNum] = colorCount [randNum] + 1;
						pass = true;
					}
				}
				pass = false;
				passCount = 0;

				Color randCol = colors [randNum];
				//print (randCol);
				c.GetComponent<SpriteRenderer> ().color = randCol;
				cells [i,j] = c;
				pos.x = pos.x + p_cellSize;
			}
			pos.y = pos.y - p_cellSize;
			pos.x = 0f;
		}

		print ("Did I win? " + DidIWin ());

	}

	bool DidIWin() {
		List<GameObject> Quadrant1 = new List<GameObject>();
		List<GameObject> Quadrant2 = new List<GameObject>();
		List<GameObject> Quadrant3 = new List<GameObject>();
		List<GameObject> Quadrant4 = new List<GameObject>();

		bool win = false;

		for (int i = 0; i < p_boardSize; i++) {
			for (int j = 0; j < p_boardSize; j++) {
				if ((i < p_boardSize / 2) && (j < p_boardSize / 2)) {
					Quadrant2.Add (cells [i,j]);
				} else if ((i >= p_boardSize / 2) && (j < p_boardSize / 2)) {
					Quadrant3.Add (cells [i,j]);
				} else if ((i < p_boardSize / 2) && (j >= p_boardSize / 2)) {
					Quadrant1.Add (cells [i,j]);	
				} else if ((i >= p_boardSize / 2) && (j >= p_boardSize / 2)) {
					Quadrant4.Add (cells [i,j]);
				}
			}
		}

		foreach (GameObject cell in Quadrant1) {
			if (cell.GetComponent<SpriteRenderer> ().color == quad1color) {
				//continue;
			} else {
				return win;
			}
		}

		foreach (GameObject cell in Quadrant2) {
			if (cell.GetComponent<SpriteRenderer> ().color == quad2color) {
				//continue;
			} else {
				return win;
			}
		}

		foreach (GameObject cell in Quadrant3) {
			if (cell.GetComponent<SpriteRenderer> ().color == quad3color) {
				//continue;
			} else {
				return win;
			}
		}

		foreach (GameObject cell in Quadrant4) {
			if (cell.GetComponent<SpriteRenderer> ().color == quad4color) {
				//continue;
				win = true;
			} else {
				return win;
			}
		}

		return win;

	}
}
