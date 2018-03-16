using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInteraction : MonoBehaviour {

	private RaycastHit hitInfo;

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hitInfo, 300f, 1 << LayerMask.NameToLayer ("Road"))) {
				Debug.Log (hitInfo.collider.gameObject.name);
				AudioSource audio =	hitInfo.collider.gameObject.GetComponent<AudioSource> ();
				if (audio != null) {
					audio.Play();
				}
			}
		}


	}
}
