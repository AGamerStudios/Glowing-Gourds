using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoIntroController : MonoBehaviour {
	public Animator animator;
	void Update(){
		if (!animator.GetBool("IsOpen")){
			SceneManager.LoadSceneAsync("Demo_Level");
		}
	}
}
