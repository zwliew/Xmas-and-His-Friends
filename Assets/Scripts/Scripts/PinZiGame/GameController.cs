using System;
using UnityEngine;

/**
 * Controls the logic and flow of the current game
 * This delegates some heavy-work to separate sub-controllers.
 * 
 * Subcontrollers:
 * Handling the data: DataController
 * Displaying the game: DisplayController
 */
public class GameController : MonoBehaviour {

	private DataController dataController;
	private DisplayController displayController;

	private Word curWord;
	private string[] curSelections;

	void Start () {
		dataController = new DataController ();
		displayController = new DisplayController ();

		RestartGame ();
	}

	void Update () {
		if (HasPlayerWon ()) {
			displayController.DisplayWin ();
			return;
		}

		if (AreSelectionsFilled ()) {
			// Reset selections
			displayController.UnselectAllSides ();
			curSelections = new string[2];
			return;
		}

		if (!Input.GetMouseButtonDown (0)) {
			return;
		}

		string selectedSide = GetSelectedSide (Input.mousePosition);
		if (selectedSide == "") {
			displayController.SelectSide (selectedSide);
			SelectSide (selectedSide);
		}
	}

	private void RestartGame () {
		dataController.Initialize ();
		curWord = dataController.GetRandomWord ();
		displayController.Initialize (curWord);
		curSelections = new string[2];
	}

	private bool AreSelectionsFilled() {
		return curSelections [0] != null && curSelections [1] != null;
	}

	private bool HasPlayerWon () {
		if (!AreSelectionsFilled()) {
			return false;
		}
		string[] correctSelections = curWord.sides;
		return Array.Exists (correctSelections, element => string.Equals (element, curSelections [0]))
					&& Array.Exists (correctSelections, element => string.Equals (element, curSelections [1]));
	}

	private void SelectSide(string side) {
		if (curSelections [0] == null) {
			curSelections [0] = side;
		} else {
			curSelections [1] = side;
		}
	}

	/**
	 * Gets the side being selected based on position
	 * Returns null if no side is being selected
	 */
	private string GetSelectedSide (Vector3 position) {
		// TODO: Get the selected side based on position (or based on something else if it's easier)
		return null;
	}
}
