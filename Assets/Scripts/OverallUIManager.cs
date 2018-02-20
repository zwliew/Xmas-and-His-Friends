using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallUIManager : MonoBehaviour {

	public CanvasGroup homeScreenCanvasGroup;
	public CanvasGroup BuildingSelectionCanvasGroup;

	void Awake(){
		homeScreenCanvasGroup.gameObject.SetActive (true);
		BuildingSelectionCanvasGroup.gameObject.SetActive (false);
	}

	public void EnterBuildingSelection(){
		FadeOutCanvas ();
		FadeInBuildingSelection ();
	}

	public void FadeOutCanvas(){
		StartCoroutine (FadeOutCoroutine (homeScreenCanvasGroup));
	}

	public void FadeInBuildingSelection(){
		StartCoroutine (FadeInCoroutine (BuildingSelectionCanvasGroup));
	}

	IEnumerator FadeOutCoroutine(CanvasGroup cvsGrp){
		float currentVelocity = 0f;
		cvsGrp.alpha = 1f;
		while (cvsGrp.alpha > 0.01f) {
			cvsGrp.alpha = Mathf.SmoothDamp (cvsGrp.alpha, 0f, ref currentVelocity, 1f);
			Debug.Log ("Fading out cvsGrp");
			yield return null;
		}
		cvsGrp.alpha = 0f;
		cvsGrp.gameObject.SetActive(false);
		Debug.Log ("Disabled cvsGrp");
	}
	IEnumerator FadeInCoroutine(CanvasGroup cvsGrp){
		float currentVelocity = 0f;
		cvsGrp.alpha = 0f;
		for (int i = 0; i < 50; i++) {
			yield return null;
		}

		cvsGrp.gameObject.SetActive(true);
		while (cvsGrp.alpha < 0.99f) {
			cvsGrp.alpha = Mathf.SmoothDamp (cvsGrp.alpha, 1f, ref currentVelocity, 1f);
			Debug.Log ("Fading in cvsGrp");
			yield return null;
		}
		cvsGrp.alpha = 1f;
		Debug.Log ("Enabled cvsGrp");
	}
}
