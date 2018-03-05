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
    private int index;

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
            currentTarget = FindNearestVisibileEnemy();
            firstSelecting = false;
        }
        else if (!firstSelecting)
        {
            if (visibleEnemies.Count > 0)
            {
                Debug.Log(visibleEnemies.Count);

                if (Input.GetButtonDown("R3") && !switchButtonPressed)
                {
                    //if(currentTarget == visibleEnemies[visibleEnemies.Count])
                    switchButtonPressed = true;
                    index++;
                    Debug.Log(visibleEnemies.Count);
                    if (index > visibleEnemies.Count)
                    {
                        index = 0;
                    }
                    currentTarget = visibleEnemies[index];
                }
                else if (Input.GetButtonUp("R3") && switchButtonPressed)
                {
                    switchButtonPressed = false;
                }
            }
        }
        CrosshairLock();
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

    void CheckForEnemiesInScreen(/*GameObject enemy*/)
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
                PickTarget();
            }
        }

        Debug.Log(visibleEnemies.Count);
    }

    void CrosshairLock()
    {
        //spawns crosshair
        Crosshair.SetActive(true);
        Crosshair.transform.position = currentTarget.transform.position;
    }

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
                index = i;
            }

        }

        return nearestObject;
    }
}
