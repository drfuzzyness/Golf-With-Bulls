using UnityEngine;
using System.Collections;

public class GoalKeeper : MonoBehaviour {

	public GameManager player;

	void OnTriggerEnter( Collider activator ) {
		if( activator.CompareTag( "Bull" ) ) {
//			Debug.Log ( "GOAL!" );
			activator.GetComponent<Bull>().disable();
			player.scorePoint();
		}
	}
}
