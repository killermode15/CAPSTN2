using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PeaShooterManager : StateManager {


	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, DetectionRange);
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}

	// Use this for initialization
	void Start () {
		base.Start ();
		ChangeState(GetState("Patrol"));
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

	public override void StateTransition()
	{
		base.StateTransition ();
		/*if(!GetComponent<AbsorbableCorruption>().HasEnergyLeft())
		{
			ChangeState(GetState("Dead"));
			CurrentState.OnUpdate();
		}*/
		if (CompareToCurrentState (typeof(Patrol))) {
			//If the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				ChangeState (GetState ("PeaChase"));
			}
		} else if (CompareToCurrentState (typeof(PeaChase))){
			if (!CurrentState.OnUpdate())
			{
				ChangeState(GetState("Patrol"));
			}
		}
	}

	public override void CheckIfPlayerInRange()
	{
		playerDistance = Vector3.Distance(Player.transform.position, transform.position);
		if (playerDistance <= DetectionRange)
		{
			if (!CompareToCurrentState(typeof(Chase)) && !CompareToCurrentState(typeof(Attack)) && !CompareToCurrentState(typeof(StunnedState)))
				ChangeState(GetState("PeaChase"));
		}
	}

	public override void Pause(){
		isPaused = true;
	}

	public override void UnPause(){
		isPaused = false;
	}
}
