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
	public GameObject player;

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
		instructions.text = "Space to Dare\nthe Bull";
	}

	public void scorePoint() { 
		score++;
		instructions.text = "GOAL!\nGOAL!";
		if( score == bulls.Count ) {
			mode = WON_MODE;
		}
	}

	public void gameOver() {
		if( mode != GAME_OVER_MODE ) {
			mode = GAME_OVER_MODE;
			foreach( Bull thisBull in bulls ) {
				thisBull.disable();
//				thisBull.GetComponent<Rigidbody>().useGravity = false;
//				thisBull.GetComponent<Rigidbody>().drag = 0;
			}
			instructions.text = "You Died\nSpace to Restart";
			player.GetComponent<Rigidbody>().constraints = 0;
//			player.GetComponent<Rigidbody>().useGravity = false;
//			player.GetComponent<Rigidbody>().drag = 0;
			player.transform.FindChild("Camera").parent = null; // Remove the camera so that it doesn't slam into the ground
		}
	}

	// Use this for initialization
	void Start () {
		score = 0;
		defaultInstructions();
	}

	// Update is called once per frame
	void Update () {
		if( canControl () && Input.GetKeyDown("space") ) {
			bool didAnyoneCharge = false;
			foreach( Bull thisBull in bulls ) {
				if( thisBull.isActive && !thisBull.isCharging ) {
					thisBull.Charge();
					didAnyoneCharge = true;
				}
			}
			if( didAnyoneCharge ) {
				addStroke();
			}
		}
		else if( Input.GetKeyDown("space") ) {

			Application.LoadLevel(0); // load the first scene
		}
	}
}
