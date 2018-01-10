using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNavigation : MonoBehaviour {

	private static Vector3 v3destination;
	private static Vector3 v3startLocation;
	private static Vector3 v3nextNode;

	private static List<Vector3> v3nodes = new List<Vector3>();

	private static bool XYstate;
	private static int intdirection;

	public static void SetDestination(Vector3 location, Vector3 des){
		v3startLocation = location;
		v3destination = des;
	}

	public static Vector3 GetLocation(){
		Vector3 loc = v3startLocation;
		return loc;
	}

	public static Vector3 GetDestination(){
		Vector3 des = v3destination;
		return des;
	}


	public static List<Vector3> GetPath(){
		return v3nodes;
	}


	public static void GeneratePath(){
		int i = 1;
		int XYactive = 0;

		//v3startLocation = new Vector3 (-46, -3, -3);
		//v3destination = new Vector3 (565, 114, -3);

		v3nodes.Clear();
		v3nodes.Add(v3startLocation);
		Debug.Log (v3startLocation);



		while (true) {
			
			bool boolwall = true;

			//generate the next node
			if (UpOrDown ()) {
				if (LeftOrRight ()) {
					if (ChangeXY (XYactive)) {
						v3nextNode = new Vector3 (v3nodes [i-1].x - 9, v3nodes [i-1].y, v3nodes [i-1].z);
					} else {
						v3nextNode = new Vector3 (v3nodes [i-1].x, v3nodes [i-1].y + 9, v3nodes [i-1].z);
					}
				} else {
					if (ChangeXY (XYactive)) {
						v3nextNode = new Vector3 (v3nodes [i-1].x + 9, v3nodes [i-1].y, v3nodes [i-1].z);
					} else {
						v3nextNode = new Vector3 (v3nodes [i-1].x, v3nodes [i-1].y + 9, v3nodes [i-1].z);
					}
				}
			} else {
				if (LeftOrRight ()) {
					if (ChangeXY (XYactive)) {
						v3nextNode = new Vector3 (v3nodes [i-1].x - 9, v3nodes [i-1].y, v3nodes [i-1].z);
					} else {
						v3nextNode = new Vector3 (v3nodes [i-1].x, v3nodes [i-1].y - 9, v3nodes [i-1].z);
					}
				} else {
					if (ChangeXY (XYactive)) {
						v3nextNode = new Vector3 (v3nodes [i-1].x + 9, v3nodes [i-1].y, v3nodes [i-1].z);
					} else {
						v3nextNode = new Vector3 (v3nodes [i-1].x, v3nodes [i-1].y - 9, v3nodes [i-1].z);
					}
				}
			}
            


			v3nodes.Add(v3nextNode);

			//check if arrives
			if (Mathf.Abs (v3nodes [i].x - v3destination.x) < 5f) {
				XYactive = 1; //go y direction
				if (Mathf.Abs (v3nodes [i].y - v3destination.y) < 5f) {
					Debug.Log ("Path found");
					break;
				}
			}
			if (Mathf.Abs (v3nodes [i].y - v3destination.y) < 5f) {
				XYactive = 2; //go x direction
				if (Mathf.Abs (v3nodes [i].x - v3destination.x) < 5f) {
					Debug.Log ("Path found");
					break;
				}
			}

			//check if go into obstacles
			RaycastHit2D[] rays = new RaycastHit2D[1];
			int hitno = 0;
			Vector2 v2start = new Vector2 (v3nodes[i-1].x, v3nodes[i-1].y);
			Vector2 v2end = new Vector2 (v3nodes[i].x, v3nodes[i].y);
			hitno = Physics2D.RaycastNonAlloc (v2start, v2end, rays, 1 << LayerMask.NameToLayer ("Objects"));
			if (hitno > 0) {
				
				//where the obstacle is
				Debug.Log("--Find an obstacle--" + " ,hit " + rays[0].collider.gameObject.name);
				Debug.DrawLine (v3nodes [i - 1], v3nodes [i], Color.red, 2f);

				if (Mathf.Abs (v3nodes [i - 1].x - v3nodes [i].x) < 0.5f) {
					if (v3nodes [i - 1].y - v3nodes [i].y < 0) {
						intdirection = 2;//up
					} else {
						intdirection = 4;//down
					}
				} else {
					if (v3nodes [i - 1].x - v3nodes [i].x < 0) {					
						intdirection = 1;//right
					} else {
						intdirection = 3;//left
					}
				}

				v3nodes.RemoveAt (i);

				//go until passing the obstacle
			 	int j = 0; //counter to prevent infinite loop
				while (j < 100) {
					//go forward one unit
					Debug.Log("Trying hard to get over it");
					switch (intdirection) {
					case 1:
						v3nodes.Add (new Vector3 (v3nodes [i - 1].x, v3nodes [i - 1].y + 9, v3nodes [i - 1].z));
						break;
					case 2:							
						v3nodes.Add (new Vector3 (v3nodes [i - 1].x - 9, v3nodes [i - 1].y, v3nodes [i - 1].z));
						break;
					case 3:
						v3nodes.Add (new Vector3 (v3nodes [i - 1].x, v3nodes [i - 1].y - 9, v3nodes [i - 1].z));
						break;
					case 4:
						v3nodes.Add (new Vector3 (v3nodes [i - 1].x + 9, v3nodes [i - 1].y, v3nodes [i - 1].z));
						break;
					}
						

					//check if obstacle is still on the right
					switch (intdirection) {
					case 1:
				    	v2start = new Vector2 (v3nodes[i-1].x, v3nodes[i-1].y);		
						v2end = new Vector2 (v3nodes[i-1].x + 9, v3nodes[i-1].y);
						hitno = Physics2D.RaycastNonAlloc (v2start, v2end,rays, 1 << LayerMask.NameToLayer ("Objects"));
						if (hitno > 0) {
							boolwall = true;
						} else {
							boolwall = false;
						}
						break;

					case 2:
						v2start = new Vector2 (v3nodes[i-1].x, v3nodes[i-1].y);
						v2end = new Vector2 (v3nodes[i-1].x, v3nodes[i-1].y + 9);
						hitno = Physics2D.RaycastNonAlloc (v2start, v2end, rays, 1 << LayerMask.NameToLayer ("Objects"));
						if (hitno > 0) {
							boolwall = true;
						} else {
							boolwall = false;
						}
						break;
					case 3:
						v2start = new Vector2 (v3nodes[i-1].x, v3nodes[i-1].y);
						v2end = new Vector2 (v3nodes[i-1].x - 9, v3nodes[i-1].y);
						hitno = Physics2D.RaycastNonAlloc (v2start, v2end, rays, 1 << LayerMask.NameToLayer ("Objects"));
						if (hitno > 0) {
							boolwall = true;
						} else {
							boolwall = false;
						}
						break;
					case 4:
						v2start = new Vector2 (v3nodes[i-1].x, v3nodes[i-1].y);
						v2end = new Vector2 (v3nodes[i-1].x, v3nodes[i-1].y - 9);
						hitno = Physics2D.RaycastNonAlloc (v2start, v2end, rays, 1 << LayerMask.NameToLayer ("Objects"));
						if (hitno > 0) {
							boolwall = true;
						} else {
							boolwall = false;
						}
						break;
					}

					i += 1;
					j += 1;

					if(!boolwall){
						Debug.Log ("We did it");
						i -= 1;
						v3startLocation = v3nodes [i];
						break;
					}

				}	


			}
			

			Debug.Log ("find path no " + i);

			if (i > 200) {
				Debug.Log ("Did not find a path");
				break;
			}

			i += 1;
			/*
			if (i > 10) {
				break;
			}
			foreach (Vector3 node in v3nodes) {
				Debug.Log (node);
			}
			*/
		}

		i = 1;
		foreach (Vector3 vet in v3nodes) {
			Debug.Log (vet);
			
		}
		Debug.Log (v3destination);
	}

	static bool UpOrDown(){
		bool upOrDown;
		if (v3destination.y - v3startLocation.y > 0) {
			upOrDown = true;
		} else {
			upOrDown = false;
		}
		return upOrDown;
	}
	static bool LeftOrRight(){
		bool leftOrRight;
		if (v3destination.x - v3startLocation.x > 0) {
			leftOrRight = false;
		} else {
			leftOrRight = true;
		}
		return leftOrRight;
	}
	static bool ChangeXY(int active){

		switch (active) {
		case 0:
			float ran = Random.value;
			if (ran > 0.9) {
				XYstate = !XYstate;
			}
			break;
		case 1:
			XYstate = false;
			break;
		case 2:
			XYstate = true;
			break;
		
		}

		return XYstate;

	}

}
