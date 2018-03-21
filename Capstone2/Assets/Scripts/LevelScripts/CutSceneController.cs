using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour {

    public GameObject Camera;
    public List<iTweenPath> Paths;
    public Image FadeImage;
    public GameObject PressToContinue;

    private int currentPathIndex;
    private bool canShowNextFrame;

	// Use this for initialization
	void Start () {
        currentPathIndex = 0;
        canShowNextFrame = true;
        NextFrame();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            NextFrame();
            //NextPage();
        }
    }

    void NextFrame()
    {
        if (canShowNextFrame)
        {
            if (currentPathIndex < Paths.Count)
            {
                Camera.GetComponent<ParticleFollowPath>().PathName = Paths[currentPathIndex].pathName;
                Camera.GetComponent<ParticleFollowPath>().Activate();

                currentPathIndex++;
                StartCoroutine(WaitForFrame());
            }
            else
            {
                StartCoroutine(FadeScreen(4));
            }
        }
    }

    IEnumerator WaitForFrame()
    {
        PressToContinue.SetActive(false);
        canShowNextFrame = false;
        yield return new WaitForSeconds(Camera.GetComponent<ParticleFollowPath>().TimeToFinish);
        canShowNextFrame = true;
        PressToContinue.SetActive(true);
    }

    IEnumerator FadeScreen(float duration)
    {
        float alpha = 0;
        float perc = 0;
        float currTime = 0;

        while(perc < 1)
        {
            currTime += Time.deltaTime;
            perc = currTime / duration;
            alpha = Mathf.Lerp(alpha, 1, perc);
            //FadeImage.material.color = new Color(FadeImage.material.color.r, FadeImage.material.color.g, FadeImage.material.color.b, alpha);
            FadeImage.GetComponent<Image>().color = new Color(0,0,0, alpha);
            yield return new WaitForEndOfFrame();
        }

        StartMainGame();
    }

    void StartMainGame()
    {
        Debug.Log("start game");
        SceneManager.LoadScene("MainGame");
    }
}
