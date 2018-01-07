using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 6.0f;
	private Vector3 moveDirection = Vector3.zero;
	Rigidbody rBody;

	void Start(){
		rBody = GetComponent<Rigidbody> ();
		rBody.freezeRotation = true;
	}
	void Update(){
		Move ();
	}

	void Move(){
		CharacterController controller = GetComponent<CharacterController> ();

		moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= speed;

		controller.Move (moveDirection * Time.deltaTime);
	}
}
