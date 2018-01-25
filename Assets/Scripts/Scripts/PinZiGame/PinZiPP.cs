using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is attached to the pianpang prefab and
 * controls the spritePianpang that should be displayed
 */

public class PinZiPP : MonoBehaviour, IRecycle {

	private Material curMat;
    public string name;


	public void Restart(){
		curMat = GetComponent<Renderer>().material;
	}

	public void SetDisplay (Texture2D texture2D){
		curMat.SetTexture ("_MainTex", texture2D);
	}

	
	public void Shutdown(){
		
	}
}
