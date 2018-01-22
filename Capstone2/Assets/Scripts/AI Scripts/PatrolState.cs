using UnityEngine;
using System.Collections.Generic;
public class PatrolState : IEnemyState
{
	private readonly StatePatternEnemy Enemy;

	bool atPoint0;
	int x;

	void Start(){
		
	}

	public PatrolState (StatePatternEnemy statePatternEnemy)
	{
		Enemy = statePatternEnemy;
	}

	public void UpdateState()
	{
		DetectPlayer ();
		Patrol ();
	}

	public void ToPatrolState()
	{
		Debug.Log ("Can't transition to same state");
	}

	public void ToChaseState()
	{
		Enemy.currentState = Enemy.chaseState;
	}

	public void ToIdleState()
	{
		Enemy.currentState = Enemy.idleState;
	}

	public void ToDeadState()
	{
		Enemy.currentState = Enemy.deadState;
	}

	void Patrol ()
	{
		Enemy.moveSpeed = 2.0f;
		if (atPoint0 == true) {
			x = 1;
			//Debug.LogError ("tangina x = 1");
			//atPoint0 = false;
		} else if (atPoint0 == false) {
			x = 0;
			//Debug.LogError ("tangina x = 0");
			//atPoint0 = true;
		}
		Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Enemy.patrolPoint[x].position, Enemy.moveSpeed * Time.deltaTime);
		Vector3 distance = new Vector3 (Enemy.patrolPoint [x].position.x, Enemy.transform.position.x, 0.0f);
		Debug.Log ("distance: " + distance);
		if (distance.magnitude <= 4/* || distance.magnitude >= 11*/) {
			//reached point
			ToIdleState ();
			if (atPoint0 == true) {
				atPoint0 = false;
			} else if (atPoint0 == false) {
				atPoint0 = true;
			}
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