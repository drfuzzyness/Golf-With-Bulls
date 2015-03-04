/*
                      _ ________    __   __  __  __  __    _ ____               
                /|/| /_| /   /    /(    / _ /  )/  )/  )  /_| /                 
               /   |(  |(   (    (__)  (__)(__/(__//(_/  (  |(                  
                                                                                
                    __  __  __  __  __  _                  __                   
                   /__)/__)/  )/ _ /__)/_| /|/| /|/| //| )/ _                   
                  /   / ( (__/(__)/ ( (  |/   |/   |(/ |/(__)                   
 */

using UnityEngine;
using System.Collections;

public class Bull : MonoBehaviour {

	public float maxSpeed;
	public GameObject player;
	public PassedPlayerOnTrigger playerTrigger;
	public float playerTrackingForce;
	public GameManager gameManager;

	private bool isActive;
	private bool isCharging;
	private bool isSlowingDown;
	private Vector3 chargeTarget;


	public void Charge() {
		Debug.Log( "Torro!" );
		playerTrigger.stopLooking();
		isCharging = true;
		isSlowingDown = false;
		chargeTarget = player.transform.position;
		gameManager.torro();
		GetComponent<Rigidbody>().drag = 0;
		GetComponent<Rigidbody>().velocity = transform.forward * maxSpeed;

	}

	public void PassedPlayer() {
		isSlowingDown = true;
		GetComponent<Rigidbody>().drag = 1f;

	}

	public void disable() {
		halt();
		isActive = false;

	}

	public void halt() {
		GetComponent<Rigidbody>().Sleep();
		isCharging = false;
		isSlowingDown = false;
		gameManager.defaultInstructions();
	}

	// Use this for initialization
	void Start () {
		isCharging = false;
		isSlowingDown = false;
		isActive = true;
		GetComponent<Rigidbody>().drag = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		if( isActive && gameManager.canControl() ) {
			transform.LookAt( player.transform );

			if( !isCharging && Input.GetKeyDown("space") ) {
				Charge();
				gameManager.addStroke();
			}
			else if( isSlowingDown ) {
				GetComponent<Rigidbody>().AddForce( (player.transform.position - transform.position).normalized * playerTrackingForce );
				if( GetComponent<Rigidbody>().velocity.magnitude < 1 ) {
					halt();

				}
			}
		}
	}

	void FixedUpdate() {


	}


}
