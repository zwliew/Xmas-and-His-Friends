using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnDestroy : MonoBehaviour, IRecycle {


	public void Restart(){
		this.gameObject.tag = "Untagged";
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
		}
	}

}
