using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour {
	protected AudioSource audioSource;
	protected AudioClip audioClip;
	protected float durationCountDown;
	void Start(){
		audioSource = GetComponent<AudioSource>();
		audioClip = audioSource.clip;
		durationCountDown = audioClip.length;
	}
	void Update(){
		if(durationCountDown <= 0){
			Destroy(gameObject);
		}else{
			durationCountDown -= Time.deltaTime;
		}
	}
}
