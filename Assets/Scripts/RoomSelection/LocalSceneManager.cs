using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSceneManager : MonoBehaviour {
	OverallSceneManager sceneManager;
	// Use this for initialization
	void Start () {
		sceneManager = GameObject.Find ("OverallSceneManager").GetComponent<OverallSceneManager>();
	}

	public void LoadScene(string sceneName){
		sceneManager.LoadScene (sceneName);
	}
}
