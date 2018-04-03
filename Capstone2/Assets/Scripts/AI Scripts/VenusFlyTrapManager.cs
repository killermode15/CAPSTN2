using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusFlyTrapManager : MonoBehaviour {

    public GameObject Player;
    public float damage;
    public bool isAttacking;

	// Use this for initialization
	void Start () {
        isAttacking = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAttacking)
        {
            GetComponent<Animator>().SetBool("Idle", true);
            GetComponent<Animator>().SetBool("Attack", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Idle", false);
            GetComponent<Animator>().SetBool("Attack", true);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    public void DamagePlayer()
    {
        if(isAttacking)
            Player.GetComponent<HP>().RemoveHealth(damage);
    }

    public void IdleTrap()
    {
        isAttacking = false;
    }
}
