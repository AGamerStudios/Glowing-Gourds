using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Weapon : MonoBehaviour {
	public string weaponName;
    [Range(1f,20f)]
	public float aimSpeed;
    [Range(0.25f,10f)]
	public float cooldown;
    [Range(5,100)]
	public int maxAmmo;
    [Range(1,50)]
	public int damage;
    public int price;
	public GameObject shellEjectionParticle;
	public GameObject shellLandingNoise;
	public GameObject gunShotNoise;
	public int roundsPerShot;
	public int ammo;
	protected Transform reticle;
	protected Color reticleColor;
	protected float lastFired;
	protected bool weaponEnabled = true;
	protected bool automatic = false;

	void Start(){
		reticle = transform.GetChild(0);
		reticleColor = Color.white;
        if(SceneManager.GetActiveScene().name.Equals("Demo_Level")){
			ammo = maxAmmo;
        }
		if(roundsPerShot > 1){
			automatic = true;
		}
	}

	void Update()
    {
        if (GetUserInput() && weaponEnabled)
        {
            FireWeapon();
        }

        if (!weaponEnabled && (Time.time - lastFired) >= 0.25f)
        {
            ChangeWeaponReticleColor(Color.grey);
        }

        if ((Time.time - lastFired) >= cooldown)
        {
            lastFired = 0.0f;
            weaponEnabled = true;
            ChangeWeaponReticleColor(Color.white);
        }

        if (ammo < 1)
        {
            weaponEnabled = false;
            ChangeWeaponReticleColor(Color.grey);
        }

        FollowCursor();
    }

    private bool GetUserInput()
    {
        return (Input.GetButton("Fire"));
    }

    void FollowCursor(){
		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, Time.deltaTime * aimSpeed);
	}

	public void ChangeWeaponReticleColor(Color newReticleColor){
		reticle.GetComponent<SpriteRenderer>().color = newReticleColor;
	}

	private void FireWeapon()
    {
        FindObjectOfType<Player>().TrackAmmo(weaponName, roundsPerShot);
        ChangeWeaponReticleColor(Color.red);
        InitShellParticles();
        StartCoroutine(SpawnWeaponAudio());
        SetWeaponStats();
        CalculateShot();
    }

    private void InitShellParticles()
    {
        ParticleSystem shellEjectParticle = Instantiate(shellEjectionParticle, reticle.position, Quaternion.identity).GetComponent<ParticleSystem>();
        var main = shellEjectParticle.main;
        main.maxParticles = roundsPerShot;
        shellEjectParticle.Play();
    }

    private void SetWeaponStats()
    {
        lastFired = Time.time;
        weaponEnabled = false;
        ammo-= roundsPerShot;
    }

    private void CalculateShot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 100f);
        if (hit.transform.tag == "Enemy")
        {
            hit.transform.GetComponent<Enemy>().RecieveDamage(damage);
        }
    }

	IEnumerator SpawnWeaponAudio(){
		for(int index =0; index < roundsPerShot; index++)
        {
            SpawnGunShot();
            SpawnShellLanding();
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
	}

    private void SpawnGunShot()
    {
        AudioSource gunshotAudio = Instantiate(gunShotNoise, Vector3.zero, Quaternion.identity).GetComponent<AudioSource>();
        gunshotAudio.pitch = Random.Range(0.9f, 1.1f);
        gunshotAudio.Play();
    }

	private void SpawnShellLanding()
    {
        AudioSource shellLandingAudio = Instantiate(shellLandingNoise, Vector3.zero, Quaternion.identity).GetComponent<AudioSource>();
        shellLandingAudio.pitch = Random.Range(0.9f, 1.1f);
        shellLandingAudio.Play();
    }
}
