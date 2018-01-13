using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script enables the attached gameObject to disappear after some time
 * change the floatLifetime in the inspector window!
 */
public class DestroyTouchSpawner : MonoBehaviour {

	public float lifetime = 0.25f;
	private bool dead;


	// Update is called once per frame
	void Update () {
		if (lifetime > 0) {
			lifetime -= Time.deltaTime;
		} else {
			dead = true;
		}

		if (dead)
			deleteTouch();
	}
	void deleteTouch(){
		dead = false;
		lifetime = 0.25f;
		GameObjectUtility.customDestroy(gameObject);
	}
}
