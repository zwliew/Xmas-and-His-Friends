using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//GetComponent<Renderer>().material.SetTexture ("_MainTex", (Texture2D)listTextures[2]);  //Set texture

public class DebugScript : MonoBehaviour{
	CanvasGroup canvasGroup;
	float velocity;

	public void FadeOut(){
		canvasGroup = GetComponent<CanvasGroup> ();
		StartCoroutine (Fade ());
	}

	IEnumerator Fade(){
		yield return new WaitForSeconds (0.5f);
		while (canvasGroup.alpha > 0f) {
			canvasGroup.alpha -= 0.025f;
			yield return null;
		}
	}
	
}