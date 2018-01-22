using UnityEngine;
using System.Collections;

public class IdleState : IEnemyState
{
	private readonly StatePatternEnemy Enemy;
	float Timer = 2.0f;

	public IdleState (StatePatternEnemy statePatternEnemy)
	{
		Enemy = statePatternEnemy;
	}

	public void UpdateState()
	{
        Timer += Time.deltaTime;
        DetectPlayer ();
		Idle ();
	}

	public void ToPatrolState()
	{
		Enemy.currentState = Enemy.patrolState;
		Timer = 1.0f;
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


	void Idle()
	{
		Debug.Log ("in idle state");
		Enemy.moveSpeed = 0.0f;
		//Debug.Log ("Timer:: " + Timer);
        if(Timer > Enemy.IdleTime)
		{
			ToPatrolState();
			Timer = 0;
		}
	}
    
	private void DetectPlayer()
	{
		Enemy.playerDistance = Vector3.Distance (Enemy.player.position, Enemy.transform.position);
		if(Enemy.playerDistance < Enemy.enemyDetect)
		{
			ToChaseState ();
		}
	}
    

}
