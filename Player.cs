using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public TMP_Text weaponText, ammoText, remainingAmmoText;
	public GameObject[] weapons;
	private GameObject selectedWeapon;
	private int currentWeaponIndex = 0;
    private int[] weaponAmmoTracker;

	void Start()
    {
        UpdateWeaponsAmmoTracker();
        SetWeapon(0);
    }

	void Update()
	{
		if (Input.GetAxis("Switch Weapon") > 0f && weapons.Length != 1){
			ChangeWeapon();
		}
		UpdateWeaponUI();

		if(Input.GetButtonDown("Reload") && FindObjectOfType<PlayerDataManager>().playerData.reloads > 0){
			ReloadWeapon();
		}
	}
	
	private void ReloadWeapon(){
		weaponAmmoTracker[currentWeaponIndex] = selectedWeapon.GetComponent<Weapon>().maxAmmo;
		FindObjectOfType<PlayerDataManager>().playerData.reloads--;
		SetWeapon(currentWeaponIndex);
	}

    private void UpdateWeaponsAmmoTracker()
    {
        List<int> weaponAmmoList = new List<int>();
        foreach (GameObject weapon in weapons)
        {
            weaponAmmoList.Add(weapon.GetComponent<Weapon>().maxAmmo);
        }
        weaponAmmoTracker = weaponAmmoList.ToArray();
    }

    private void SetWeapon(int weaponIndex)
    {
        Destroy(selectedWeapon);
        
        if(SceneManager.GetActiveScene().name.Equals("Demo_Level")){
            selectedWeapon = Instantiate(
                weapons[weaponIndex],
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Quaternion.identity
            );
            selectedWeapon.transform.SetParent(transform);
        }else{
            if (IsWeaponUnlocked(weaponIndex))
            {
                selectedWeapon = Instantiate(
                    weapons[weaponIndex],
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Quaternion.identity
                );
                selectedWeapon.transform.SetParent(transform);
                selectedWeapon.GetComponent<Weapon>().ammo = weaponAmmoTracker[weaponIndex];
            }
        }
    }

    public void UpdateWeaponUI()
    {
        weaponText.text = selectedWeapon.GetComponent<Weapon>().weaponName.ToUpper();
		if(selectedWeapon.GetComponent<Weapon>().ammo <= 0){
			ammoText.text = "0";
		}else{
			ammoText.text = selectedWeapon.GetComponent<Weapon>().ammo + "";
		}
        remainingAmmoText.text = selectedWeapon.GetComponent<Weapon>().maxAmmo + "";
    }

    private void ChangeWeapon()
    {
        if (currentWeaponIndex + 1 == weapons.Length){
            currentWeaponIndex = 0;
        }
        else{
            currentWeaponIndex += 1;
        }

        if(SceneManager.GetActiveScene().name.Equals("Demo_Level")){
            SetWeapon(currentWeaponIndex);
            return;
        }

        if(IsWeaponUnlocked(currentWeaponIndex)){
            Destroy(selectedWeapon);
            SetWeapon(currentWeaponIndex);
            return;
        }else{
            ChangeWeapon();
        }
        
    }

    private bool IsWeaponUnlocked(int weaponIndex)
    {
        return FindObjectOfType<PlayerDataManager>().playerData.unlockedWeapons[weaponIndex].Value;
    }

    public void TrackAmmo(string weaponName, int roundsPerShot){
        for(int index = 0; index < weapons.Length; index++){
            if(weapons[index].GetComponent<Weapon>().weaponName.Equals(weaponName)){
                weaponAmmoTracker[index] -= roundsPerShot;
            }
        }
    }
}
