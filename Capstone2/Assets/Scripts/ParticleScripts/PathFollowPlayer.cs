using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowPlayer : MonoBehaviour {

	public Transform Player;

	private iTweenPath path;

	private void Start()
	{
		path = GetComponent<iTweenPath>();

	}

	// Update is called once per frame
	void Update () {

		path.nodes[path.nodeCount - 2] = Vector3.Lerp(transform.position, Player.position, 0.5f) ;
		path.nodes[path.nodeCount - 1] = Player.position;
	}
}
