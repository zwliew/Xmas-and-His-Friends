using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectionXmasAnimationController : MonoBehaviour {
	private Animator xmasAnimator;

	void Start(){
		xmasAnimator = GetComponent<Animator> ();
	}

	public void SetAnimationState(int state){
		xmasAnimator.SetInteger ("State", state);
		Debug.Log ("Setting animation State to " + state);
	}

	public void SetTrigger(string trg){
		xmasAnimator.SetTrigger (trg);
	}
}
