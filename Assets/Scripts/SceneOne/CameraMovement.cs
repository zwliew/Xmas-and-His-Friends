using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script enables a followed camera
 */
public class CameraMovement : MonoBehaviour {

	private Vector3 v3cameraLocation;
	private Vector2 v2cameraLocation;
	private Vector3 v3des;
	private Vector2 v2des;
	private Vector2 v2smoothVelocity;
	private Vector2 v2damped;

	public int rightMost;
	public int leftMost;
	public int upMost;
	public int downMost;


	void Start(){
		v3cameraLocation = transform.position;
		v2smoothVelocity = new Vector2(0f, 0f);
	}
	void LateUpdate(){
		v3cameraLocation = transform.position;
		v3des = MovementXmasRB2D.GetLocation ();
		if (v3des.x > rightMost) {
			v3des.x = rightMost;
		}
		if (v3des.x < leftMost) {
			v3des.x = leftMost;
		}
		if (v3des.y > upMost) {
			v3des.y = upMost;
		}
		if (v3des.y < downMost) {
			v3des.y = downMost;
		}


		v2des = new Vector2 (v3des.x, v3des.y);
		v2cameraLocation = new Vector2 (v3cameraLocation.x, v3cameraLocation.y);
		v2damped = Vector2.SmoothDamp (v2cameraLocation, v2des, ref v2smoothVelocity, 0.5f, 1000f, Time.deltaTime);
		transform.position = new Vector3 (v2damped.x, v2damped.y, -10f);
	}

}

