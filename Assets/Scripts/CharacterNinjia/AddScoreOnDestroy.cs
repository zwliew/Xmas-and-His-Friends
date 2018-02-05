using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnDestroy : MonoBehaviour, IRecycle {

	private SpriteRenderer renderer2D;

	//[HideInInspector]
	//public Sprite sprite;

	public void Restart(){
		this.gameObject.tag = "Untagged";
		renderer2D = GetComponent<SpriteRenderer> ();
		renderer2D.sprite = CharacterSpawner.GetRandomSprite ();
	}

	void Update () {
        if(this.gameObject.transform.position.y < -5.85)
        {
			GameObjectUtility.customDestroy(this.gameObject);
        }
    }
		
    void addScore()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
    }

	public void Shutdown(){
		if (this.gameObject.tag == "destroyed")
		{
			addScore();
			SplitIntoTwo (renderer2D.sprite.name);
		}
	}

	private void SplitIntoTwo(string spriteName){
		Debug.Log (spriteName);
		Sprite[] temp = CharacterSpawner.GetSprSides (spriteName);
	}

}
