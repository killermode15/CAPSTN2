using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
	public float MoveAwaySpeed;
	public float ChargeSpeed;
	public float ChargeDuration;
	public AnimationCurve ChargeCurve;

	private float chargeValue;
	private float initialChargeValue;
	private bool doneAttacking;
	private Vector3 chargeDir;


	public override void OnEnable()
	{
		GetComponent<Collider>().isTrigger = true;
		base.OnEnable();
		if (ChargeDuration <= 0)
			ChargeDuration = 1;
		chargeValue = ChargeDuration;
		initialChargeValue = ChargeDuration;
		doneAttacking = false;

		chargeDir = (new Vector3(Manager.Player.transform.position.x, 0, 0) - new Vector3(transform.position.x, 0, 0)).normalized;
		

	}

	public override bool OnUpdate()
	{
		if (!doneAttacking)
		{
            //Charge();
            transform.LookAt(new Vector3(Manager.Player.transform.position.x, transform.position.y, Manager.Player.transform.position.z));
            if (Manager.Player.GetComponent<HP>().Health > 0)
            {
                Debug.Log(Manager.Player.GetComponent<HP>().Health);
                Bite();
            }
		}
		else
		{
            /*transform.position += chargeDir * MoveAwaySpeed * Time.deltaTime;
			GetComponent<Collider>().isTrigger = false;
			if (Vector3.Distance(transform.position, Manager.Player.transform.position) > Manager.attackRange)
			{
				return false;
			}*/
            return false;
		}

		return true;
	}

    void Bite()
    {
        //stops
        float distance = Vector3.Distance(transform.localPosition, Manager.Player.transform.localPosition);
        if (distance >= Manager.attackRange)
        {
            doneAttacking = true;
        }
        GetComponentInChildren<Animator>().SetBool("Slither", false);
        GetComponentInChildren<Animator>().SetBool("Bite", true);

        /*if ()
        {
            doneAttacking = true;
        }*/
    }

	void Charge()
	{
		chargeValue -= Time.deltaTime;
		float currentDashValue = chargeValue / initialChargeValue;
		//transform.position = Vector3.Lerp(transform.position, dir * (ChargeCurve.Evaluate(currentDashValue) * ChargeSpeed), currentDashValue);
		transform.position += chargeDir * (ChargeCurve.Evaluate(currentDashValue) * ChargeSpeed);

		if (currentDashValue <= 0)
			doneAttacking = true;
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1.5f);
	}

	public override void OnDisable()
	{
		base.OnDisable();
		GetComponent<Collider>().isTrigger = false;
		chargeDir = Vector3.zero;
		doneAttacking = false;
		
	}
}
