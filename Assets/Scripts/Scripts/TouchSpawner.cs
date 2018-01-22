using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script generates a feedback animation for touch inputs
 */
public class TouchSpawner : MonoBehaviour {

	public GameObject objTouch;
	private Vector3 v3TouchLocation;
	private Vector3 v3GroundLocation;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			v3TouchLocation = Input.mousePosition;
			v3GroundLocation = Camera.main.ScreenToWorldPoint (new Vector3(v3TouchLocation.x, v3TouchLocation.y, 10f));

			GameObjectUtility.customInstantiate (objTouch, new Vector3(v3GroundLocation.x, v3GroundLocation.y, -3f));
			
			//MovementXmasRB2D.SetDestination (MovementXmas.GetLocation (), new Vector3 (v3GroundLocation.x, v3GroundLocation.y, 0f));

			Ray ray = Camera.main.ScreenPointToRay (v3TouchLocation);//从摄像机发出到点击坐标的射线
			RaycastHit hitInfo;
			/*
			if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Objects"))) {
				MovementXmasRB2D.SetDestination (MovementXmasRB2D.GetLocation (), 
					new Vector3 (
					hitInfo.collider.gameObject.transform.position.x, 
						hitInfo.collider.gameObject.transform.position.y + hitInfo.collider.gameObject.transform.localScale.y/2 + 100,
					0f));
				

			}
			*/



		}

	}
}
