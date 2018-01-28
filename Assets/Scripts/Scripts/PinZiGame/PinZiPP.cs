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
	[HideInInspector]
    public string name;

	private Vector3 v3originalPosition;
	private static Vector3 v3center = new Vector3(0f, 1.43f, 0f);
	/*
	void Start(){
		curMat = GetComponent<Renderer>().sharedMaterial;
		particalSys = GetComponent<ParticleSystem> ();

	}*/

	public void Restart(){
		curMat = GetComponent<Renderer>().sharedMaterial;
		particalSys = GetComponent<ParticleSystem> ();
	}

	public void SetDisplay (Texture2D texture2D){
		curMat = GetComponent<Renderer>().sharedMaterial;
		curMat.SetTexture ("_MainTex", texture2D);
		v3originalPosition = transform.position;
		name = texture2D.name;
	}

	
	public void Shutdown(){
		
	}

	public void SetSelected(){
		Debug.Log ("Selected: " + curMat.mainTexture.name);
		particalSys = GetComponent<ParticleSystem> ();
		particalSys.Play ();
		StopCoroutine ("MoveTo");
		StartCoroutine (MoveTo (v3center));
	}

	public void SetUnselected(){
		particalSys = GetComponent<ParticleSystem> ();
		particalSys.Stop();
		StopCoroutine ("MoveTo");
		StartCoroutine (MoveTo (v3originalPosition));
	}

	IEnumerator MoveTo (Vector3 pos){
		while (!transform.position.Equals (pos)) {
			Vector3 vel = Vector3.zero;
			transform.position = Vector3.SmoothDamp (transform.position, pos, ref vel, 0.1f);
			yield return null;
		}
	}
}
