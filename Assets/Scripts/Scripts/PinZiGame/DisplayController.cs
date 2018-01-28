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

    public GameObject winScreen;
    public GameObject button;

	private Texture2D[] texture2DSides;

	private Vector3[] v3Positions = new Vector3[4]{new Vector3(-1.64f, 1.96f, 0f), new Vector3(-0.04f, 3.32f, 0f), new Vector3(1.72f, 2.32f, 0f), new Vector3(0.02f, -0.37f, 0f)};
	private PinZiPP[] selectedSides = new PinZiPP[2];
	public GameObject[] prefabPianPangs;


	public void Initialize (Word word) {
		this.word = word;
		DisplayAllSides ();
	}

	private void Reset () {
		// TODO: Remove everything that is currently being displayed
	}

	private void DisplayAllSides () {
		string[] sides = word.sides;
		texture2DSides = new Texture2D[sides.Length];
		// TODO: Display all 4 side indices of the current word
		/* Initialize a pianpang at vec3location
		 * Get the PinZiPP script by GetComponent
		 * PinZiPP.SetDisplay(Texture2D texture2D);
		 */
		for (int i = 0; i < sides.Length; i++) {
			string strTexturePath = "PinZiPianPang/" + sides [i].ToString ();
			Debug.Log ("Loading side" + i + " " +strTexturePath);
			texture2DSides[i] =	Resources.Load (strTexturePath) as Texture2D;
			Debug.Log ("Loaded " + texture2DSides [i].name);
			GameObjectUtility.customInstantiate (prefabPianPangs[i], v3Positions[i]);
			prefabPianPangs[i].GetComponent<PinZiPP> ().SetDisplay (texture2DSides [i]);
			prefabPianPangs[i].GetComponent<PinZiPP> ().SetOriginalPosition(v3Positions[i]);
		}
	}

	public void DisplayWin () {

        winScreen.SetActive(true);
        button.SetActive(true);
		Debug.Log ("Win!");
    }


	public void SelectSide (PinZiPP side) {

		// TODO: Indicate that a particular side is selected

		side.SetSelected ();

		if (selectedSides [0] == null) {// record what has been selected for UnselectAllSides
			selectedSides [0] = side;
		} else {
			selectedSides [1] = side;
		}
	}

	public void UnselectAllSides() {
		// TODO: Unselect all sides (occurs when the player selects 2 incorrect sides)
		if (selectedSides [0] != null) {
			selectedSides [0].SetUnselected ();
		}
		if (selectedSides [1] != null) {
			selectedSides [1].SetUnselected ();
		}
		selectedSides = new PinZiPP[2];
	}
}
