using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script enables the attached gameObject to disappear after some time
 * change the floatLifetime in the inspector window!
 */
public class DestroyTouchSpawner : MonoBehaviour {

	private float lifetime = 2f;
	private float lifeTime = 2f;
	private bool dead;


	// Update is called once per frame
	void Update () {
		if (lifeTime > 0) {
			lifeTime -= Time.deltaTime;
		} else {
			lifeTime = lifetime;
			GameObjectUtility.customDestroy(gameObject);
		}
	}
}
