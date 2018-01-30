using UnityEngine;
using System.Collections;

public class StatePatternEnemy: MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

	public float IdleTime;
	public float AttackRange;
	public float moveSpeed;
	public float damping;
	public float playerDistance;
	public float enemyDetect;
	public Transform player;

	public Transform[] patrolPoint;

    public IEnemyState currentState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public PatrolState patrolState;
	[HideInInspector] public IdleState idleState;
	[HideInInspector] public AttackState attackState;
	[HideInInspector] public DeadState deadState;
	//[HideInInspector] public UnityEngine.AI.NavMeshAgent navMeshAgent;

	private void Awake()
	{

		chaseState = new ChaseState (this);
		patrolState = new PatrolState (this);
		idleState = new IdleState (this);
		attackState = new AttackState (this);
		deadState = new DeadState (this);
		//navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	void Start ()
	{
		anim = GetComponent<Animator>();
		currentState = patrolState;
	
	}

	void Update () 
	{
		currentState.UpdateState ();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * AttackRange);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position + (transform.up * -0.05f), enemyDetect);
	}

}