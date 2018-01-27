using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

	Image img;

	void Start(){
		StartCoroutine( Fade());
	}

	IEnumerator Fade(){
		for (float i = 0.5f; i > 0f; i -= 0.1f) {
			Debug.Log ("imagine that it is being faded out");
			yield return null;
		}
	}
}
