using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour {
	protected ParticleSystem.MainModule particleSystemMain;
	protected float durationCountDown;
	void Start(){
		particleSystemMain = GetComponent<ParticleSystem>().main;
		durationCountDown = particleSystemMain.duration;
	}
	void Update(){
		if(durationCountDown <= 0){
			Destroy(gameObject);
		}else{
			durationCountDown -= Time.deltaTime;
		}
	}
}
