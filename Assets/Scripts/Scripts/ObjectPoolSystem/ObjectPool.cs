using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script I also dont know for what but is important
 */
public class ObjectPool : MonoBehaviour {

	public RecycledGameObject prefab;
	private List<RecycledGameObject> poolGameObjects = new List<RecycledGameObject>();

	private RecycledGameObject CreateTouch(Vector3 position){
		var cloneGameObject = GameObject.Instantiate (prefab);
		cloneGameObject.transform.position = position;
		cloneGameObject.transform.parent = transform;

		poolGameObjects.Add (cloneGameObject);

		return cloneGameObject;
	}

	public RecycledGameObject NextTouch(Vector3 position){
		RecycledGameObject instance = null;

		foreach (var gO in poolGameObjects) {
			if (gO.gameObject.activeSelf != true) {
				instance = gO;
				instance.transform.position = position;
			}
		}
		if (instance == null) {
			instance = CreateTouch (position);
		}
		instance.Restart ();

		return instance;
	}

}
