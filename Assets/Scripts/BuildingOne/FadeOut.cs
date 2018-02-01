using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {
	CanvasGroup canvasGroup;
	float velocity;

	public void FadeOutOnLoad(){
		canvasGroup = GetComponent<CanvasGroup> ();

		StartCoroutine (Fade ());
	}

	IEnumerator Fade(){
		yield return new WaitForSeconds (0.5f);
		while (canvasGroup.alpha > 0f) {
			canvasGroup.alpha -= 0.025f;
			yield return null;
		}
		canvasGroup.interactable = false;
	}
}
