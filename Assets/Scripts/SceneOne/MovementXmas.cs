using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is superseded by MovementXmasRB2D and is no longer used

public class MovementXmas : MonoBehaviour {

	private static Vector3 v3location;
	private static Vector3 v3destination;
	private int state;
	private int frameCounter;
	private bool XY;
	private static bool XYActive;

	public int width = 800;
	public int height = 1000;

	public static void SetXYActive(){
		XYActive = true;
	}

	public static void SetDestination(Vector3 location, Vector3 des){
		v3location = location;
		v3destination = des;
	}

	public static Vector3 GetLocation(){
		Vector3 loc = v3location;
		return loc;
	}

	public static Vector3 GetDestination(){
		Vector3 des = v3destination;
		return des;
	}

	private Vector3 GetRealDestination(){
		Vector3 des = GetDestination ();
		return des;
	}

	// ------------------------------Use this for initialization-----------------------------
	void Start () {
		v3location = transform.position;
		v3destination = transform.position;
		frameCounter = 0;
		XY = false;
		XYActive = true;
	}
	
	// -----------------------------Update is called once per frame---------------------------
	void FixedUpdate () {

		if (frameCounter > 1000) {
			frameCounter = 0;
		} else {
			frameCounter += 1;
		}
			
		if(near(v3location, v3destination)){
			state = 0;
		}else{
			state = 1;
			if (frameCounter % 30 != 0) {
				MoveToDestination (v3destination);
			} else {
				ChangeXY();
			}
		}
			
		AnimationManager.SetState (state);

		v3location = transform.position;
	}

	//---------------------------------void-----------------------------------------------------
	void MoveToDestination(Vector3 des){
		int speed = 5;

		if (XY) {
			if (Mathf.Abs (transform.position.x - des.x) > 5f) {
				if (transform.position.x > des.x) {
					transform.SetPositionAndRotation (new Vector3 (transform.position.x - speed, transform.position.y, -3f), Quaternion.identity);
				} else {
					transform.SetPositionAndRotation (new Vector3 (transform.position.x + speed, transform.position.y, -3f), Quaternion.identity);
				}
			} else {
				XYActive = false;
				XY = false;
			}
		} else {
			if (Mathf.Abs (transform.position.y - des.y) > 5f) {
				if (transform.position.y > des.y) {
					transform.SetPositionAndRotation (new Vector3 (transform.position.x, transform.position.y - speed, -3f), Quaternion.identity);
				} else {
					transform.SetPositionAndRotation (new Vector3 (transform.position.x, transform.position.y + speed, -3f), Quaternion.identity);
				}
			} else {
				XYActive = false;
				XY = true;
			}
		}

	}

	void ChangeXY(){
		if(XYActive){
			float ran = Random.value;
			if(ran > 0.5){
				XY = true;
			}else{
				XY = false;
			}
		}
	}

	bool near(Vector3 loc,Vector3 des){
		bool nearma;
		if(Mathf.Abs((loc.x - des.x)) < 5f & Mathf.Abs((loc.y - des.y)) < 5f){
			nearma = true;
		}else{
			nearma = false;
		}
		return nearma;
	}
	
}
