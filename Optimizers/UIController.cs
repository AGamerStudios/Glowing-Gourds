using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
	protected float durationCountDown = 3f;
	void Update(){
		if(durationCountDown <= 0){
			Destroy(gameObject);
		}else{
			durationCountDown -= Time.deltaTime;
		}
	}
}
