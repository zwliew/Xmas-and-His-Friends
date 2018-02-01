using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnDestroy : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.transform.position.y < -5.85)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (this.gameObject.tag == "destroyed")
        {
            addScore();
        }
    }
    void addScore()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
    }
}
