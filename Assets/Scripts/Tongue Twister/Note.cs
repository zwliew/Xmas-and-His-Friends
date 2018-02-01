using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
    Rigidbody2D rb;
    public float speed;
    Vector2 centre = new Vector2(0,0);
    // Use this for initialization

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start () {
        rb.velocity = new Vector2(0, -speed);
	}
	
	// Update is called once per frame
	void Update () {
		/*if (rb.GetRelativePoint (centre).y < -6) {
			Destroy (gameObject);//Try to use the object pool's GameObjectUtility.CustomInstantiate and .CustomDestroy to destroy the gameObject;
        }*/
	}
}
