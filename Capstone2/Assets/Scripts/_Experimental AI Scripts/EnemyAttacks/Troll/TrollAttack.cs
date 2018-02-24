using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrollAttack : MonoBehaviour {

	private bool hasAttacked;
	public GameObject clubPrefabTest;

	void Start()
	{
		GetComponent<StateController> ().onStateTransition += ResetVariables;
	}

	void ResetVariables()
	{
		hasAttacked = false;
	}

	public void JumpAttack(StateController controller)
	{
		if (!controller.animator.GetBool ("IsSmashing") && !hasAttacked) {
			controller.animator.SetBool ("IsSmashing", true);
			hasAttacked = true;
		} else {
			if(controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
				controller.animator.SetBool ("IsSmashing", false);
		}
	}

	public void Charge(StateController controller)
	{
		if (!hasAttacked) {
			if (!controller.navMeshAgent.pathPending) {
				if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance) {
					if (!controller.navMeshAgent.hasPath || controller.navMeshAgent.velocity.sqrMagnitude == 0f) {
						Debug.Log ("Before Charge Update " + controller.navMeshAgent.speed);
						controller.navMeshAgent.speed = controller.Stats.ChargeSpeed;
						controller.navMeshAgent.SetDestination (controller.patrolPoints [controller.nextPatrolPoint].position);
						controller.navMeshAgent.Resume ();
						controller.nextPatrolPoint++;
						hasAttacked = true;
						if (controller.nextPatrolPoint >= controller.patrolPoints.Count)
							controller.nextPatrolPoint = 0;
					}
				}
			}
		}
	}

	public void ClubSmash(StateController controller)
	{
		if (!hasAttacked) {
			Debug.Log("I will club smash now");
			Vector3 location = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
			Instantiate(clubPrefabTest, location, Quaternion.identity);
			hasAttacked = true;
		}
	}
}
