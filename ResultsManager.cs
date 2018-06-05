using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour {
	public TMP_Text scoreText, totalText;
	public GameObject scorePanel, deathPanel;
	public PlayerData playerData;
	private int totalScore = 0;

	public void GameWin(){
		deathPanel.SetActive (false);
		scorePanel.SetActive (true);

		if (playerData.score >= 0 && SceneManager.GetActiveScene().name != "Demo_Level"){
			playerData = FindObjectOfType<PlayerDataManager>().playerData;
		}

		int levelScore = FindObjectOfType<ScoreKeeper>().score;
		scoreText.text = levelScore + "";
		totalText.text = totalScore + "";
		StartCoroutine (CalculateTotalScore (levelScore));
	}

	public void GameLose(){
		scorePanel.SetActive (false);
		deathPanel.SetActive (true);
	}

	IEnumerator CalculateTotalScore(int levelScore){
		if (playerData.score >= 0 && SceneManager.GetActiveScene().name != "Demo_Level"){
			totalScore = playerData.score;
		}
		if(SceneManager.GetActiveScene().name!= "Demo_Level"){
			FindObjectOfType<PlayerDataManager>().playerData = FindObjectOfType<GameDataManager>().GeneratePlayerData(
				PlayerPrefs.GetString("playerName"), 
				SceneManager.GetActiveScene().name, 
				totalScore + levelScore,
				0,
				FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons
			);
			FindObjectOfType<GameDataManager>().SaveGame(FindObjectOfType<PlayerDataManager>().playerData);
		}
		for (int index = 0; index < levelScore; index++){
			totalScore++;
			totalText.text = totalScore + "";
			yield return null;
		}
	}
}
