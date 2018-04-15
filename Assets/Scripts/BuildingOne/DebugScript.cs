//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
////GetComponent<Renderer>().material.SetTexture ("_MainTex", (Texture2D)listTextures[2]);  //Set texture
//
//public class DebugScript : MonoBehaviour{
//
//
//	void Update(){
//		if (Input.GetMouseButtonDown (0)) {
//			List<Vector2> nodes = new List<Vector2> ();
//			nodes = GetNodes ();
//		}
//	}
//
//	public List<Vector2> GetNodes(){
//		List<Vector2> nodes = new List<Vector2>();
//		int row = 0;
//		int col = Random.Range(0, 6);
//		Vector2 newNode = new Vector2 (row, col);
//		nodes.Add(newNode);
//
//		while (row < 6) {
//			int noOfSteps = 0;
//			int direction = 999;//0 forward. -1 left. 1 right.
//			while (direction != 0) {
//				if (direction > 4) {//Toss a five-sided coin to decide where to go[first time only]
//					int coin = Random.Range (0, 5);//0 forward, 1,2 left, 3,4 right
//					switch (coin) {
//					case 0:
//						direction = 0;
//						break;
//					case 1:
//						direction = -1;
//						break;
//					case 2:
//						direction = -1;
//						break;
//					case 3:
//						direction = 1;
//						break;
//					case 4:
//						direction = 1;
//						break;
//					}
//				}
//
//				if ((col + direction) > 4 || (col + direction) < 0) {//decide whether moving horizontally is possible
//					direction = 0;
//				}
//
//				float anotherCoin = Random.Range (0f, 1f);// decide whether to move horizontally
//				if (anotherCoin > 0.5f) {
//					
//				} else {
//					direction = 0;
//				}
//
//				if (noOfSteps >= 3) {// decide whether Xmas has been at this row for too long
//					direction = 0;
//				}
//
//				if (direction != 0) {
//					col += direction;
//					noOfSteps += 1;
//					newNode = new Vector2 (row, col);
//					nodes.Add (newNode);
//				}
//
//			}
//				
//			row += 1;
//			newNode = new Vector2 (row, col);
//			nodes.Add (newNode);
//
//		}
//
//		foreach (Vector2 thisNode in nodes) {
//			Debug.Log (thisNode.x + ", " + thisNode.y);
//		}
//		Debug.Log ("------------" + nodes.Count + "-----------------");
//		return nodes;
//	}
//
//}