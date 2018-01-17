using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPinZiGame : MonoBehaviour {

	void OnTriggerStay2D(Collider2D collider){
		if (AnimationManager.state == 0) {
			Debug.Log ("Entering Next Scence");
			SceneManager.LoadScene("PinZiGame");
		}
	}
}
