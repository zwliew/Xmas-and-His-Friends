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

		if (Input.GetMouseButtonDown (0)) {
			string selectedSide = GetSelectedSide (Input.mousePosition);
			if (selectedSide != null) {
				displayController.SelectSide (selectedSide);
				SelectSide (selectedSide);
			}
		}
		if (Input.GetMouseButtonDown (1)) {// Just for clearing selected effects. Merge with Display controller in the future
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("PinZiSide")))
			{
				hitInfo.collider.gameObject.GetComponent<PinZiPP> ().SetUnselected ();
			}
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
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;
        string side = null;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("PinZiSide")))
        {
            side = hitInfo.collider.gameObject.GetComponent<PinZiPP>().name;
			hitInfo.collider.gameObject.GetComponent<PinZiPP> ().SetSelected ();
        }
		return side;
	}
}
