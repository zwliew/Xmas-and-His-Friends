using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinZiMain : MonoBehaviour, IRecycle {

	public Sprite[] spritePianPang;

	public float lifetime = 0.25f;
	private bool dead;

	private bool haha;

	public static Dictionary<Vector2Int, string> theDic = new Dictionary<Vector2Int, string>();
	static List<int> theList = new List<int> ();
	static Vector2Int v2Ans;

	public void Restart(){
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = spritePianPang [Random.Range (0, spritePianPang.Length)];
	
	}

	public void Shutdown(){
	
	}

	// Use this for initialization
	void Start () {
		theDic = PinZiData.AnswerDictionary();
		theList = PinZiData.QuestionList();
	}



	// Update is called once per frame
	void Update () {
		if (lifetime > 0) {
			lifetime -= Time.deltaTime;
		} else {
			dead = true;
		}

		if (dead)
			delete();
	}
	void delete(){
		dead = false;
		lifetime = 0.25f;
		GameObjectUtility.customDestroy (gameObject);
	}
}
