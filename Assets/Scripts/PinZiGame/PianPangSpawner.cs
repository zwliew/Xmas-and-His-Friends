using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script generates pianpang prefabs on the screen
 * 
*/
public class PianPangSpawner : MonoBehaviour {


	public GameObject prefabPianPang;
	private Vector3 [] pos = new Vector3[5];

	// Use this for initialization
	void Start () {
		GenerateQn ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {

			GenerateQn ();
		}
	}

	void GenerateQn(){
		PinZiGM.SetChooseZero ();
		for(int i = 0; i < 4; i++){
			pos[i] = new Vector3 (-3 + i * 2, 1, -3);
		}


		for (int i = 0; i < 4; i++) {
			GameObjectUtility.customInstantiate (prefabPianPang, pos [i]);
		}
	}

}
