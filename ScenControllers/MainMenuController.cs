using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

[System.Serializable]
class GameData{
	public string title;
	public string[] version;
	public string author;
	public static GameData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GameData>(jsonString);
    }
}

public class MainMenuController : MonoBehaviour {
	public Button loadGamesButton;
	public TMP_Text titleText, authorText, versionText;
	public GameObject OptionsDataPrefab;

	void Awake()
	{
		foreach (GameObject gameSettingObject in GameObject.FindGameObjectsWithTag("GameSettingObject"))
		{
			Destroy(gameSettingObject);
		}
		Instantiate(OptionsDataPrefab).GetComponent<OptionSingelton>().gameSettings = OptionsController.GetGameSettings();
	}

	void Start()
	{
		string json = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath,"game-info.json"));
		GameData gameData = GameData.CreateFromJSON(json);

		titleText.text = gameData.title.ToUpper();
		authorText.text = gameData.author;
		versionText.text = gameData.version[0] + ": " + gameData.version[1] + gameData.version[2];

		PlayerData[] gameSaves = FindObjectOfType<GameDataManager>().LoadGameSaves();
		if (gameSaves.Length >= 1){
			loadGamesButton.interactable = true;
		}

	}

	public void StartNewGame(){
		SceneManager.LoadScene("PlayerName");
	}

	public void OpenLoadGames(){
		SceneManager.LoadScene("Load_Game");
	}
	
	public void OpenOptionsMenu(){
		SceneManager.LoadScene("Options");
	}

	public void LoadDemo(){
		SceneManager.LoadScene("Demo_Intro");
	}

	public void QuitApplication(){
		PlayerPrefs.DeleteAll();
		Application.Quit();
	}
}
