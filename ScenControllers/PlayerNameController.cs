using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerNameController : MonoBehaviour {
	[SerializeField]
	private GameObject 
		enterPlayerNameDialogue,
		playerNameUI,
		dialogueButton,
		welcomePlayerDialogue,
        playerDataPrefab,
        startWeaponPrefab;
	private string playerName;

	void Start(){
        foreach(GameObject playerData in GameObject.FindGameObjectsWithTag("PlayerDataObject")){
            Destroy(playerData);
        }
        Instantiate(playerDataPrefab);
		playerNameUI.SetActive(false);
	}

	void Update ()
    {
		DisplayPlayerNameInput();

        if(CheckForPlayerNameInput()){
			SetPlayerName();
			SceneManager.LoadScene("Intro");
		}
    }

    private bool CheckForPlayerNameInput()
    {
		bool inputFocused =  playerNameUI.GetComponentInChildren<TMP_InputField>().isFocused;
		string inputValue = playerNameUI.GetComponentInChildren<TMP_InputField>().text;
		
        if(playerNameUI.activeInHierarchy && !inputFocused &&  inputValue!= "")
        {
            return true;
        }
		return false;
    }

    private void SetPlayerName()
    {
        dialogueButton.GetComponent<Button>().interactable = true;
        enterPlayerNameDialogue.GetComponent<DialogueTrigger>().EndDialog();

        playerName = playerNameUI.GetComponentInChildren<TMP_InputField>().text;
        playerNameUI.GetComponentInChildren<TMP_InputField>().interactable = false;

		PlayerPrefs.SetString("playerName", playerName);
        
        FindObjectOfType<PlayerDataManager>().playerData = FindObjectOfType<GameDataManager>().GeneratePlayerData(
            PlayerPrefs.GetString("playerName"),
            "Level_0",
            0,
			0,
            new KeyValuePair<string, bool>[]{
                new KeyValuePair<string, bool>("Pistol", true),
                new KeyValuePair<string, bool>("Assault Rifle", false),
                new KeyValuePair<string, bool>("Shotgun", false),
                new KeyValuePair<string, bool>("Sniper Rifle", false)
            }
        );

		Destroy(enterPlayerNameDialogue);
    }

    private void DisplayPlayerNameInput()
    {
        if(enterPlayerNameDialogue.GetComponent<DialogueTrigger>().GetCurrentDialogueSentenceIndex() == 0){
            playerNameUI.SetActive(true);
            dialogueButton.GetComponent<Button>().interactable = false;
        }
    }
}
