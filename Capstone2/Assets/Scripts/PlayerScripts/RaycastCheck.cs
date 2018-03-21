using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour {

    public float PushForce;
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
        /*if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.CompareTag("Vine"))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 5, transform.position.x), 3.0f * Time.deltaTime);
            }
        }*/

        //if(Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit))

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit, 0.25f))
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
            }
            else
            {
                isOnVines = false;
                GetComponent<PlayerController>().ApplyExternalForce(Vector3.zero);
            }
        }

        Debug.DrawRay(transform.position, -Vector3.up);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position - Vector3.up, sphereRadius);
	}


}
