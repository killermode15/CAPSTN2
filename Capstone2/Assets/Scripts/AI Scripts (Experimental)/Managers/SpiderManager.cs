using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpiderManager : StateManager {

	public float damping;
	float rotateTo;
	float turnSmoothVel;

	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere (transform.position, DetectionRange);
	}

	void Start(){
		base.Start ();
		ChangeState (GetState ("Idle"));
	}

	void Update(){
		CheckIfPlayerInRange ();
		StateTransition ();
	}

	public override void StateTransition(){
		if (CompareToCurrentState (typeof(RangeAttack))) {
			//if the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				ChangeState (GetState ("Idle"));
			}
		}
	}

	public override void CheckIfPlayerInRange(){
		playerDistance = Vector3.Distance (Player.transform.position, transform.position);
		if (playerDistance <= DetectionRange) {
			if (!CompareToCurrentState (typeof(RangeAttack))) {
				LookAtPlayer ();
			}
		}
	}

	void LookAtPlayer(){
		/*Vector3 rotation = (Player.transform.position - transform.position);
		rotation.Normalize ();
		rotateTo = Mathf.Atan2 (rotation.x, rotation.y) * Mathf.Rad2Deg;
		if (transform.eulerAngles.z != rotateTo) {
			transform.eulerAngles = 
				new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			transform.eulerAngles = Vector3.right * Mathf.SmoothDampAngle (transform.eulerAngles.x, rotateTo, ref turnSmoothVel, damping);
		}*/

		//Transform.rotation = new Vector3 (-180, -90, 90);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-180, -90, 90), Time.deltaTime * damping);
		Debug.Log (transform.eulerAngles.x);
		if (transform.eulerAngles.x >= 355.0f || transform.eulerAngles.x >= -0.1f)
			ChangeState (GetState ("RangeAttack"));
	}
}
