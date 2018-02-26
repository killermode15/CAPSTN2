using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
	public BossStats Stats;
	public BaseState CurrentState;
	public BaseState RemainState;

	public delegate void OnStateTransition();
	public OnStateTransition onStateTransition;
	public Transform Destination;

	[HideInInspector] public TrollAttack attackScript;
	[HideInInspector] public Animator animator;
	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public BossHealth bossHealth;
	/*[HideInInspector]*/ public List<Transform> patrolPoints;
	/*[HideInInspector]*/ public int nextPatrolPoint;
	private bool isDoneWaiting;
	private bool isAIActive;

	// Use this for initialization
	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator> ();
		attackScript = GetComponent<TrollAttack> ();
	}

	public virtual void SetupAI()
	{
		if (!isAIActive)
		{
			isAIActive = true;
			patrolPoints = GameObject.Find("TrollPatrol").GetComponentsInChildren<Transform>().ToList() ;
		}
	}

	// Update is called once per frame
	public virtual void Update()
	{
		Destination = patrolPoints [nextPatrolPoint];
		
		if (Input.GetKeyDown(KeyCode.Space))
			isAIActive = !isAIActive;

		if (!isAIActive || !CurrentState)
			return;

		CurrentState.UpdateState(this);


	}

	public void TransitionToState(BaseState newState)
	{
		if (newState != RemainState)
		{
			CurrentState = newState;
			if (onStateTransition != null)
				onStateTransition.Invoke ();

		}
	}

	public bool IsDoneWaiting()
	{
		if (!isDoneWaiting)
		{
			StartCoroutine(WaitForSeconds());
		}
		else if (isDoneWaiting)
		{
			isDoneWaiting = false;
			StopAllCoroutines();
			return true;
		}

		return false;
	}

	IEnumerator WaitForSeconds()
	{
		yield return new WaitForSeconds(Stats.WaitDuration);
		isDoneWaiting = true;
	}
}
