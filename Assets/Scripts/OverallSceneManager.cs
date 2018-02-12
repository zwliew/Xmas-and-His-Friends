using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverallSceneManager : MonoBehaviour {

	private AsyncOperation async;

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
		CharacterSpawner.EndGameClear ();
		LoadSceneAsync ("BuildingOne");
	}

	IEnumerator LoadSceneAsync (string sceneName){
		async = SceneManager.LoadSceneAsync(sceneName);
		while (!async.isDone) {
			yield return null;
		}
	}

	public static void EnterGame(string gameName){
	}

}
