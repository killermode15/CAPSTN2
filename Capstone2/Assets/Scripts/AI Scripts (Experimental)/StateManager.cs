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
		ChangeState(GetState("Idle"));
	}
	
	// Update is called once per frame
	void Update () {
		StateTransition();
		Debug.Log(PossibleStates[0].GetType().Name.ToLower());
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
		if (CurrentState)
		{
			CurrentState.enabled = false;
		}
		CurrentState = newState;
		newState.enabled = true;
	}

	public State GetState(string name)
	{
		return PossibleStates.Find(x => x.GetType().Name.ToLower() == name.ToLower());
	}

	bool CompareToCurrentState(System.Type stateType)
	{
		if (CurrentState)
			return CurrentState.GetType() == stateType;
		else
			return false;
	}
}
