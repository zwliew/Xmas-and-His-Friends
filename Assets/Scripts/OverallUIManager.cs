using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverallUIManager : MonoBehaviour {

	public CanvasGroup homeScreenCanvasGroup;
	public CanvasGroup RoomSelectionCanvasGroup;
	public CanvasGroup ShopCanvasGroup;
	public CanvasGroup EditorModeCanvasGroup;
	public CanvasGroup InGameUICanvasGroup;

	public Sprite sprSelected;

	public GameObject playerDataControllerGameObject;

	void Awake(){
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (true);
 
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
			//Debug.Log ("Fading out cvsGrp");
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
			//Debug.Log ("Fading in cvsGrp");
			yield return null;
		}
		cvsGrp.alpha = 1f;
		Debug.Log ("Enabled cvsGrp");
	}

	//-------------InGameUI---------------
	public void EnterShop(){
		StartCoroutine (ExitAfterTime (0.2f, InGameUICanvasGroup, ShopCanvasGroup));
	}

	public void EnterEditorMode(){
		StartCoroutine (ExitAfterTime (0.2f, InGameUICanvasGroup, EditorModeCanvasGroup));
	}
	public void ReturnToRoomSelection(){//Does not return actually, for some debug purposes
		homeScreenCanvasGroup.gameObject.SetActive (false);
		RoomSelectionCanvasGroup.gameObject.SetActive (false);
		ShopCanvasGroup.gameObject.SetActive (false);
		EditorModeCanvasGroup.gameObject.SetActive (false);
		InGameUICanvasGroup.gameObject.SetActive (true);
	}
	//----------------------------

	//-------------EditorMode---------------
	public void ExitEditorMode(){
		StartCoroutine (ExitAfterTime (0.1f, EditorModeCanvasGroup, InGameUICanvasGroup));
	}
	//----------------------------

	//-------------Shop---------------
	public void ExitShop(){
		StartCoroutine (ExitAfterTime (0.1f, ShopCanvasGroup, InGameUICanvasGroup));
	}
	//---------------------------

	//-------------SelectionOfRoom---------------
	public void EnterRoom(){
		
	}
	//---------------------------

	IEnumerator ExitAfterTime(float time, CanvasGroup closedCvsGrp, CanvasGroup openedCvsGrp){
		closedCvsGrp.GetComponent<Animator> ().SetTrigger ("Exit");

		float t = time;
		while (t > 0f || !closedCvsGrp.GetComponent<Animator> ().IsInTransition(0)) {
			//Debug.Log (closedCvsGrp.GetComponent<Animator> ().IsInTransition(0));
			t -= Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		closedCvsGrp.gameObject.SetActive (false);
		openedCvsGrp.gameObject.SetActive (true);

		if (openedCvsGrp.GetComponent<ShopController> ()) {
			openedCvsGrp.GetComponent<ShopController> ().Initialize ();
		}

		if (openedCvsGrp.GetComponent<EditorModeController> ()) {
			openedCvsGrp.GetComponent<EditorModeController> ().Initialize ();
		}
	}
		

}
