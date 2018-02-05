using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverallSceneManager : MonoBehaviour {

	public static void LoadScene(string sceneName){
		switch(sceneName){
		case "":
			break;
		default:
			SceneManager.LoadScene (sceneName);
			break;
		}
	}

	public void LoadSceneForUI(){
		GameObjectUtility.ClearObjectPools ();
		SceneManager.LoadScene ("BuildingOne");
	}

	public static void EnterGame(string gameName){
	}

}
