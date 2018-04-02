using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverallSceneManager : MonoBehaviour {

	private AsyncOperation async;

	public void LoadScene(string sceneName){
		GameObjectUtility.ClearObjectPools ();
		switch(sceneName){
		case "":
			break;
		default:
			SceneManager.LoadScene (sceneName);
			GameObject gO = GameObject.FindWithTag ("Transition");
			if(gO!=null)
				gO.GetComponent<SceneTransitionAnimator> ().PlayTransition ();
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
