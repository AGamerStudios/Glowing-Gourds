using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadGameController : MonoBehaviour {
	[SerializeField]
	private GameObject playerDataPrefab, gameSaveObjectParent, gameSavePrefab;
	private PlayerData[] gameSaves;
	private GameObject[] gameSaveButtons;

	void Start () {
		gameSaves = FindObjectOfType<GameDataManager> ().LoadGameSaves ();
		foreach (PlayerData gameSave in gameSaves){
			GameObject gameSaveObj = Instantiate(gameSavePrefab);
			gameSaveObj.transform.SetParent(gameSaveObjectParent.transform, false);
			gameSaveObj.transform.GetChild (0).GetComponent<TMP_Text>().text = gameSave.playerName;
			gameSaveObj.transform.GetChild (1).GetComponent<TMP_Text>().text = gameSave.levelName;
			gameSaveObj.transform.GetChild (2).GetComponent<TMP_Text>().text = gameSave.score.ToString();
			gameSaveObj.GetComponent<Button>().onClick.AddListener(delegate(){
				LoadGame(gameSave.playerName);
			});
		}
	}

	void LoadGame(string playerName)
    {
        DestroyPlayerDataObjects();
        Instantiate(playerDataPrefab);
        FindObjectOfType<PlayerDataManager>().playerData = FindObjectOfType<GameDataManager>().LoadGame(playerName);
        SceneManager.LoadScene("Lobby");
    }

    private static void DestroyPlayerDataObjects()
    {
        GameObject[] playerDataObjects = GameObject.FindGameObjectsWithTag("PlayerDataObject");
        foreach (GameObject playerDataObject in playerDataObjects)
        {
            Destroy(playerDataObject);
        }
    }

    public void ClearGameData(){
		GameDataManager.DeleteGameSaves();
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene("Main_Menu");
	}
}
