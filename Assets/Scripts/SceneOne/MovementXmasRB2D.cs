using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//2d

public class MovementXmasRB2D : MonoBehaviour {
	private static Vector3 v3location;
	private static Vector3 v3destination;


	private int animationState;
	private int frameCounter;
	private bool XY;//true-x direction false-y direction
	private static bool XYActive;
	private static bool obstacleNavi;
	private int movingState;//0default 1right 2up 3left 4down

	private Rigidbody2D xmasRBody2d;

	private int speed = 160;

	// Use this for initialization
	void Awake(){
		xmasRBody2d = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		v3location = transform.position;
		v3destination = transform.position;
		frameCounter = 0;
		XY = false;
		XYActive = true;
	}

	void FixedUpdate () {

		if (frameCounter > 1000) {
			frameCounter = 0;
		} else {
			frameCounter += 1;
		}

		if(near(v3location, v3destination)){
			animationState = 0;
		}else{
			if (frameCounter % 30 != 0) {
				MoveToDestination (v3destination);
			} else {
				ChangeXY();
			}
		}

		AnimationManager.SetState (animationState);

		v3location = transform.position;
	}

	//---------------------------------void-----------------------------------------------------
	void MoveToDestination(Vector3 des){
		
		switch (movingState) {
		case 0:
			Debug.Log ("default");
			if (XY) {
				if (Mathf.Abs (transform.position.x - des.x) > 5f) {
					if (transform.position.x > des.x) {
						//left
						xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x - speed * Time.fixedDeltaTime, xmasRBody2d.position.y));
						animationState = 1;
					} else {
						//right
						xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x + speed * Time.fixedDeltaTime, xmasRBody2d.position.y));
						animationState = 1;
					}
				} else {
					XYActive = false;
					XY = false;
				}
			} else {
				if (Mathf.Abs (transform.position.y - des.y) > 5f) {
					if (transform.position.y > des.y) {
						//down
						xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x, xmasRBody2d.position.y - speed * Time.fixedDeltaTime));
						animationState = 1;
					} else {
						//up
						xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x, xmasRBody2d.position.y + speed * Time.fixedDeltaTime));
						animationState = 1;
					}
				} else {
					XYActive = false;
					XY = true;
				}
			}
			break;
		case 1:
			Debug.Log ("right");
			xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x + speed * Time.fixedDeltaTime, xmasRBody2d.position.y));
			animationState = 1;
			break;
		case 2:
			Debug.Log ("up");
			xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x, xmasRBody2d.position.y + speed * Time.fixedDeltaTime));
			animationState = 1;
			break;
		case 3:
			Debug.Log ("left");
			xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x - speed * Time.fixedDeltaTime, xmasRBody2d.position.y));
			animationState = 1;
			break;
		case 4:
			Debug.Log ("down");
			xmasRBody2d.MovePosition(new Vector2(xmasRBody2d.position.x, xmasRBody2d.position.y - speed * Time.fixedDeltaTime));
			animationState = 1;
			break;
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
	//---------------------------------------------------------------------------------------------------

	public static void SetDestination(Vector3 location, Vector3 des){
		v3location = location;
		v3destination = des;
		XYActive = true;
		obstacleNavi = true;
	}

	public static Vector3 GetLocation(){
		Vector3 loc = v3location;
		return loc;
	}

	public static Vector3 GetDestination(){
		Vector3 des = v3destination;
		return des;
	}
	//----------------------------Obstacles-----------------------------------------
	void OnTriggerStay2D(Collider2D collider){
		if (obstacleNavi && collider.gameObject.tag == "Obstacles3") {
			
			if ((transform.position.x - v3destination.x) < 0 && (transform.position.y - v3destination.y) > 0) {//right bottom
				movingState = 4;
				//Debug.Log ("set down");
			}
			if ((transform.position.x - v3destination.x) < 0 && (transform.position.y - v3destination.y) < 0) {//right top
				movingState = 2;
				//Debug.Log ("set up");
			}

			if ((transform.position.x - v3destination.x) > 0) {//left
				movingState = 0;
				//Debug.Log ("set default");
			}


		}
		if (obstacleNavi && collider.gameObject.tag == "Obstacles1") {

			if ((transform.position.x - v3destination.x) > 0 && (transform.position.y - v3destination.y) > 0) {//left bottom
				movingState = 4;
				//Debug.Log ("set down");
			}
			if ((transform.position.x - v3destination.x) > 0 && (transform.position.y - v3destination.y) < 0) {//left top
				movingState = 2;
				//Debug.Log ("set up");
			}

			if ((transform.position.x - v3destination.x) < 0) {//right
				movingState = 0;
				//Debug.Log ("set default");
			}


		}
		if (obstacleNavi && collider.gameObject.tag == "Obstacles2") {

			if ((transform.position.x - v3destination.x) < 0 && (transform.position.y - v3destination.y) > 0) {//right bottom
				movingState = 1;
				//Debug.Log ("set right");
			}
			if ((transform.position.x - v3destination.x) > 0 && (transform.position.y - v3destination.y) > 0) {//left bottom
				movingState = 3;
				//Debug.Log ("set left");
			}

			if ((transform.position.y - v3destination.y) < 0) {//top
				movingState = 0;
				//Debug.Log ("set default");
			}


		}
		if (obstacleNavi && collider.gameObject.tag == "Obstacles4") {

			if ((transform.position.x - v3destination.x) > 0 && (transform.position.y - v3destination.y) < 0) {//left top
				movingState = 3;
				//Debug.Log ("set left");
			}
			if ((transform.position.x - v3destination.x) < 0 && (transform.position.y - v3destination.y) < 0) {//right top
				movingState = 1;
				//Debug.Log ("set right");
			}

			if ((transform.position.y - v3destination.y) > 0) {//bottom
				movingState = 0;
				//Debug.Log ("set default");
			}


		}
		obstacleNavi = false;
	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.tag == "Obstacles1"||collider.gameObject.tag == "Obstacles2"
			||collider.gameObject.tag == "Obstacles3"||collider.gameObject.tag == "Obstacles4") {
			movingState = 0;
			Debug.Log ("set default");
			XYActive = true;
			obstacleNavi = true;

			if (collider.gameObject.tag == "Obstacles1" ) {
				xmasRBody2d.MovePosition (new Vector2 (xmasRBody2d.position.x + speed * 3 * Time.fixedDeltaTime, xmasRBody2d.position.y));
				animationState = 1;
			}
			if (collider.gameObject.tag == "Obstacles2" ) {
				xmasRBody2d.MovePosition (new Vector2 (xmasRBody2d.position.x, xmasRBody2d.position.y - speed * 3 * Time.fixedDeltaTime));
				animationState = 1;
			}
			if (collider.gameObject.tag == "Obstacles3" ) {
				xmasRBody2d.MovePosition (new Vector2 (xmasRBody2d.position.x - speed * 3 * Time.fixedDeltaTime, xmasRBody2d.position.y));
				animationState = 1;
			}
			if (collider.gameObject.tag == "Obstacles4" ) {
				xmasRBody2d.MovePosition (new Vector2 (xmasRBody2d.position.x, xmasRBody2d.position.y + speed * 3 * Time.fixedDeltaTime));
				animationState = 1;
			}


		}
	}
}

