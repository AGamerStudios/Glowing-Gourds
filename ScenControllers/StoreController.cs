using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoreController : MonoBehaviour {
	public TMP_Text moneyText;
	public GameObject inventoryGrid, inventoryObject;
	public Weapon[] weaponsTabInventory;

	void Start(){
		LoadAmmoInventory();
	}

	void Update(){
		moneyText.text = FindObjectOfType<PlayerDataManager>().playerData.score.ToString();
	}

	public void LoadAmmoInventory(){
		ClearInventory();
		GenerateAmmoInventoryObject();
	}

	private void GenerateAmmoInventoryObject(){
		GameObject ammoObject = Instantiate(inventoryObject);
		ammoObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "Gun Reload";
		ammoObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "";
		ammoObject.transform.GetChild(3).GetComponent<TMP_Text>().text = 5.ToString();

		if(5 > FindObjectOfType<PlayerDataManager>().playerData.score){
			ammoObject.transform.GetComponent<Button>().interactable = false;
		}

		ammoObject.GetComponent<Button>().onClick.AddListener(delegate(){
			PurchaseReload();
		});

		ammoObject.transform.SetParent(inventoryGrid.transform, false);
	}

	private void PurchaseReload(){
		FindObjectOfType<PlayerDataManager>().playerData.reloads += 1;
		FindObjectOfType<PlayerDataManager>().playerData.score -= 5;
		FindObjectOfType<GameDataManager>().SaveGame(FindObjectOfType<PlayerDataManager>().playerData);
		LoadAmmoInventory ();
	}

	public void LoadWeaponsInventory(){
		ClearInventory();
		foreach(Weapon weapon in weaponsTabInventory){
            GenerateWeaponInventoryObject(weapon);
        }
    }

    private void GenerateWeaponInventoryObject(Weapon weapon)
    {
        GameObject weaponObject = Instantiate(inventoryObject);
        weaponObject.transform.GetChild(0).GetComponent<Image>();
        ConfigureWeaponText(weapon, weaponObject);

		for(int index = 0; index < FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons.Length; index++){
			if(FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons[index].Key == weapon.weaponName && FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons[index].Value){
				weaponObject.transform.GetComponent<Button>().interactable = false;
			}
		}

        if(weapon.price > FindObjectOfType<PlayerDataManager>().playerData.score){
			weaponObject.transform.GetComponent<Button>().interactable = false;
		}
		
		weaponObject.GetComponent<Button>().onClick.AddListener(delegate(){
			PurchaseWeapon(weapon);
		});
        weaponObject.transform.SetParent(inventoryGrid.transform, false);
    }

	private void PurchaseWeapon(Weapon weapon){
		for(int index = 0; index < FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons.Length; index++){
			if(FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons[index].Key == weapon.weaponName){
				FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons[index] = new KeyValuePair<string,bool>(weapon.weaponName, true);
			}
		}
		FindObjectOfType<PlayerDataManager>().playerData.score -= weapon.price;
		FindObjectOfType<GameDataManager>().SaveGame(FindObjectOfType<PlayerDataManager>().playerData);
		LoadWeaponsInventory();
	}

    private void ConfigureWeaponText(Weapon weapon, GameObject weaponObject)
    {
        weaponObject.transform.GetChild(1).GetComponent<TMP_Text>().text = weapon.name;
        weaponObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "* " + weapon.maxAmmo + " rnds";
        weaponObject.transform.GetChild(3).GetComponent<TMP_Text>().text = weapon.price.ToString();
    }

    public void LoadUpgradesInventory(){
		ClearInventory();
	}

	void ClearInventory(){
		GameObject[] inventoryObjects = GameObject.FindGameObjectsWithTag("InventoryObject");
		if(inventoryObjects.Length > 0){
			foreach(GameObject inventoryObject in inventoryObjects){
				Destroy(inventoryObject);
			}
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene("Lobby");
	}
}
