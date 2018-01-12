using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinZiGM : MonoBehaviour {
	static List<int> theList = new List<int> ();
	static Vector2Int v2Ans;
	static Dictionary<Vector2Int, string> theDic = new Dictionary<Vector2Int, string>();
	static int Choose;

	// Use this for initialization
	void Awake() {
		Choose = 0;
		v2Ans = new Vector2Int (999, 999);
		theList.Clear ();
		theDic.Clear ();
		for (int i = 0; i < 8; i++) {
			theList.Add (i);
			Debug.Log (i + " added to the list");
		}

		theDic.Add (new Vector2Int (0, 1), "an");
		theDic.Add (new Vector2Int (1, 0), "an");
		theDic.Add (new Vector2Int (2, 3), "an");
		theDic.Add (new Vector2Int (3, 2), "an");
		theDic.Add (new Vector2Int (0, 3), "an");
		theDic.Add (new Vector2Int (3, 0), "an");
		theDic.Add (new Vector2Int (1, 2), "an");
		theDic.Add (new Vector2Int (2, 1), "an");
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Objects"))) {
				if (Choose == 0) {
					Choose = 1;
					v2Ans.x = hitInfo.collider.gameObject.GetComponent<PinZiPP> ().spriteNo;
					hitInfo.collider.gameObject.GetComponent<PinZiPP> ().SetSelected (); 
				} else {
					if (Choose == 1) {
						v2Ans.y = hitInfo.collider.gameObject.GetComponent<PinZiPP> ().spriteNo;
						hitInfo.collider.gameObject.GetComponent<PinZiPP> ().SetSelected (); 
						DisplayResult (v2Ans);
						Choose = 0;
					}
				}
			}
		}
	}

	void DisplayResult(Vector2Int ans){
		if(theDic.ContainsKey(ans)){
			Debug.Log(theDic[ans]);
		}
	}

	public static List<int> QuestionList(){
		return theList;
	}

	public static Dictionary<Vector2Int, string> AnswerDictionary(){
		return theDic;
	}
	public static void SetChooseZero(){
		Choose = 0;
		v2Ans = new Vector2Int (999, 999);
	}
}
