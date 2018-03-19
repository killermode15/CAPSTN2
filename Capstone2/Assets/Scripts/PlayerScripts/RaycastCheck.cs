using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour {

    RaycastHit hit;
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

        if(Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit))
        {
            if (hit.collider.CompareTag("Vine"))
            {
                isOnVines = true;
                //check if left or right of vine (if left, positive. else, negative)
                transform.position = 
                    Vector3.MoveTowards(transform.position, 
                    new Vector3(transform.position.x - x, transform.position.y - y, transform.position.x), time * Time.deltaTime);
            }
            else
            {
                isOnVines = false;
            }
        }

        Debug.DrawRay(transform.position, -Vector3.up);
	}
}
