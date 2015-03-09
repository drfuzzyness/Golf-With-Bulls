using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinnerScreenManager : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject gameCamera;
	public Text scoreText;
	
	// Use this for initialization
	void Start () {
		scoreText.text = PlayerPrefs.GetInt("Score") + "";
		PlayerPrefs.SetInt("Score", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown("space") ) {
			StartCoroutine( startGame() );
			
		}
	}
	
	IEnumerator startGame() {
		
		iTween.MoveTo(mainCamera, iTween.Hash("position", gameCamera.transform, "time", 2f, "easetype", "easeInOutQuint") );
		iTween.RotateTo(mainCamera, iTween.Hash("rotation",gameCamera.transform, "time", 2f, "easetype", "easeInOutQuint") );
		yield return new WaitForSeconds( 2f );
		Application.LoadLevel(1);
		
	}
}
