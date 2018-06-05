using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public bool finalWave = false;
	[SerializeField]
	private GameObject resultsUI;
	private bool gameOver = false;
	private GameObject player, waveSpawner;
	private GameObject[] enemiesOnField;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		waveSpawner = GameObject.FindGameObjectWithTag("WaveSpawner");
		resultsUI.gameObject.SetActive(false);
	}

	void Update(){
		enemiesOnField = GameObject.FindGameObjectsWithTag("Enemy");
		if(finalWave && enemiesOnField.Length == 0 && !gameOver){
			LevelOver(true);
		}

		if(Input.GetButtonDown("Exit Level")){
			SceneManager.LoadScene("Main_Menu");
		}
	}

	public void LevelOver(bool playerWon){
		gameOver = true;

		Destroy(player);
		resultsUI.SetActive(true);

		if(playerWon){
			FindObjectOfType<ResultsManager>().GameWin();
		}else{
			Destroy(waveSpawner);
			FindObjectOfType<ResultsManager>().GameLose();
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene("Main_Menu");
	}

	public void LoadLobby(){
		SceneManager.LoadScene("Lobby");
	}
}
