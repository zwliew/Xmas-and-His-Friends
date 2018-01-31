using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 *This script is not in use anymore
 */
/*
public class MoveMent : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent navAgent;
	private List<Vector3> v3destinations = new List<Vector3> ();

	private RaycastHit hitInfo;
	private	Vector3 v3destination;
	private Vector3 v3location;
	private string standOn;
	private string destOn;

	private Movement1 MovementScript;

	private CameraMovement camMovement;

	private Vector3 node1 = new Vector3 (-8.29f, 47.64f, -32.25f);//Between 1 & 2
	private Vector3 node2 = new Vector3 (25.4f, 47.6f, -32.5f);//Between 1 & 3
	private Vector3 node3 = new Vector3 (24.2f, 49.48f, 0.23f);//Between 3 & 4
	private Vector3 node4 = new Vector3 (24.2f, 56.57f, 7.56f);//Between 4 & 3

	private Vector3[] nodes = new Vector3[]{};

	public GameObject[] node = new GameObject[9];

	void Start (){
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		v3location = transform.position;
		v3destination = v3location;
		v3destinations.Clear ();
		MovementScript = GetComponent<Movement1> ();
		camMovement = Camera.main.GetComponent<CameraMovement> ();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			
			v3location = transform.position;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Road"))) {//Navigate to the destination
				v3destination = FineTuneDestination(hitInfo.point);
				destOn = hitInfo.collider.gameObject.tag;//Determine the region destination is in
				GetComponent<AnimationController> ().holdFlag = false;
				GetComponent<AnimationController> ().holdFlagTwo = false;
				camMovement.followState = 0;

				if (hitInfo.collider.gameObject.GetComponent<FurnitureProperty> () != null) {
					if (hitInfo.collider.gameObject.GetComponent<FurnitureProperty> ().furnitureType.Equals ("Game")) {
						Debug.Log ("It is a game");
						camMovement.SetFurniture (hitInfo.collider.gameObject);
						GetComponent<AnimationController> ().holdFlag = true;
						v3destination = new Vector3 (-7.3f, 65.4f, -12.7f);
					}
				}

				if(hitInfo.collider.gameObject.tag.Equals("RegionFour")){
					GetComponent<AnimationController> ().holdFlagTwo = true;
				}


				NavigateTo ();
			}
		}

		if (Input.GetMouseButtonDown(1)) {//For debug use only
			navAgent.SetDestination (node1);//Crossing
			GetComponent<AnimationController> ().holdFlag = false;
			GetComponent<AnimationController> ().holdFlagTwo = false;
			camMovement.followState = 0;
		}
	}

	private void NavigateTo(){

		v3destinations.Clear ();

		Ray ray = new Ray (v3location, v3location + new Vector3 (0f, -99f, 0f));
		if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Road"))) {//Determine the region Xmas is in
			standOn = hitInfo.collider.gameObject.tag;
		}


		switch (standOn) {//Hard coding for the destinations
		case "RegionOne":
			switch (destOn) {
			case "RegionOne":
				v3destinations.Add (v3destination);
				break;
			case "RegionTwo":
				v3destinations.Add (node1);
				v3destinations.Add (v3destination);
				break;
			case "RegionThree":
				v3destinations.Add (node2);
				v3destinations.Add (v3destination);
				break;
			case "RegionFour":
				v3destinations.Add (node2);
				v3destinations.Add (node3);
				v3destinations.Add (v3destination);
				break;
			}
			break;

		case "RegionTwo":
			switch (destOn) {
			case "RegionOne":
				v3destinations.Add (node1);
				v3destinations.Add (v3destination);
				break;
			case "RegionTwo":
				v3destinations.Add (v3destination);
				break;
			case "RegionThree":
				v3destinations.Add (node1);
				v3destinations.Add (node2);
				v3destinations.Add (v3destination);
				break;
			case "RegionFour":
				v3destinations.Add (node1);
				v3destinations.Add (node2);
				v3destinations.Add (node3);
				v3destinations.Add (v3destination);
				break;
			}
			break;

		case "RegionThree":
			switch (destOn) {
			case "RegionOne":
				v3destinations.Add (node2);
				v3destinations.Add (v3destination);
				break;
			case "RegionTwo":
				v3destinations.Add (node2);
				v3destinations.Add (node1);
				v3destinations.Add (v3destination);
				break;
			case "RegionThree":
				v3destinations.Add (v3destination);
				break;
			case "RegionFour":
				v3destinations.Add (node3);
				v3destinations.Add (v3destination);
				break;
			}
			break;

		case "RegionFour":
			switch (destOn) {
			case "RegionOne":
				v3destinations.Add (node4);
				v3destinations.Add (node2);
				v3destinations.Add (v3destination);
				break;
			case "RegionTwo":
				v3destinations.Add (node4);
				v3destinations.Add (node2);
				v3destinations.Add (node1);
				v3destinations.Add (v3destination);
				break;
			case "RegionThree":
				v3destinations.Add (node4);
				v3destinations.Add (v3destination);
				break;
			case "RegionFour":
				v3destinations.Add (v3destination);
				break;
			}
			break;
		}
		MovementScript.StopAllCoroutines ();
		MovementScript.SetDestinations (v3destinations);

		
	}
	private Vector3 FineTuneDestination(Vector3 des){
		Vector3 destination = des;
		if (hitInfo.collider.gameObject.GetComponent<RoadProperty> () != null) {
			if (hitInfo.collider.gameObject.GetComponent<RoadProperty> ().direction.Equals ("Horizontal")) {
			
				destination = new Vector3 (des.x, des.y, hitInfo.collider.transform.position.z);
			}
			if (hitInfo.collider.gameObject.GetComponent<RoadProperty> ().direction.Equals ("Vertical")) {
			
				destination = new Vector3 (hitInfo.collider.transform.position.x, des.y, des.z);
			}
		}
		return destination;
	}
}
*/