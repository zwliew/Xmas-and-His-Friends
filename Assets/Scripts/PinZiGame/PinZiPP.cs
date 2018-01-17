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

	[HideInInspector]
	public int spriteNo;

	private float lifetime = 2f;

	public static List<int> theList = new List<int> ();

	// Use this for initialization
	void Start () {
		
	}

	public void Restart(){
		lifetime = 2f;
		spriteNo = PinZiGM.GetNextNo ();
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = spritePianPang [7 - spriteNo];
	}
	
	// Not Very Useful
	void Update () {
		//delete the pianpangs after some time
		if (lifetime > 0) {
			lifetime -= Time.deltaTime;
		} else {
			GameObjectUtility.customDestroy (gameObject);
		}
	}

	public void SetSelected(){
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = spritePianPang[spriteNo];
	}
	
	public void Shutdown(){
		
	}
}
