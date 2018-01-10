using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	private Animator animatorXmas;
    private static int state = 0;

	public static void SetState(int stateno){
		state = stateno;
	}

	void Awake(){
		animatorXmas = GetComponent<Animator> ();

	}
		
	// Update is called once per frame
	void Update () {
		
		animatorXmas.SetInteger ("State", state);
	}
}
