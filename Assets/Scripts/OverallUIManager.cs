using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallUIManager : MonoBehaviour {

	public CanvasGroup homeScreenCanvasGroup;
	public CanvasGroup RoomSelectionCanvasGroup;
	public CanvasGroup ShopCanvasGroup;
	public CanvasGroup EditorModeCanvasGroup;
	public CanvasGroup InGameUICanvasGroup;

	void Awake(){
		homeScreenCanvasGroup.gameObject.SetActive (true);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (false);
	}

	public void EnterBuildingSelection(){
		FadeOutCanvas ();
		FadeInBuildingSelection ();
	}

	public void FadeOutCanvas(){
		StartCoroutine (FadeOutCoroutine (homeScreenCanvasGroup));
	}

	public void FadeInBuildingSelection(){
		StartCoroutine (FadeInCoroutine (RoomSelectionCanvasGroup));
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
	//-------------InGameUI---------------
	public void EnterShop(){
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (true);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (false);
	}

	public void EnterEditorMode(){
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (true);
		InGameUICanvasGroup.gameObject.SetActive (false);
	}
	public void ReturnToRoomSelection(){
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (true);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (false);
	}
	//-------------InGameUI---------------

	//-------------EditorMode---------------
	public void ExitEditorMode(){
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (true);
	}
	//-------------EditorMode---------------

	//-------------Shop---------------
	public void ExitShop(){
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (true);
	}
	//-------------Shop---------------

	//-------------SelectionOfRoom---------------
	public void EnterRoom(){
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (true);
	}
	//-------------SelectionOfRoom---------------
}
