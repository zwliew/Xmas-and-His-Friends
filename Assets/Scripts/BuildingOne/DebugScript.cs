using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//GetComponent<Renderer>().material.SetTexture ("_MainTex", (Texture2D)listTextures[2]);  //Set texture

public class DebugScript : MonoBehaviour{
	public GameObject gameObjectBtn;

	void Start(){
		Button btn = GameObjectUtility.customInstantiate (gameObjectBtn, Vector3.zero).GetComponent<Button> ();
		Debug.Log (btn);
		btn.GetComponent<EditorModeItem> ().Initialize ();
	}
	
}