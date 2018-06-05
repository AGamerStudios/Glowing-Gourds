using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData{
	public string playerName;
	public string levelName;
	public int score;
	public int reloads;
	public KeyValuePair<string, bool>[] unlockedWeapons;

	public void DebugLogWeapons(){
		foreach(KeyValuePair<string, bool> keypair in unlockedWeapons){
            Debug.Log(keypair.Key + " : " + keypair.Value);
        }
	}
}

public class PlayerDataManager : MonoBehaviour {
	public PlayerData playerData;

	void Awake(){
		DontDestroyOnLoad(this);
	}
}
