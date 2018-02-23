using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AI/States/State")]
public class BaseState : ScriptableObject {

	public List<Action> Actions;
	public List<Transition> Transitions;

	public void UpdateState(StateController controller)
	{
		DoActions(controller);
		CheckTransitions(controller);
	}

	public void DoActions(StateController controller)
	{
		for(int i = 0; i < Actions.Count; i++)
		{
			Actions[i].Act(controller);
		}
	}

	private void CheckTransitions(StateController controller)
	{
		for(int i = 0; i < Transitions.Count; i++)
		{
			bool decisionSucceeded = Transitions[i].Decision.Decide(controller);

			if(decisionSucceeded)
			{
				controller.TransitionToState(Transitions[i].TrueState);
			}
			else
			{
				controller.TransitionToState(Transitions[i].FalseState);
			}
		}
	}

}
