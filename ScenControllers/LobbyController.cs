using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour {
	public TMP_Text lastLevelText;
	
	void Start(){
		if(!PlayerPrefs.GetString("lastLevelCompleted").Equals(null)){
			lastLevelText.text = PlayerPrefs.GetString("lastLevelCompleted");
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene ("Main_Menu");
	}

	public void LoadStore()
    {
        SceneManager.LoadScene("Store");
    }

	public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level_" + CalculateNextLevelIndex());
    }

    private static int CalculateNextLevelIndex()
    {
        int lastLevelIndex = int.Parse(PlayerPrefs.GetString("lastLevelCompleted").Split('_')[1]);
        int nextLevelIndex = lastLevelIndex + 1;
        return nextLevelIndex;
    }
}
