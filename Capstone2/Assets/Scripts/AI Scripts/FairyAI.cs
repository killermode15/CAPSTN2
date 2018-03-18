using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyAI : MonoBehaviour {

    Transform destinations;
    float patrolPause;
    private float moveSpeed;

    private int CurrentPatrolPoint;

    public Vector2 newPosition;

	// Use this for initialization
	void Start () {
        /*moveSpeed = 3.0f;

        PatrolPoints[0].transform.position = transform.position;
        PatrolPoints[1].transform.position = transform.position + new Vector3(10.0f, 10.0f, 10.0f);*/
    }
	
	// Update is called once per frame
	void Update () {
        /*transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].position, moveSpeed * Time.deltaTime);

        CurrentPatrolPoint++;
        if (CurrentPatrolPoint >= PatrolPoints.Count)
        {
            CurrentPatrolPoint = 0;
        }*/
        NewPosition();
        transform.position = Vector3.MoveTowards
            (transform.position, new Vector3(newPosition.x, transform.position.y, newPosition.y), moveSpeed * Time.deltaTime);
    }

    void NewPosition()
    {
        newPosition = Random.insideUnitCircle * 5;
    }
}
