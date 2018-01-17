using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is attached to the pianpang prefab and
 * controls the spritePianpang that should be displayed
 * 
*/
public class PinZiPP : MonoBehaviour, IRecycle {

	public Sprite[] spritePianPang;
	public GameObject prefabGlowingEffect;

	[HideInInspector]
	public int spriteNo;
	public bool dead;
	public static List<int> theList = new List<int> ();

	// Use this for initialization
	void Start () {
		
	}

	public void Restart(){
		spriteNo = 0;
		dead = false;
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = spritePianPang [7 - spriteNo];
	}
	
	// Not Very Useful
	void Update () {
		//delete the pianpangs after some time
		if (dead == true)
			GameObjectUtility.customDestroy (gameObject);
	}

	public void SetSelected(){
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = spritePianPang[spriteNo];
		GameObjectUtility.customInstantiate (prefabGlowingEffect, this.transform.position);
	}
	
	public void Shutdown(){
		
	}
}
