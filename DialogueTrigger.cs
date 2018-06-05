using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	public bool playOnStart = false;
	public DialogueObject dialogue;

	void Start(){
		if(playOnStart){
			TriggerDialogue();
		}
	}

	public void TriggerDialogue(){
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public void EndDialog(){
		FindObjectOfType<DialogueManager>().EndDialoge();
	}

	public int GetCurrentDialogueSentenceIndex(){
		return FindObjectOfType<DialogueManager>().GetDialogueSentenceIndex();
	}
}
