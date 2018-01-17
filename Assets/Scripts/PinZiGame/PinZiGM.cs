using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script pulls the listQuestions<intPianp> and dicAnswer<Vector2Int(intPianp1, intPianp2), strZi> from the data base
 * TODO the above mentioned function is yet to be achieved
 * PinZiGM.QuestionList(): get the list of pianpang
 * PinZiGM.AnswerDictionary(): get the dictionary of answers
 * PinZiGM.SetChooseZero(): reset the counter for selected pianpang by player
 */ 
public class PinZiGM : MonoBehaviour {
	static List<int> theList = new List<int> ();
	static Vector2Int v2Ans;
	static Dictionary<Vector2Int, string> theDic = new Dictionary<Vector2Int, string>();
	static int Choose;
	static int qnNo;
	static List<int> qnList = new List<int>();
	public GameObject sprAns;

	// Use this for initialization
	void Awake() {
		Choose = 0;
		qnNo = 0;
		v2Ans = new Vector2Int (999, 999);
		theList.Clear ();
		theDic.Clear ();

		//Get QnList
		for (int i = 0; i < 8; i++) {
			theList.Add (i);
			//Debug.Log (i + " added to the list");
		}

		//Get AnsDic
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
			GameObjectUtility.customInstantiate (sprAns, new Vector3(0, 0,-3));
			Debug.Log(theDic[ans]);
		}
	}

	public static List<int> QuestionList(){
		return theList;
	}

	public static Dictionary<Vector2Int, string> AnswerDictionary(){
		return theDic;
	}

	
	public static void PrepareQnList(){
		qnList.Clear ();
		qnList.Add (0);
		qnList.Add (1);
		qnList.Add (2);
		qnList.Add (3);
		
	}

	public static int GetNextNo(){
		if (qnNo > 3)
			qnNo = 0;
		Debug.Log (qnNo);
		int nextNo = 0;
		switch (qnNo) {
		case 0:
			qnNo += 1;
			nextNo = qnList[0];
			break;
		case 1:
			qnNo += 1;
			nextNo = qnList[1];
			break;
		case 2:
			qnNo += 1;
			nextNo = qnList[2];
			break;
		case 3:
			qnNo += 1;
			nextNo = qnList[3];
			break;
		}
		return nextNo;
	}

	public static void SetChooseZero(){
		Choose = 0;
		v2Ans = new Vector2Int (999, 999);
	}
	public static void GetReady(){
		SetChooseZero ();
		PrepareQnList ();
	}
}
