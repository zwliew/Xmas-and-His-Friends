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
    public string sidename;

	private Vector3 v3originalPosition;
	private static Vector3 v3center = new Vector3(0f, 1.43f, 0f);

	void Awake(){
		curMat = GetComponent<Renderer>().sharedMaterial;
		Debug.Log ("curMat Get");
		particalSys = GetComponent<ParticleSystem> ();
		v3originalPosition = transform.position;
	}

	public void Restart(){
		
	}

	public void SetDisplay (Texture2D texture2D){
		curMat.SetTexture ("_MainTex", texture2D);
	}

	public void SetOriginalPosition(Vector3 pos){
		v3originalPosition = pos;
		//Debug.Log (sidename +"'s v3originalPosition is set to "+ v3originalPosition);
	}

	void Update(){
		//if(Time.frameCount % 5 == 0 && sidename.Equals("Nv")) Debug.Log (v3originalPosition);
	}
	
	public void Shutdown(){
		
	}

	public void SetSelected(){
		Debug.Log ("Selected: " + curMat.mainTexture.name);
		//particalSys = GetComponent<ParticleSystem> ();
		particalSys.Play ();
		StopAllCoroutines ();
		sidename = curMat.mainTexture.name;
		//Debug.Log ("when selected, the original location is " + v3originalPosition);
		StartCoroutine (MoveTo (v3center));
	}

	public void SetUnselected(){
		//particalSys = GetComponent<ParticleSystem> ();
		particalSys.Stop();
		StopAllCoroutines ();
		//Debug.Log ("when unselected, the original location is " + v3originalPosition);
		StartCoroutine (MoveTo (v3originalPosition));
	}

	IEnumerator MoveTo (Vector3 pos){
		if (NearMa (transform.position, pos)) {
			yield break;
		}else{
			Vector3 vel = Vector3.zero;
			//Debug.Log (name + transform.position + " is moving to " + pos);
			transform.position = Vector3.SmoothDamp (transform.position, pos, ref vel, 0.1f);
			yield return null;
		}
	}

	private bool NearMa(Vector3 pos1, Vector3 pos2){
		if (Mathf.Abs (pos1.x - pos2.x) + Mathf.Abs (pos1.y - pos2.y) + Mathf.Abs (pos1.z - pos2.z) < 0.5f) {
			return true;
		} else {
			return false;
		}
	}
}
