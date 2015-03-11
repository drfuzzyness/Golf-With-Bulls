using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject gameCamera;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("Strokes", 0);
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
