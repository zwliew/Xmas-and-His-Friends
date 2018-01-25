using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles displaying of the game, including the
 * 4 sides, the selections, and the winning screen
 */
public class DisplayController {

	// The current word being displayed
	private Word word;

	public void Initialize (Word word) {
		this.word = word;
		DisplayAllSides ();
	}

	private void Reset () {
		// TODO: Remove everything that is currently being displayed
	}

	private void DisplayAllSides () {
		string[] sides = word.sides;
		// TODO: Display all 4 side indices of the current word
		/* Initialize a pianpang at vec3location
		 * Get the PinZiPP script by GetComponent
		 * PinZiPP.SetDisplay(Texture2D texture2D);
		 */
	}

	public void DisplayWin () {
		// TODO: Display the winning screen and the correct word
	}

	public void SelectSide (string side) {
		// TODO: Indicate that a particular side is selected
	}

	public void UnselectAllSides() {
		// TODO: Unselect all sides (occurs when the player selects 2 incorrect sides)
	}
}
