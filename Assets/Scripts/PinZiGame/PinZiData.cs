using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinZiData : MonoBehaviour {
	static List<int> theList = new List<int> ();
	static Vector2Int v2Ans;
	static Dictionary<Vector2Int, string> theDic = new Dictionary<Vector2Int, string>();

	// Use this for initialization
	void Start () {
		theList.Add (1);
		theList.Add (2);
		theList.Add (3);
		theList.Add (4);
		theList.Add (5);
		theList.Add (6);
		theList.Add (7);

		theDic.Add (new Vector2Int (1, 2), "three");
		theDic.Add (new Vector2Int (3, 4), "seven");
		theDic.Add (new Vector2Int (5, 6), "eleven");
	}
	
	public static List<int> QuestionList(){
		return theList;
	}

	public static Dictionary<Vector2Int, string> AnswerDictionary(){
		return theDic;
	}
}
