using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectionSprfAni : MonoBehaviour {
	private Animator SprfAnimator;

	void Start(){
		SprfAnimator = GetComponent<Animator> ();
	}

//	public void SetAnimationState(int state){
//		SprfAnimator.SetInteger ("State", state);
//		Debug.Log ("Setting animation State to " + state);
//	}

	public void SetTrigger(string trg){
		SprfAnimator.SetTrigger (trg);
	}
}
