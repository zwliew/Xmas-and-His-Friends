using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public Touch touch;
    public KeyCode key;
    bool active = false;
    GameObject note;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key)&&active)//Input.GetTouch(0).position.x>-6&& Input.GetTouch(0).position.x<-3&&Input.GetTouch(0).position.y>-4&& Input.GetTouch(0).position.x < -2 && active) //detect area touched
        {
            Destroy(note);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "note")
        {
            active = true;
            note = col.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }
}

