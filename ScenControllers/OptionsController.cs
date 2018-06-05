using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings{
	public int masterVolume;
	public bool masterVolumeMute;
	public static GameSettings CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GameSettings>(jsonString);
    }
}

public class OptionsController : MonoBehaviour
{
	[Header("Master Volume UI:")] [SerializeField] private TMP_Text MasterVolumeLabel;
	[SerializeField] private Slider MasterVolumeSlider;
	[SerializeField] private Toggle MasterVolumeMute;
	[Header("UI Controls")] [SerializeField] private Button ResetButton;
	[SerializeField] private Button DoneButton;
	[SerializeField] private Button ApplyButton;

	private int savedMasterVolume;
	private bool savedMasterVolumeMuted;

	void Start()
	{
		ConfigureMasterVolume();
		ApplyButton.interactable = false;
	}

	void Update()
	{
		ManageMasterVolume();
		if (
			MasterVolumeSlider.value != savedMasterVolume ||
			MasterVolumeMute.isOn != savedMasterVolumeMuted
		)
		{
			ApplyButton.interactable = true;
		}
		else
		{
			ApplyButton.interactable = false;
		}
	}

	private void ConfigureMasterVolume()
	{
		if (ConvertIntToBool(PlayerPrefs.GetInt("SavedSettings")))
		{
			MasterVolumeSlider.value = PlayerPrefs.GetInt("MasterVolume");
			savedMasterVolume = PlayerPrefs.GetInt("MasterVolume");
			MasterVolumeMute.isOn = ConvertIntToBool(PlayerPrefs.GetInt("MasterVolumeMuted"));
			savedMasterVolumeMuted = ConvertIntToBool(PlayerPrefs.GetInt("MasterVolumeMuted"));
		}
		else
		{
			SetDefaultOptions();
		}
	}

	private static GameSettings GetDefaultOptions()
	{
		string json = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "game-settings.json"));
		GameSettings gameSettings = GameSettings.CreateFromJSON(json);
		return gameSettings;
	}

	private void SetDefaultOptions()
	{
		GameSettings gameSettings = GetDefaultOptions();
		MasterVolumeSlider.value = gameSettings.masterVolume;
		savedMasterVolume = gameSettings.masterVolume;
		MasterVolumeMute.isOn = gameSettings.masterVolumeMute;
		savedMasterVolumeMuted = gameSettings.masterVolumeMute;
	}

	private void ManageMasterVolume()
	{
		MasterVolumeLabel.text = (int) MasterVolumeSlider.value + "%";
		if (MasterVolumeMute.isOn)
		{
			MasterVolumeSlider.interactable = false;
			MasterVolumeLabel.text = "Muted";
			MasterVolumeLabel.color = Color.red;
		}
		else
		{
			MasterVolumeLabel.color = Color.white;
			MasterVolumeSlider.interactable = true;
		}
	}

	private static bool ConvertIntToBool(int input)
	{
		if (input == 1)
		{
			return true;
		}
		return false;
	}

	public void SaveOptions()
	{
		PlayerPrefs.SetInt("SavedSettings", 1);
		PlayerPrefs.SetInt("MasterVolume", (int) MasterVolumeSlider.value);

		savedMasterVolume = (int) MasterVolumeSlider.value;
		savedMasterVolumeMuted = MasterVolumeMute.isOn;

		if (MasterVolumeMute.isOn)
		{
			PlayerPrefs.SetInt("MasterVolumeMuted", 1);
		}
		else
		{
			PlayerPrefs.SetInt("MasterVolumeMuted", 0);
		}
	}

	public void ResetOptions()
	{
		SetDefaultOptions();
		SaveOptions();
	}

	public void LeaveOptions()
	{
		SceneManager.LoadScene("Main_Menu");
	}

	public static GameSettings GetGameSettings()
	{
		GameSettings gameSettings = new GameSettings();
		if (ConvertIntToBool(PlayerPrefs.GetInt("SavedSettings")))
		{
			gameSettings.masterVolume = PlayerPrefs.GetInt("MasterVolume");
			gameSettings.masterVolumeMute = ConvertIntToBool(PlayerPrefs.GetInt("MasterVolumeMuted"));
		}
		else
		{
			gameSettings = GetDefaultOptions();
		}
		return gameSettings;
	}

	public void DeleteGameData()
	{
		PlayerPrefs.DeleteAll();
	}

	public void DeletePlayerData()
	{
		GameDataManager.DeleteGameSaves();
	}

}
