using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour {

	public void SaveGame(PlayerData playerData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/" + playerData.playerName + "_save.dat");
        binaryFormatter.Serialize(saveFile, playerData);
        saveFile.Close();
        PlayerPrefs.SetString("lastLevelCompleted", playerData.levelName);
		PlayerPrefs.SetString("playerName", playerData.playerName);
    }

    public PlayerData GeneratePlayerData(
		string playerName, 
		string levelName, 
		int playerScore, 
		int reloads, 
		KeyValuePair<string, bool>[] unlockedWeapons
	){
        PlayerData playerData = new PlayerData();
        playerData.playerName = playerName;
        playerData.levelName = levelName;
		playerData.reloads = reloads;
        playerData.score = playerScore;
		playerData.unlockedWeapons = unlockedWeapons;
        return playerData;
    }

    public PlayerData LoadGame(string playerName){
		if(File.Exists(Application.persistentDataPath + "/" + playerName + "_save.dat")){
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream saveFile = File.Open(Application.persistentDataPath + "/" + playerName + "_save.dat", FileMode.Open);
			PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(saveFile);
			saveFile.Close();
			
			PlayerPrefs.SetString("lastLevelCompleted", playerData.levelName);
			PlayerPrefs.SetString("playerName", playerData.playerName);
			
			return playerData;
		}
		return new PlayerData();
	}

	public PlayerData LoadGameByPath(string path){
		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		FileStream saveFile = File.Open(path, FileMode.Open);
		PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(saveFile);
		saveFile.Close();
		return playerData;
	}

	public PlayerData[] LoadGameSaves(){
		List<PlayerData> gameSaves = new List<PlayerData>();
		string[] saveFilePaths = Directory.GetFiles(Application.persistentDataPath);
		foreach(string saveFilePath in saveFilePaths){
			gameSaves.Add(LoadGameByPath(saveFilePath));
		}
		return gameSaves.ToArray();
	}

	public static void DeleteGameSaves(){
		string[] saveFilePaths = Directory.GetFiles(Application.persistentDataPath);
		foreach (string saveFilePath in saveFilePaths){
			File.Delete(saveFilePath);
		}
		PlayerPrefs.DeleteKey("lastLevelCompleted");
		SceneManager.LoadScene("Main_Menu");
	}
}
