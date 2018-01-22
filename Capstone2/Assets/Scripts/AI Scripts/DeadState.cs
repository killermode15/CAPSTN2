using UnityEngine;
using System.Collections;

public class DeadState : IEnemyState
{
	private readonly StatePatternEnemy Enemy;

	public DeadState (StatePatternEnemy statePatternEnemy)
	{
		Enemy = statePatternEnemy;
	}

	public void UpdateState()
	{
		Dead ();
	}

	public void ToPatrolState()
	{
		Enemy.currentState = Enemy.patrolState;
	}

	public void ToIdleState()
	{
		Debug.Log ("Can't transition to same state");
	}

	public void ToChaseState()
	{
		Enemy.currentState = Enemy.chaseState;

	}

	public void ToDeadState()
	{
		Enemy.currentState = Enemy.deadState;
	}

	void Dead()
	{

	}

}
