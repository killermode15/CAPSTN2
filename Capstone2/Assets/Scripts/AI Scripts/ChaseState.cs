using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState 
{
    [HideInInspector]
    public Animator anim;
	private readonly StatePatternEnemy Enemy;

	public ChaseState (StatePatternEnemy statePatternEnemy)
	{
		Enemy = statePatternEnemy;
	}

	public void UpdateState()
	{
        CheckIfCanAttack ();
        Chase();
    }

	public void ToPatrolState()
	{
        Enemy.currentState = Enemy.patrolState;
	}

	public void ToChaseState()
	{
        Enemy.currentState = Enemy.chaseState;
	}

	public void ToIdleState()
	{
		Enemy.currentState = Enemy.idleState;
	}

	public void ToAttackState()
	{     
        Enemy.currentState = Enemy.attackState;
	}

	public void ToDeadState()
	{
		Enemy.currentState = Enemy.deadState;
	}


	private void Chase()
	{
		Debug.Log ("in chase state");
		//LookAtPlayer();
		Enemy.moveSpeed = 2.0f;
		Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Enemy.player.position, Enemy.moveSpeed * Time.deltaTime);
		Enemy.playerDistance = Vector3.Distance (Enemy.player.position, Enemy.transform.position);
		if(Enemy.playerDistance > Enemy.enemyDetect)
		{
			ToIdleState ();
		}
	}

    private void CheckIfCanAttack()
	{
		Vector3 distance = new Vector3 (Enemy.player.position.x, Enemy.transform.position.x, 0.0f);
		if (distance.magnitude <= 3) {
			//to Attack
			Debug.Log("WE ATTACKIG");
			ToAttackState ();
		}
    }

	void LookAtPlayer(){
		///Tangina, paps. kung san san tumitingin and napupunta habang naka LookRotation si Enemy xD
		Quaternion rotation = Quaternion.LookRotation (Enemy.player.position - Enemy.transform.position);
		Enemy.transform.rotation = Quaternion.Slerp (Enemy.transform.rotation, rotation, Time.deltaTime * Enemy.damping);
	}

   
}