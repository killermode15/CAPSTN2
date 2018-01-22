using UnityEngine;
using System.Collections;

public class AttackState : IEnemyState {
	
	private readonly StatePatternEnemy Enemy;


	public AttackState (StatePatternEnemy statePatternEnemy)
	{
		Enemy = statePatternEnemy;
	}

	public void UpdateState()
	{
        Attack ();
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

	public void ToDeadState()
	{
		Enemy.currentState = Enemy.deadState;
	}

	void Attack()
	{
		Enemy.moveSpeed = 0.0f;
		/*if (Vector3.Distance (Enemy.transform.position, Enemy.target.transform.position) <= Enemy.AttackRange)
        {
            //Enemy.navMeshAgent.Stop();
            
			//Enemy.anim.SetFloat ("Blend", 1f);
            //Enemy.navMeshAgent.destination = Enemy.target.transform.position;


        }
        else if (Vector3.Distance (Enemy.transform.position, Enemy.target.transform.position) > Enemy.AttackRange)
        {
			ToChaseState();
            
        }*/
	}

   


}
