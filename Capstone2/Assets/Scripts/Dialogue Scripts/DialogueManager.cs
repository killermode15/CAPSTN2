using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private bool inDialogue;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public GameObject Player;

    private Queue<string> sentences;
    private bool isTextFinished;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        //Player.GetComponent<PlayerController> ().InDialogue = inDialogue;
        if (inDialogue)
        {
            //Player.GetComponent<PlayerController> ().CanJump = false;
            //if (Input.GetButtonDown ("Cross"))
            if (isTextFinished)
                StartCoroutine(WaitAndDisplay(4.0f));
        }
    }

    IEnumerator WaitAndDisplay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isTextFinished = false;
        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;

        //Debug.Log ("dialogue startnow");

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        //clear previous sentences
        sentences.Clear();

        //loop over sentences in the dialogue then add to queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        isTextFinished = true;
    }

    void EndDialogue()
    {
        //Player.GetComponent<PlayerController> ().CanJump = false;
        inDialogue = false;
        //Debug.Log ("ending dialogue");
        PauseManager.Instance.UnPause();
        animator.SetBool("IsOpen", false);
    }
}
