using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script enables the attached gameObject to disappear after some time
 * change the floatLifetime in the inspector window!
 */
public class DestoryGameObject : MonoBehaviour {

	public float lifetime;
	private float lifeTime;
	private bool dead;

	void Start(){
		lifeTime = lifetime;
	}
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
