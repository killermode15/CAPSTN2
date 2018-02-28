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
		
	}

	public void Charge(StateController controller)
	{
//		Debug.Log("I am charging");
		if (!hasAttacked && !controller.animator.GetBool ("ChargeUp")) {
			if (!controller.navMeshAgent.pathPending) {
				if (!controller.navMeshAgent.hasPath || controller.navMeshAgent.velocity.sqrMagnitude == 0f) {
					controller.navMeshAgent.speed = controller.Stats.ChargeSpeed;
					controller.navMeshAgent.SetDestination (controller.Destination.position);
					controller.nextPatrolPoint++;
					controller.navMeshAgent.isStopped = false;
					//controller.animator.SetBool ("ChargeUp", true);	
					hasAttacked = true;

					if (controller.nextPatrolPoint >= controller.patrolPoints.Count)
						controller.nextPatrolPoint = 0;
				}
			}
		}else if(hasAttacked && controller.animator.GetBool("ChargeUp")) {
			if (controller.animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1) {
				//controller.animator.SetBool ("ChargeUp", false);
				hasAttacked = false;
			}
		}
	}

	public void ClubSmash(StateController controller)
	{
		if (!controller.animator.GetBool ("ClubSmash") && !hasAttacked) {
			Vector3 location = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
			Instantiate(clubPrefabTest, location, Quaternion.identity);
			//controller.animator.SetBool ("ClubSmash", true);
			hasAttacked = true;
		}else if(hasAttacked && controller.animator.GetBool("ClubSmash")) {
			//if(controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
				//controller.animator.SetBool ("ClubSmash", false);
		}
	}
}
