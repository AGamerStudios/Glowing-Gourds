using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {
	[SerializeField]
	private GameObject[] Dialogues;
	[SerializeField]
	private Image DialogueBackground;
	[SerializeField]
	private Sprite[] Backgrounds;
	private int dialogueIndex = 0;
	[SerializeField]
	private Animator DialogueAnimator;

	void Start(){
		DialogueBackground.sprite = Backgrounds[dialogueIndex];
	}

	void Update(){
		if(CheckForDialogueOver()){
			if(dialogueIndex < Dialogues.Length){
				dialogueIndex++;
				SpawnNextDialogue();
			}
		}
		if(dialogueIndex == Dialogues.Length || Input.GetButton("Exit Level")){
			SceneManager.LoadScene("Level_1");
		}
	}

	private bool CheckForDialogueOver(){
		if(!DialogueAnimator.GetBool("IsOpen")){
			return true;
		}
		return false;
	}

	private void SpawnNextDialogue(){
		Dialogues[dialogueIndex].GetComponent<DialogueTrigger>().TriggerDialogue();
		DialogueBackground.sprite = Backgrounds[dialogueIndex];
	}
}
