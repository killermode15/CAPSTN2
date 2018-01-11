using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;

	void Start () {
		sentences = new Queue<string> ();
	}

	void Update(){
		if (Input.GetButtonDown ("Fire2"))
			DisplayNextSentence ();
	}

	public void StartDialogue(Dialogue dialogue){

		animator.SetBool ("IsOpen", true);

		nameText.text = dialogue.name;

		//clear previous sentences
		sentences.Clear ();

		//loop over sentences in the dialogue then add to queue
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	public void DisplayNextSentence(){
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}

		string sentence = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
	}

	IEnumerator TypeSentence (string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue(){
		animator.SetBool ("IsOpen", false);
	}
}
