using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpiderManager : StateManager {


	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere (transform.position, DetectionRange);
	}

	public override void Start(){
		base.Start ();
		ChangeState (GetState ("Idle"));
		PauseManager.Instance.addPausable (this);
	}

	void OnDisable(){
		PauseManager.Instance.removePausable (this);
	}

	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			CheckIfPlayerInRange ();
			StateTransition ();
		}
	}

	public override void StateTransition(){
		if(!GetComponent<AbsorbableCorruption>().HasEnergyLeft())
		{
			Debug.Log ("isdead");
			ChangeState(GetState("Dead"));
			CurrentState.OnUpdate();
		}
		base.StateTransition ();
		if (CompareToCurrentState (typeof(RangeAttack))) {
			//if the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				ChangeState (GetState ("Idle"));
			}
		}
	}

	public override void CheckIfPlayerInRange(){
		if(!CompareToCurrentState(typeof(StunnedState))){
			playerDistance = Vector3.Distance (Player.transform.position, transform.position);
			if (playerDistance <= DetectionRange) {
				if (!CompareToCurrentState (typeof(RangeAttack))) {
					ChangeState (GetState ("RangeAttack"));
				}
			}
		}
	}

	public override void Pause(){
		isPaused = true;
	}

	public override void UnPause(){
		isPaused = false;
	}
}
