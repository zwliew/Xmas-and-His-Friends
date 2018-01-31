using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles displaying of the game, including the
 * 4 sides, the selections, and the winning screen
 */
public class DisplayController : MonoBehaviour{

	// The current word being displayed
	private Word word;

	private Texture2D[] texture2DSides;
	private Texture2D texture2DAns;

	private Vector3[] v3Positions = new Vector3[5]{
		new Vector3(-1.64f, 1.96f, 0f), 
		new Vector3(-0.04f, 3.32f, 0f), 
		new Vector3(1.72f, 2.32f, 0f), 
		new Vector3(0.02f, -0.37f, 0f),
		new Vector3(0f, 0.8f, -3f)
	};
	private PinZiPP[] selectedSides = new PinZiPP[2];

	
	public GameObject[] prefabPianPangs;
	public GameObject winScreen;
	public GameObject button;

	public void Initialize (Word word) {
		this.word = word;
		DisplayAllSides ();
	}

	private void Reset () {
		// TODO: Remove everything that is currently being displayed
	}

	private void DisplayAllSides () {
		StartCoroutine (LoadPinZiResources ());
	}

	IEnumerator LoadPinZiResources(){
		string[] sides = word.sides;
		texture2DSides = new Texture2D[sides.Length];

		Debug.Log ("Start Loading");

		for (int i = 0; i < sides.Length; i++) {
			string strTexturePath = "PinZiPianPang/" + sides [i].ToString ();
			//Debug.Log ("Loading " + (i+1) + " " +strTexturePath);
			texture2DSides[i] =	Resources.Load (strTexturePath) as Texture2D;
			//Debug.Log ("Loaded " + texture2DSides [i].name);
			Debug.Log ("Loading... " + (i+1) + "/" + sides.Length);
			yield return new WaitForFixedUpdate ();
		}

		string strCorrectTexturePath = "PinZiPianPang/" + word.name.ToString ();
		texture2DAns =	Resources.Load (strCorrectTexturePath) as Texture2D;
		Debug.Log ("Loaded " + texture2DAns.name);

		Debug.Log ("Start assigning");

		for (int i = 0; i < sides.Length; i++) {
			GameObject PianPang = GameObjectUtility.customInstantiate (prefabPianPangs [i], v3Positions [i]);
			Debug.Log ("Getting pinZiScript");
			PinZiPP pinZiScript = PianPang.GetComponent<PinZiPP> ();
			Debug.Log ("Initializing");
			pinZiScript.Initialize ();
			Debug.Log ("Setting texture");
			pinZiScript.SetDisplay (texture2DSides [i]);
			pinZiScript.SetOriginalPosition (v3Positions [i]);
			pinZiScript.sidename = texture2DSides [i].name;
			Debug.Log ("-----------Assigned: " + (i + 1) + "/" + sides.Length + "------------");
			yield return new WaitForFixedUpdate ();
		}

	}

	public void DisplayWin () {
        winScreen.SetActive(true);
        //button.SetActive(true);
		Debug.Log ("Win!");
		GameObject PianPang = GameObjectUtility.customInstantiate (prefabPianPangs [4], v3Positions [4]);
		Debug.Log ("Getting pinZiScript");
		PinZiPP pinZiScript = PianPang.GetComponent<PinZiPP> ();
		Debug.Log ("Initializing");
		pinZiScript.Initialize ();
		Debug.Log ("Setting texture");
		pinZiScript.SetDisplay (texture2DAns);

    }


	public void SelectSide (PinZiPP side) {

		side.SetSelected ();

		if (selectedSides [0] == null) {// record what has been selected for UnselectAllSides
			selectedSides [0] = side;
		} else {
			if (selectedSides [0] == side) {
				UnselectAllSides ();
			} else {
				selectedSides [1] = side;
			}
		}
	}

	public void UnselectAllSides() {
		
		if (selectedSides [0] != null) {
			selectedSides [0].SetUnselected ();
		}
		if (selectedSides [1] != null) {
			selectedSides [1].SetUnselected ();
		}

		selectedSides = new PinZiPP[2];
	}
}
