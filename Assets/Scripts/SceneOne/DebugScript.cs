using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {
	private Vector3 v3TouchLocation;
	private Vector3 v3GroundLocation;
	private int i;
	private List<Vector3> v3list;
	private int length;
	private Rigidbody2D body2d;


	

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			
			body2d.MovePosition (new Vector2(body2d.position.x + 5, body2d.position.y));
         
		}
		if (Input.GetMouseButtonUp (0)) {
			
		}
	}
	void OnTriggerStay2D(Collider2D collider){
		if (collider.gameObject.tag == "Obstacles") {
			body2d.MovePosition (new Vector2 (body2d.position.x, body2d.position.y - 5));
		}
	}


}
