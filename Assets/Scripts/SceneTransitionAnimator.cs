using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionAnimator : MonoBehaviour {

	private Animator aniController;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		aniController = GetComponentInChildren<Animator> ();
	}

	public void PlayTransition(){
		Debug.Log ("TransitionAnimation");
		aniController.Play ("Transitiontest");
	}

	void Update(){
		if (Input.GetKey (KeyCode.A)) {
			PlayTransition ();
		}
	}
}
