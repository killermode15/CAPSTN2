using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager : MonoBehaviour {

	public List<State> PossibleStates;
	public State CurrentState;
	public float DetectionRange;

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, DetectionRange);
	}

	// Use this for initialization
	void Start () {
		PossibleStates = GetComponents<State>().ToList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StateTransition()
	{
		//Compare the current state to check if the current state is idle
		if (CompareToCurrentState(typeof(Idle)))
		{
			//If the current state is not updating
			if(!CurrentState.OnUpdate())
			{
				//Transition to Patrol
				ChangeState(GetState("Patrol"));
			}
		}
		else if(CompareToCurrentState(typeof(Patrol)))
		{
			//If the current state is not updating
			if (!CurrentState.OnUpdate())
			{
				//Transition to Patrol
				ChangeState(GetState("Idle"));
			}
		}
	}

	public void ChangeState(State newState)
	{
		CurrentState.enabled = false;
		CurrentState = newState;
		newState.enabled = true;
	}

	public State GetState(string name)
	{
		return PossibleStates.Find(x => x.name.ToLower() == name.ToLower());
	}

	bool CompareToCurrentState(System.Type stateType)
	{
		return CurrentState.GetType() == stateType;
	}
}
