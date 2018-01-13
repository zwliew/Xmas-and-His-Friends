using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is attached to the pianpang prefab and
 * controls the spritePianpang that should be displayed
 * 
*/
public class PinZiPP : MonoBehaviour, IRecycle {

	private Sprite[] spritePianPang;

	[HideInInspector]
	public int spriteNo;

	private float lifetime = 2f;
	private bool dead;

	public static List<int> theList = new List<int> ();

	// Use this for initialization
	void Start () {		
		theList = PinZiGM.QuestionList();
	}

	public void Restart(){
		dead = false;
		lifetime = 2f;
		theList = PinZiGM.QuestionList();
		spriteNo = theList [Random.Range(0, theList.Count / 2)];
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = spritePianPang [7 - spriteNo];
	}
	


	// Update is called once per frame
	void Update () {
		//delete the pianpangs after some time
		if (lifetime > 0) {
			lifetime -= Time.deltaTime;
		} else {
			dead = true;
		}

		if (dead)
			delete();
	}

	void delete(){
		GameObjectUtility.customDestroy (gameObject);
	}

	public void Shutdown(){
		
	}
	public void SetSelected(){
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = spritePianPang[spriteNo];
	}
}
