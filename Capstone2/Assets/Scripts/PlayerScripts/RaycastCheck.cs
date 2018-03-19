using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour {

<<<<<<< HEAD
    public float PushForce;
=======
    RaycastHit hit;
>>>>>>> 8c48023cc230bf3ff25de4dc68a807da2ed32302
    public float sphereRadius;
    public bool isOnVines;
    public float x;
    public float y;
    public float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        /*if(Physics.Raycast(transform.position, -Vector3.up, out hit))
=======
		/*if(Physics.Raycast(transform.position, -Vector3.up, out hit))
>>>>>>> 8c48023cc230bf3ff25de4dc68a807da2ed32302
        {
            if (hit.collider.CompareTag("Vine"))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 5, transform.position.x), 3.0f * Time.deltaTime);
            }
        }*/

<<<<<<< HEAD
        //if(Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit))

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit))
        {
            if (hit.collider.CompareTag("Vine"))
            {
                Vector3 normalDirection = new Vector3(hit.normal.normalized.x, 0, hit.normal.normalized.z);
                //normalDirection = transform.TransformDirection(normalDirection);
                isOnVines = true;
                GetComponent<PlayerController>().ApplyExternalForce(normalDirection * PushForce);
                //check if left or right of vine (if left, positive. else, negative)
                //transform.position = 
                //    Vector3.MoveTowards(transform.position, 
                //    new Vector3(transform.position.x - x, transform.position.y - y, transform.position.x), time * Time.deltaTime);
=======
        if(Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit))
        {
            if (hit.collider.CompareTag("Vine"))
            {
                isOnVines = true;
                //check if left or right of vine (if left, positive. else, negative)
                transform.position = 
                    Vector3.MoveTowards(transform.position, 
                    new Vector3(transform.position.x - x, transform.position.y - y, transform.position.x), time * Time.deltaTime);
>>>>>>> 8c48023cc230bf3ff25de4dc68a807da2ed32302
            }
            else
            {
                isOnVines = false;
<<<<<<< HEAD
                GetComponent<PlayerController>().ApplyExternalForce(Vector3.zero);
=======
>>>>>>> 8c48023cc230bf3ff25de4dc68a807da2ed32302
            }
        }

        Debug.DrawRay(transform.position, -Vector3.up);
	}
}
