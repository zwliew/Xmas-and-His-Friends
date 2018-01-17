using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
/*
 * This script pulls the listQuestions<intPianp> and dicAnswer<Vector2Int(intPianp1, intPianp2), strZi> from the data base
 * TODO the above mentioned function is yet to be achieved
 * PinZiGM.QuestionList(): get the list of pianpang
 * PinZiGM.AnswerDictionary(): get the dictionary of answers
 * PinZiGM.ResetGame(): reset the counter for selected pianpang by player
 */ 
public class PinZiGM : MonoBehaviour {
    private static string pinZiDataFileName = "PinZiData.json";
    private static PinZiWord[] words;

	private static int curChoice;
	public GameObject sprAns;

    private static string[] curSelection;
    private static PinZiWord curWord;

	// Use this for initialization
	void Awake()
    {
        LoadPinZiData();
        ResetGame();
    }

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Objects"))) {
				if (curChoice == 0) {
                    curChoice = 1;
					curSelection[0] = hitInfo.collider.gameObject.GetComponent<PinZiPP> ().spriteNo.ToString();
					hitInfo.collider.gameObject.GetComponent<PinZiPP> ().SetSelected (); 
				} else {
					if (curChoice == 1) {
						curSelection[1] = hitInfo.collider.gameObject.GetComponent<PinZiPP> ().spriteNo.ToString();
						hitInfo.collider.gameObject.GetComponent<PinZiPP> ().SetSelected (); 
						DisplayResult ();
                        curChoice = 0;
					}
				}
			}
		}
	}

	private void DisplayResult() {
        if (Array.Exists(curWord.correctSideIndices, element => string.Equals(element, curSelection[0])) &&
            Array.Exists(curWord.correctSideIndices, element => string.Equals(element, curSelection[1])))
        {
            DisplayCorrectResult();
        } else
        {
            DisplayWrongResult();
        }
	}

    private void DisplayWrongResult()
    {
        Debug.Log("Wrong result!");
    }

    private void DisplayCorrectResult()
    {
        Debug.Log("Correct result!");
        GameObjectUtility.customInstantiate(sprAns, new Vector3(0, 0, -3));
        // TODO
    }

    public static PinZiWord GetRandomWord()
    {
        int wordIndex = UnityEngine.Random.Range(0, words.Length);
        return words[wordIndex];
    }

	private static void LoadPinZiData()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, pinZiDataFileName);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            PinZiWordData wordData = JsonUtility.FromJson<PinZiWordData>(dataAsJson);
            words = wordData.words;
        }
        else
        {
            Debug.LogError("PinZiData file does not exist!");
        }
	}

	public static void ResetGame() {
		curChoice = 0;
        curWord = GetRandomWord();
    }
}
