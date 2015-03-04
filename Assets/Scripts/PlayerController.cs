using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public float rotateSpeed = 10;
	public GameManager gameManager;
	
	private Rigidbody rbody;
	
	// Use this for initialization
	void Start () {
		// sometimes called "caching" a reference to a rigidbody
		rbody = GetComponent<Rigidbody>();
	}
	

	void FixedUpdate () {

		// No mouselook in this version.
		if( gameManager.canControl() ) {
			rbody.AddForce( transform.forward * Input.GetAxis("Vertical") * speed);
	//		rbody.AddForce( transform.right * Input.GetAxis("Horizontal") * speed);

			transform.Rotate( 0f, Input.GetAxis("Horizontal") * rotateSpeed, 0f );
		}
	}

	void OnCollisionEnter( Collision activator ) {
		if( activator.gameObject.CompareTag("Bull") ) {
			gameManager.gameOver();
		}
	}
}
