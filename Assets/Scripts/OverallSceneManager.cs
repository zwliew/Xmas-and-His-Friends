using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverallSceneManager : MonoBehaviour {

	private AsyncOperation async;

	public void Start(){
		DontDestroyOnLoad (gameObject);
	}

	public void LoadScene(string sceneName){
		GameObjectUtility.ClearObjectPools ();
		switch(sceneName){
		case "":
			break;
		default:
			GameObject gO = GameObject.FindWithTag ("Transition");
			switch (sceneName) {
			case "Intro":
				break;
			case "RoomSelection":
				break;
			case "HomeScreen":
				break;
			case "NewPinZiGame":
				break;
			case "maze":
				break;
			}
			if (gO != null)
				gO.GetComponent<SceneTransitionAnimator> ().PlayTransition ();
			StartCoroutine (LoadSceneAfterTime (1.3f, sceneName));
			break;
		}
	}

	IEnumerator LoadSceneAfterTime(float t, string sceneName){
		//yield return new WaitForSeconds (t);
		//SceneManager.LoadScene (sceneName);
		yield return StartCoroutine(LoadSceneAsync(sceneName));
		
	}
	
	IEnumerator LoadSceneAsync (string sceneName){
		Debug.Log ("waiting for transition animation to be played first");
		yield return new WaitForSeconds (1f);
		Debug.Log ("Start async");
		async = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Single);
		async.allowSceneActivation = true;
		while (!async.isDone) {
			yield return new WaitForFixedUpdate();
		}
		Debug.Log ("Finished async");
	}
	
//	//-------------------------------
//	public void LoadSceneForUI(){
//		GameObjectUtility.ClearObjectPools ();
//		CharacterSpawner.EndGameClear ();
//		LoadSceneAsync ("BuildingOne");
//	}
//
//	public static void EnterGame(string gameName){
//	}
}
