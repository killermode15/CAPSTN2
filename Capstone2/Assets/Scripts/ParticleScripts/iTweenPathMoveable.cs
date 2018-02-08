using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class iTweenPathMoveable : MonoBehaviour
{

	public Vector3 PathOffset;
	public bool EnableMove;
	public bool UseLocalPosition;
	public bool CanChangeNodePositions;
	public bool DisableOnStart;

	public iTweenPath path;

	public List<Vector3> pathNodes;
	public List<Vector3> originalNodes;

	// Use this for initialization
	void Start()
	{
		if (DisableOnStart)
			enabled = false;
		GetNodes();
	}

	void Update()
	{

		if (pathNodes == null)
			GetNodes();

		if (CanChangeNodePositions)
		{
			for (int i = 0; i < path.nodeCount; i++)
			{
				pathNodes[i] = path.nodes[i] + new Vector3(transform.parent.position.x, transform.parent.localPosition.y, 0) + PathOffset;
			}
		}
		else
		{

			if (EnableMove && !CanChangeNodePositions)
			{
				for (int i = 0; i < path.nodeCount; i++)
				{
					if (UseLocalPosition && pathNodes != null)
						path.nodes[i] = pathNodes[i] + new Vector3(transform.parent.position.x, transform.parent.localPosition.y, 0) + PathOffset;
					else if (!UseLocalPosition && pathNodes != null)
						path.nodes[i] = pathNodes[i] + new Vector3(transform.parent.position.x, transform.parent.position.y, 0) + PathOffset;
				}
			}
			else if (!EnableMove && !CanChangeNodePositions)
			{
				if (pathNodes != null)
				{
					for (int i = 0; i < path.nodeCount; i++)
					{
						path.nodes[i] = pathNodes[i];
					}
				}
			}
		}
	}

	void GetNodes()
	{
		pathNodes = new List<Vector3>();
		if (!path)
			path = GetComponent<iTweenPath>();
		for (int i = 0; i < path.nodeCount; i++)
		{
			pathNodes.Add(path.nodes[i]);
		}
	}
}
