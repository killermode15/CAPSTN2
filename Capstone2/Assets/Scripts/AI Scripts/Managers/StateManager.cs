using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager : MonoBehaviour, IPausable {

    public int HP;
    public float damage;
	public float collisionDamage;
	public GameObject Player;
	public List<State> PossibleStates;
	public State CurrentState;
	public float attackRange;
	public float DetectionRange;
	public float playerDistance;
	private State stateBeforeStun;
	public bool isPaused;
    public GameObject OrbPrefab;

    // Use this for initialization
    public virtual void Start () {
		PossibleStates = GetComponents<State>().ToList();
		PauseManager.Instance.addPausable (this);
		isPaused = false;
	}

	void Update(){
        StateTransition ();
        CheckIfDead();

    }

	public virtual void StateTransition()
	{
		if (CompareToCurrentState (typeof(StunnedState))) {
			if (!CurrentState.OnUpdate ()) {
				ChangeState (stateBeforeStun);
			}
		}
	}

    public virtual void CheckIfDead()
    {
        if (HP <= 0)
        {
            ChangeState(GetState("Dead"));
        }
    }

	public void ChangeState(State newState)
	{
		if (CurrentState)
		{
			CurrentState.enabled = false;
		}
		stateBeforeStun = CurrentState;
		CurrentState = newState;
		CurrentState.enabled = true;
	}

	public State GetState(string name)
	{
		return PossibleStates.Find(x => x.GetType().Name.ToLower() == name.ToLower());
	}

	public bool CompareToCurrentState(System.Type stateType)
	{
		if (CurrentState)
			return CurrentState.GetType() == stateType;
		else
			return false;
	}

	public virtual void CheckIfPlayerInRange(){
		
	}

	public void GetDamage()
	{
        //Debug.Log("getDamanged");
        //Add damage function here
        if (HP > 0)
        {
            HP -= 1;
        }
        
		#region Pseudo Code for conditions
		/// if( enemy still has orbs )
		/// {
		///		Spawn orb here
		///		Reduce health / corruption orb amount
		/// }
		#endregion

		//Temporary
		GameObject orbSpawned = 
            Instantiate(OrbPrefab, new Vector3(transform.position.x - 1.0f, transform.position.y + 3.5f, transform.position.z), Quaternion.identity);

	}

	public virtual void Pause(){
		isPaused = true;
	}

	public virtual void UnPause(){
		isPaused = false;
	}
}
