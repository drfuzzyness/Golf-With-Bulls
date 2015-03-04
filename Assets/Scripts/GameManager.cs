using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public List<Bull> bulls;
	public Transform primaryCamera;
	public Transform gameoverCamera;
	public Text scoreText;
	public Text instructions;

	private int mode;
	private int score;
	private int strokes;

	const int GAME_MODE = 0;
	const int GAME_OVER_MODE = 1;
	const int WON_MODE = 2;

	public void addStroke() {
		strokes++;
		scoreText.text = strokes.ToString();
	}

	public bool canControl() {
		return mode == GAME_MODE;
	}

	public void torro() {
		instructions.text = "Torro!";
	}

	public void defaultInstructions() {
		instructions.text = "Space to Dare the Bull";
	}

	public void scorePoint() { 
		score++;
		instructions.text = "GOAL!\nGOAL!";
		if( score == bulls.Count ) {
			mode = WON_MODE;
		}
	}

	public void gameOver() {
		mode = GAME_OVER_MODE;
		foreach( Bull thisBull in bulls ) {
			thisBull.disable();
		}
		instructions.text = "You Died";
	}

	// Use this for initialization
	void Start () {
		score = 0;
		defaultInstructions();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
