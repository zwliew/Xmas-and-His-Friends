using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmasMovementScript : MonoBehaviour {
	
	public GameObject[] nodes;
	private Vector3[] v3nodes;//Navigation key points

	private UnityEngine.AI.NavMeshAgent navAgent;
	private List<Vector3> v3destinations = new List<Vector3> ();

	private RaycastHit hitInfo;
	private	Vector3 v3destination;
	private Vector3 v3location;
	private int standOn;
	private int destOn;

	private Movement1 movementScript;

	private CameraMovement camMovement;


	void Awake (){
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		v3location = transform.position;
		v3destination = v3location;
		v3destinations.Clear ();
		movementScript = GetComponent<Movement1> ();
		camMovement = Camera.main.GetComponent<CameraMovement> ();
		v3nodes = new Vector3[nodes.Length];
		for (int i = 0; i < nodes.Length; i++) {
			v3nodes [i] = nodes [i].transform.position;
			//Debug.DrawRay (v3nodes [i], new Vector3(0f, -10f, 0f), Color.red, 2f);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {

			v3location = transform.position;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//Debug.DrawLine (ray.origin, ray.origin + 300 * ray.direction, Color.red, 2f);
			if (Physics.Raycast (ray, out hitInfo, 300f, 1 << LayerMask.NameToLayer ("Road"))) {//Navigate to the destination
				v3destination = FineTuneDestination(hitInfo.point, hitInfo);
				destOn = hitInfo.collider.gameObject.GetComponent<RoadProperty>().regionNumber;//Determine the region destination is in
				GetComponent<AnimationController> ().holdFlag = false;
				GetComponent<AnimationController> ().holdFlagTwo = false;
				camMovement.followState = 0;

				if (hitInfo.collider.gameObject.GetComponent<FurnitureProperty> () != null) {
					if (hitInfo.collider.gameObject.GetComponent<FurnitureProperty> ().furnitureType.Equals ("Game")) {
						Debug.Log ("Goint to play game");
						camMovement.SetFurniture (hitInfo.collider.gameObject);
						GetComponent<AnimationController> ().holdFlag = true;
						v3destination = v3nodes[0];
					}
				}

				if(hitInfo.collider.gameObject.tag.Equals("RegionFour")){
					GetComponent<AnimationController> ().holdFlagTwo = true;
				}


				NavigateTo ();
			}
		}

		if (Input.GetMouseButtonDown(1)) {//For debug use only
			navAgent.SetDestination (v3nodes[1]);//Crossing
			GetComponent<AnimationController> ().holdFlag = false;
			GetComponent<AnimationController> ().holdFlagTwo = false;
			camMovement.followState = 0;
		}
	}

	private void NavigateTo(){

		v3destinations.Clear ();
		v3location = transform.position;

		RaycastHit hitInfoLocal;
		Ray ray = new Ray (v3location + new Vector3 (0f, 5f, 0f), new Vector3 (0f, -1f, 0f));
		Debug.DrawLine (ray.origin, ray.origin + 300 * ray.direction, Color.red, 2f);
		if (Physics.Raycast (ray, out hitInfoLocal, 80f, 1 << LayerMask.NameToLayer ("Road"))) {//Determine the region Xmas is in
			standOn = hitInfoLocal.collider.gameObject.GetComponent<RoadProperty>().regionNumber;
		}

		//Debug.Log (standOn); //0, 1, 2, 3,, 4, 5, 6, 7, 8, 9

		//Debug.Log (destOn);  //0, 1, 2, 3,, 4, 5, 6, 7, 8, 9
		if (standOn > destOn) {
			//Debug.Log ("for loop1");
			for (int j = standOn - 1; j >= destOn; j--) {
				v3destinations.Add (v3nodes [j]);
				//Debug.Log ("j = " + j + " " + v3nodes[j]);
			}
			//Debug.Log ("for loop1 end");
			v3destinations.Add (v3destination);
		} else {
			if (standOn < destOn) {
				//Debug.Log ("for loop2");
				for (int k = standOn; k < destOn; k++) {
					v3destinations.Add (v3nodes [k]);
					//Debug.Log ("k = " + k + " " + v3nodes[k]);
				}
				//Debug.Log ("for loop2 end");
				v3destinations.Add (v3destination);
			} else {
				v3destinations.Add (v3destination);
			}

			for (int haha = 0; haha < v3destinations.Count; haha++) {
				//Debug.Log (v3destinations [haha]);
			}

		}
		movementScript.SetDestinations (v3destinations);
		//Debug.Log ("XMS called Set des");

	}
	private Vector3 FineTuneDestination(Vector3 des, RaycastHit hitInfoo){
		Vector3 destination = des;
		if (hitInfoo.collider.gameObject.GetComponent<RoadProperty> () != null) {
			if (hitInfoo.collider.gameObject.GetComponent<RoadProperty> ().direction.Equals ("Horizontal")) {

				destination = new Vector3 (hitInfoo.collider.transform.position.x, des.y, des.z);
			}
			if (hitInfoo.collider.gameObject.GetComponent<RoadProperty> ().direction.Equals ("Vertical")) {

				destination = new Vector3 (des.x, des.y, hitInfo.collider.transform.position.z);
			}
		}
		return destination;
	}
}
