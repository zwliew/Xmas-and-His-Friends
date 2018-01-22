using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {
	private Rigidbody rbXmas;
	int i = 0;
	void Start(){
		i = 0;
		rbXmas = GetComponent<Rigidbody> ();
	}

	void Update(){
		if(Input.GetMouseButtonDown(1)){
			Debug.Log ("go up please");
				StartCoroutine (GoUp ());
		}
	}

	IEnumerator GoUp(){
		rbXmas.MovePosition (transform.position + new Vector3 (0f, 0.5f, 0f));
		yield return new WaitForEndOfFrame();
		if (i < 100) {
			i += 1;
			StartCoroutine (GoUp ());
		}
	}

}

