using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
	public Queue<string> sentences;
	public TMP_Text characterName, dialogueText, dialogueButtonText;
	public Animator animator;
	private int sentenceIndex = 0;

	public void StartDialogue(DialogueObject dialogue){
		animator.SetBool("IsOpen", true);
		characterName.text = dialogue.characterName;
		sentences = new Queue<string>();
		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}
		DisplaySentence();
	}

	public void DisplaySentence()
    {
		dialogueButtonText.text = ">>";
        if (sentences.Count == 1){
            dialogueButtonText.text = "End >>";
        }
        if (sentences.Count == 0){
            EndDialoge();
            return;
        }
        DisplayDialogue(sentences.Dequeue());
    }

	public int GetDialogueSentenceIndex(){
		return sentences.Count;
	}

    private void DisplayDialogue(string sentence)
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()){
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialoge(){
		animator.SetBool("IsOpen", false);
	}
}
