using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowPlayer : MonoBehaviour
{

	public Transform Player;
	public Transform PathFollowReference;
	public int SmoothCount;

	private iTweenPath path;

	private void Start()
	{
		path = GetComponent<iTweenPath>();

	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < SmoothCount; i++)
		{

			int index = path.nodeCount - (SmoothCount - i);
			//Debug.Log(index);
			if (i != SmoothCount-1)
			{
				float count = SmoothCount;
				float perc = (float)i / count;
				//Debug.Log(((SmoothCount - 1) - (i + 1));// "Iteration Index: " + i + " / Perc : " + perc);
				path.nodes[index] = Vector3.Lerp(PathFollowReference.position, Player.position, perc);
			}
			else
				path.nodes[path.nodeCount - 1] = Player.position;
		}
		//path.nodes[path.nodeCount - 3] = Vector3.Lerp(transform.position, Player.position, 0.33f);
		//path.nodes[path.nodeCount - 2] = Vector3.Lerp(transform.position, Player.position, 0.66f) ;
		//path.nodes[path.nodeCount - 1] = Player.position;
	}
}
