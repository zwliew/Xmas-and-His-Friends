using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is attached to the pianpang prefab and
 * controls the spritePianpang that should be displayed
 * It also contains the information about the Pianpang displayed
 * Public void SetDisplay(Texture2D texture2D);//Set what to be displayed
 * PinZiPP.selected = bool; //Set selected
 * 
 */

public class PinZiPP : MonoBehaviour, IRecycle {

	private Material curMat;
	private ParticleSystem particalSys;
    public string name;

	void Start(){
		curMat = GetComponent<Renderer>().material;
		particalSys = GetComponent<ParticleSystem> ();

	}

	public void Restart(){
		curMat = GetComponent<Renderer>().material;
		particalSys = GetComponent<ParticleSystem> ();
	}

	public void SetDisplay (Texture2D texture2D){
		curMat.SetTexture ("_MainTex", texture2D);
	}

	
	public void Shutdown(){
		
	}

	public void SetSelected(){
		Debug.Log ("Selected: " + curMat);
		particalSys.Play ();
	}

	public void SetUnselected(){
		particalSys.Stop();
	}
}
