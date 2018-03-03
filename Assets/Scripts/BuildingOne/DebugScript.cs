using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//GetComponent<Renderer>().material.SetTexture ("_MainTex", (Texture2D)listTextures[2]);  //Set texture

public class DebugScript : MonoBehaviour{
	public void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Default"))) {
				Debug.Log (hitInfo.collider.gameObject.name);
			}
		}
	}
	
}