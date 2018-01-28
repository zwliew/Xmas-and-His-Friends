using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	float xOffset;
	float yOffset;
	float zOffset;

	GameObject target;

	[HideInInspector]
	public int followState;

	GameObject furniture = null;

	Camera cam;
	Quaternion camRotation;

	Vector3 smoothV = Vector3.zero;
	// Use this for initialization
	void Awake () {	
		followState = 999;
		target = GameObject.FindGameObjectsWithTag ("Player") [0];
	    cam = GetComponent<Camera> ();
		xOffset = transform.position.x - target.gameObject.transform.position.x;
		yOffset = transform.position.y - target.gameObject.transform.position.y;
		zOffset = transform.position.z - target.gameObject.transform.position.z;
		camRotation = cam.transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		switch (followState) {
		case 0:
			transform.position = Vector3.SmoothDamp (
				transform.position, 
				new Vector3 (-117f, 211f, 125f),
				ref smoothV,
				1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, camRotation, 0.2f);
			cam.orthographicSize = Vector2.Lerp (new Vector2 (cam.orthographicSize, cam.orthographicSize), new Vector2 (72f, 72f), 0.2f).x;
			break;

		case 1:
			SpotlightCamera ();
			break;
		case 2://Start Animation
			transform.position = Vector3.SmoothDamp (
				transform.position, 
				new Vector3 (-117f, 211f, 125f),
				ref smoothV,
				2f);
			transform.rotation = Quaternion.Lerp (transform.rotation, camRotation, 0.2f);
			cam.orthographicSize = Vector2.Lerp (new Vector2 (cam.orthographicSize, cam.orthographicSize), new Vector2 (72f, 72f), 0.2f).x;
			break;
		}
	}

	public void SetFurniture(GameObject fur){
		furniture = fur;
	}

	private void NormalFollowCamera(){//Rejected and Deleted
		transform.position = Vector3.SmoothDamp (
			transform.position, 
			new Vector3 (
				target.gameObject.transform.position.x + xOffset, 
				target.gameObject.transform.position.y + yOffset, 
				target.gameObject.transform.position.z + zOffset), 
			ref smoothV, 
			0.1f);
	}

	private void SpotlightCamera(){
		Vector2 smoothV2 = Vector2.zero;

		transform.position = Vector3.SmoothDamp(
			transform.position, 
			furniture.transform.position + new Vector3(-300f, 0f, 0f),
			ref smoothV,
			0.1f);
		
		cam.orthographicSize = Vector2.Lerp (new Vector2 (cam.orthographicSize, cam.orthographicSize), new Vector2 (11f, 11f), 0.4f).x;
		transform.LookAt (furniture.transform.position);
	}

	public void LoadMovement(){
		followState = 2;
	}

}
