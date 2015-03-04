using UnityEngine;
using System.Collections;

public class PassedPlayerOnTrigger : MonoBehaviour {

	public Transform target;
	private bool isLooking;

	public void stopLooking() {
		isLooking = false;
	}

	void Start() {
		isLooking = true;
	}
	

	// Update is called once per frame
	void Update () {
		if( isLooking )
			transform.LookAt( target );
	}
	
	void OnTriggerEnter( Collider activator ) {
		if( activator.CompareTag( "Bull" ) ) {
			activator.GetComponent<Bull>().PassedPlayer();
			Debug.Log( "Passed the player!" );
			isLooking = true;
		}
	}

}
