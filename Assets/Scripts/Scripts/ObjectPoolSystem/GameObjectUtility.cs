using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is the fundation of the object pool system
 * Just leave it there
 */
public class GameObjectUtility {

	private static Dictionary<RecycledGameObject, ObjectPool> GameObjectsPool = new Dictionary<RecycledGameObject, ObjectPool> ();

	public static GameObject customInstantiate(GameObject prefab, Vector3 position){

		GameObject instance = null;

		var recycleScript = prefab.GetComponent<RecycledGameObject> ();
		if (recycleScript != null) {
			var pool = GetObjectPool (recycleScript);
			instance = pool.NextTouch (position).gameObject;
		} else {
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = position;
		}
		return instance;
	}

	public static void customDestroy(GameObject gameObject){
		var RecycledGameObject = gameObject.GetComponent<RecycledGameObject> ();
		if (RecycledGameObject != null) {
			RecycledGameObject.Shutdown ();
		} else {
			GameObject.Destroy (gameObject);
		}
	}

	private static ObjectPool GetObjectPool(RecycledGameObject reference){
		ObjectPool pool = null;

		if (GameObjectsPool.ContainsKey (reference)) {
			pool = GameObjectsPool [reference];
		} else {
			var poolContainer = new GameObject (reference.gameObject.name + "ObjectPool");
			pool = poolContainer.AddComponent<ObjectPool> ();
			pool.prefab = reference;
			GameObjectsPool.Add (reference, pool);
		}
		return pool;
	}

}
