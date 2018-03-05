using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour
{

	public GameObject Crosshair;
	public GameObject currentTarget;
	public List<GameObject> allEnemies = new List<GameObject>();
	public List<GameObject> visibleEnemies = new List<GameObject>();
	bool firstSelecting;
	bool switchButtonPressed;
	private int selectedTarget;
	private List<GameObject> sortedVisibleEnemies;

	// Use this for initialization
	void Start()
	{
		FindAllEnemies();
		//allEnemies = GameObject.FindObjectsOfType<Absorbable>().ToList();

		Crosshair = Instantiate(Crosshair);
		Crosshair.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		CheckIfInCombat();
		SortListByDistance();
		PickTarget();

	}

	void CheckIfInCombat()
	{
		if (Input.GetButtonDown("LeftTrigger"))
			firstSelecting = true;
		if (Input.GetButton("LeftTrigger"))
		{
			CheckForEnemiesInScreen();
			//FindAllEnemies ();
		}
		else if (Input.GetButtonUp("LeftTrigger"))
		{
			Crosshair.SetActive(false);
		}
	}

	void PickTarget()
	{
		// if nothing to switch to, set Crosshair.SetActive(false);
		if (firstSelecting)
		{
			if (sortedVisibleEnemies.Count > 0)
				currentTarget = sortedVisibleEnemies[0];
			firstSelecting = false;
			selectedTarget = 0;
		}
		else if (!firstSelecting)
		{
			if (sortedVisibleEnemies.Count > 0)
			{
				currentTarget = sortedVisibleEnemies[selectedTarget];

				if (Input.GetKeyDown(KeyCode.Joystick1Button11))
				{
					Debug.Log("R3 Pressed");
					selectedTarget++;
					Debug.Log(visibleEnemies.Count);
					if (selectedTarget >= visibleEnemies.Count)
					{
						selectedTarget = 0;
					}
				}

				#region old code for targeting
				//Debug.Log(visibleEnemies.Count);

				//if (Input.GetButtonDown("R3") && !switchButtonPressed)
				//{
				//	if (currentTarget == visibleEnemies[visibleEnemies.Count])
				//		switchButtonPressed = true;
				//	index++;
				//	Debug.Log(visibleEnemies.Count);
				//	if (index > visibleEnemies.Count)
				//	{
				//		index = 0;
				//	}
				//	currentTarget = visibleEnemies[index];
				//}
				//else if (Input.GetButtonUp("R3") && switchButtonPressed)
				//{
				//	switchButtonPressed = false;
				//}
				#endregion
			}
		}
		CrosshairLock();
	}

	#region Finding Enemies

	GameObject FindNearestVisibileEnemy()
	{
		//The nearest object (default if the first in the list)
		GameObject nearestObject = (visibleEnemies.Count > 0) ? visibleEnemies[0].gameObject : null;

		//If there is no nearest object, return null
		if (!nearestObject)
			return null;

		//Loop through all objects
		for (int i = 0; i < visibleEnemies.Count; i++)
		{
			//If the object is the same as the current nearest object
			if (visibleEnemies[i].gameObject == nearestObject)
				//Continue to the next iteration
				continue;

			//The distance of the object and player
			float distBetweenPlayer = (transform.position - visibleEnemies[i].transform.position).magnitude;
			//The distance between the current nearest object and the player
			float distBetweenNearest = (transform.position - nearestObject.transform.position).magnitude;

			//If the distance of the object to the player is lesser than the 
			//distance of the current nearest object to the player
			if (distBetweenPlayer < distBetweenNearest)
			{
				//Set the current object to the nearest object.
				nearestObject = visibleEnemies[i].gameObject;
				selectedTarget = i;
			}

		}
		return nearestObject;
	}

	GameObject FindNearestVisibileEnemy(List<GameObject> listOfEnemies)
	{
		//The nearest object (default if the first in the list)
		GameObject nearestObject = (listOfEnemies.Count > 0) ? listOfEnemies[0].gameObject : null;

		//If there is no nearest object, return null
		if (!nearestObject)
			return null;

		//Loop through all objects
		for (int i = 0; i < listOfEnemies.Count; i++)
		{
			//If the object is the same as the current nearest object
			if (listOfEnemies[i].gameObject == nearestObject)
				//Continue to the next iteration
				continue;

			//The distance of the object and player
			float distBetweenPlayer = (transform.position - listOfEnemies[i].transform.position).magnitude;
			//The distance between the current nearest object and the player
			float distBetweenNearest = (transform.position - nearestObject.transform.position).magnitude;

			//If the distance of the object to the player is lesser than the 
			//distance of the current nearest object to the player
			if (distBetweenPlayer < distBetweenNearest)
			{
				//Set the current object to the nearest object.
				nearestObject = listOfEnemies[i].gameObject;
			}

		}
		return nearestObject;
	}

	void FindAllEnemies()
	{
		State[] allEnemies = FindObjectsOfType(typeof(State)) as State[];
		foreach (State Enemy in allEnemies)
		{
			//visibleEnemies.Add(Enemy);
			this.allEnemies.Add(Enemy.gameObject);
		}
		this.allEnemies = this.allEnemies.Distinct().ToList();
	}

	void CheckForEnemiesInScreen()
	{
		visibleEnemies = new List<GameObject>();
		foreach (GameObject Enemy in allEnemies)
		{
			Vector3 screenPoint = Camera.main.WorldToViewportPoint(Enemy.transform.position);
			bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
			if (onScreen)
			{
				//CrosshairLock();
				visibleEnemies.Add(Enemy);
			}
		}

		Debug.Log(visibleEnemies.Count);
	}

	void SortListByDistance()
	{
		List<GameObject> visibleTargets = new List<GameObject>(visibleEnemies);
		List<GameObject> sortedList = new List<GameObject>();
		int index = 0;
		while (visibleTargets.Count > 0)
		{
			GameObject nearestTarget = FindNearestVisibileEnemy(visibleTargets);
			visibleTargets.Remove(nearestTarget);
			sortedList.Add(nearestTarget);
			index++;
			if (index >= 10000)
				UnityEditor.EditorApplication.isPlaying = false;
		}
		sortedVisibleEnemies = sortedList;
	}

	#endregion

	void CrosshairLock()
	{
		//spawns crosshair
		if (currentTarget)
		{
			Crosshair.SetActive(true);
			Crosshair.transform.position = currentTarget.transform.position;
		}
	}
}
