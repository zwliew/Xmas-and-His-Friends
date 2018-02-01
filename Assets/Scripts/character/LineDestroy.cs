using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject == null)
        {
            addScore();
        }
	}

    void addScore()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
    }
}
