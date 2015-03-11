using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*
  ,-_/,.     .                  .                  
 ' |_|/ ,-. |-   ,-. . ,-. ,-. |  ,-. ,-.   . ,-. 
  /| |  | | |    `-. | | | | | |  |-' `-.   | | | 
  `' `' `-' `'   `-' ' ' ' `-| `' `-' `-'   ' ' ' 
                            ,|                    
                            `'                    
                                                  
        . . ,-. . . ,-.   ,-. ,-. ,-. ,-.         
        | | | | | | |     ,-| |   |-' ,-|         
        `-| `-' `-^ '     `-^ '   `-' `-^         
         /|                                       
        `-'                                       
 */

public class GameManager : MonoBehaviour {
	
	public List<Bull> bulls;
	public GameObject primaryCamera;
	public GameObject gameWonCamera;
	public Text scoreText;
	public Text instructions;
	public Text strokesText;
	public GameObject winText;
	public GameObject player;
	public int parStrokes;

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

		if( score == bulls.Count ) {

			StartCoroutine( wonLevel() );
		}
		else {
			instructions.text = (bulls.Count - score) + " bulls to go";
		}
	}

	public void gameOver() {
		if( mode != GAME_OVER_MODE ) {
			mode = GAME_OVER_MODE;
			foreach( Bull thisBull in bulls ) {
				thisBull.disable();
			}
			instructions.text = "You Died\nSpace to Restart";
			player.GetComponent<Rigidbody>().constraints = 0;
			player.transform.FindChild("Camera").parent = null; // Remove the camera so that it doesn't slam into the ground

		}
	}

	IEnumerator wonLevel() {
		instructions.text = "";
		mode = WON_MODE;
		winText.SetActive( true );
		winText.transform.Find( "Your Score" ).GetComponent<Text>().text = score + "";
		winText.transform.Find( "Score Value" ).GetComponent<Text>().text = "+" + (strokes - parStrokes) ;
//		scoreText.text = (strokes - parStrokes) + "";
		iTween.MoveTo(primaryCamera, iTween.Hash("position", gameWonCamera.transform, "time", 2f, "easetype", "easeInOutQuint") );
		iTween.RotateTo(primaryCamera, iTween.Hash("rotation", gameWonCamera.transform, "time", 2f, "easetype", "easeInOutQuint") );
		int previousScore = PlayerPrefs.GetInt( "Strokes" );
		PlayerPrefs.SetInt( "Strokes", previousScore  +  strokes - parStrokes );
		yield return new WaitForSeconds( 4f );
		Application.LoadLevel(Application.loadedLevel+1);
		
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
