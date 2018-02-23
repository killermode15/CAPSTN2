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

	[HideInInspector] public NavMeshAgent navMeshAgent;
	/*[HideInInspector]*/ public List<Transform> patrolPoints;
	[HideInInspector] public int nextPatrolPoint;
	private bool isDoneWaiting;
	private bool isAIActive;

	// Use this for initialization
	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
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
			Debug.Log(newState);
			CurrentState = newState;

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
