using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class iTweenPathMoveable : MonoBehaviour
{

	public Vector3 PathOffset;
	public bool UseLocalPosition;

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
		enabled = false;
	}

	private void OnEnable()
	{
		EditorApplication.update += EditorUpdate;
	}

	void EditorUpdate()
	{
		for (int i = 0; i < path.nodeCount; i++)
		{
			if (UseLocalPosition)
				path.nodes[i] = pathNodes[i] + new Vector3(transform.parent.position.x, transform.parent.localPosition.y, 0) + PathOffset;
			else
				path.nodes[i] = pathNodes[i] + new Vector3(transform.parent.position.x, transform.parent.position.y, 0) + PathOffset;
		}
	}
	private void OnDisable()
	{
		EditorApplication.update -= EditorUpdate;
	}
}
