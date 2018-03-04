using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script enables the player to enter an editor mode where he can shift the furnitures;
 * 
 * There are two types of furnitures{
 *   1. Furnitures that are to be places at fixed locations;
 *   2. Furnitures that can be placed anywhere on the floor;
 * }
 * 
 * Should there be an inventory(UI)?
 * 
 */

public class EditModeForFurnitures : MonoBehaviour {
	
	[HideInInspector]
	public bool EditorMode;

	public GameObject[] slotAModels;
	public GameObject[] slotBModels;
	public GameObject[] slotCModels;

	private GameObject selectedGameObject;


	// Use this for initialization
	void Start () {
		EditorMode = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!EditorMode) {
			return;
		}

		if (Input.GetMouseButtonDown (0)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

			if (selectedGameObject == null) {
				if (Physics.Raycast (ray, out hitInfo, 300f, 1 << LayerMask.NameToLayer ("Furnitures"))) {
				
					if (hitInfo.collider.gameObject.CompareTag ("Stationary")) {

						//TODO Enter editor mode for stationary furniture
						//TODO Pop up a UI or something

					} else if (hitInfo.collider.gameObject.CompareTag ("Moveable")) {

						//TODO Pick up the furniture and enable placing down


					} else {
						Debug.LogError ("Raycast to determine Furniture failed.");
					}

					if (Physics.Raycast (ray, out hitInfo, 300f, 1 << LayerMask.NameToLayer ("Floor"))) {
						//TODO do nothing or combine the movement script here
					}
				} 
			} else {

				//TODO 

			}
		}
	}
}
