using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//GetComponent<Renderer>().material.SetTexture ("_MainTex", (Texture2D)listTextures[2]);  //Set texture

public class DebugScript : MonoBehaviour{
	
	private Material curMat;
	private Object[] listTextureObjects;
	private Dictionary <string, Texture2D> dicPianPang = new Dictionary<string, Texture2D>();

	void Awake(){

		curMat = GetComponent<Renderer>().material;

		listTextureObjects = Resources.LoadAll("PinZiPianPang", typeof(Texture2D));


		for (int i = 0; i < listTextureObjects.Length; i++) {
			Debug.Log (listTextureObjects [i] + " added to listTextures");
			dicPianPang.Add (listTextureObjects [i].name, (Texture2D)listTextureObjects [i]);
			Debug.Log (dicPianPang.Keys);
		}
	}

	void Start()
	{
		curMat.SetTexture ("_MainTex", (Texture2D)listTextureObjects[2]);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			print("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
			curMat.SetTexture ("_MainTex", (Texture2D)listTextureObjects[3]);
		}
	}

	
}