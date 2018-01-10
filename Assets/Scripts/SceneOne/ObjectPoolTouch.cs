using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTouch : MonoBehaviour {

	public RecycleTouch prefab;
	private List<RecycleTouch> poolTouches = new List<RecycleTouch>();

	private RecycleTouch CreateTouch(Vector3 position){
		var cloneTouch = GameObject.Instantiate (prefab);
		cloneTouch.transform.position = position;
		cloneTouch.transform.parent = transform;

		poolTouches.Add (cloneTouch);

		return cloneTouch;
	}

	public RecycleTouch NextTouch(Vector3 position){
		RecycleTouch instance = null;

		foreach (var gO in poolTouches) {
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
