using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenPathMoveable : MonoBehaviour {
	
	public Vector3 PathOffset;

	private iTweenPath path;

	private List<Vector3> pathNodes;

	// Use this for initialization
	void Start()
	{
		pathNodes = new List<Vector3>();
		if (!path)
			path = GetComponent<iTweenPath>();
		for (int i = 0; i < path.nodeCount; i++)
		{
			pathNodes.Add(path.nodes[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < path.nodeCount; i++)
		{
			path.nodes[i] = pathNodes[i] + new Vector3(transform.parent.position.x, transform.parent.position.y,0) + PathOffset;
		}
	}
}
