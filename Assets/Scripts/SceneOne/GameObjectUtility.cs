using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is the fundation of the object pool system
 * Just leave it there
 */
public class GameObjectUtility {

	private static Dictionary<RecycleTouch, ObjectPoolTouch> touchesPool = new Dictionary<RecycleTouch, ObjectPoolTouch> ();

	public static GameObject customInstantiate(GameObject prefab, Vector3 position){

		GameObject instance = null;

		var recycleScript = prefab.GetComponent<RecycleTouch> ();
		if (recycleScript != null) {
			var pool = GetObjectPoolTouch (recycleScript);
			instance = pool.NextTouch (position).gameObject;
		} else {
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = position;
		}
		return instance;
	}

	public static void customDestroy(GameObject gameObject){
		var recycleTouch = gameObject.GetComponent<RecycleTouch> ();
		if (recycleTouch != null) {
			recycleTouch.Shutdown ();
		} else {
			GameObject.Destroy (gameObject);
		}
	}

	private static ObjectPoolTouch GetObjectPoolTouch(RecycleTouch reference){
		ObjectPoolTouch pool = null;

		if (touchesPool.ContainsKey (reference)) {
			pool = touchesPool [reference];
		} else {
			var poolContainer = new GameObject (reference.gameObject.name + "ObjectPoolTouch");
			pool = poolContainer.AddComponent<ObjectPoolTouch> ();
			pool.prefab = reference;
			touchesPool.Add (reference, pool);
		}
		return pool;
	}

}
