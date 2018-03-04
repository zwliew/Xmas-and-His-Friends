using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnDestroy : MonoBehaviour, IRecycle {

	private SpriteRenderer renderer2D;

	public GameObject prefabPartOne;
	public GameObject prefabPartTwo;

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
		Sprite[] temp = null;
		temp = CharacterSpawner.GetSprSides (spriteName);
		if (temp != null) {
			Debug.Log (temp[0].name);
			Debug.Log (temp[1].name);
		} else {
			Debug.Log ("Didnot get sprite splits");
		}
		GameObject partOne = GameObjectUtility.customInstantiate (prefabPartOne, transform.position + new Vector3(0.8f, 0f, 0f));
		partOne.GetComponent<SpriteRenderer> ().sprite = temp [0];
		GameObject partTwo = GameObjectUtility.customInstantiate (prefabPartTwo, transform.position+ new Vector3(-0.8f, 0f, 0f));
		partTwo.GetComponent<SpriteRenderer> ().sprite = temp [1];
	}

}
