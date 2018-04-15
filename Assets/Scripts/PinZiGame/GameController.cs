using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
	private bool hasWon; //So that each round can only be won once;
	private bool isPlaying;

    public AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;

    void Start () {

		dataController = GetComponent<DataController>();

		displayController = GetComponent<DisplayController> ();

		RestartGame ();

	}

	public void RestartGame () {
		hasWon = false;
		isPlaying = true;
		dataController.Initialize ();
		curWord = dataController.GetRandomWord ();
		displayController.Initialize (curWord);
		curSelections = new string[2];
	}

	void Update () {
		if (hasWon) {
			if (Input.GetMouseButtonDown (0)) {
				displayController.EndGameUINext ();
			}
		}
		#if UNITY_EDITOR	
		if (Input.GetMouseButtonUp (0)) {//Avoid accidental clicking
			PinZiPP selectedSide = GetSelectedSide (Input.mousePosition);
			if (selectedSide != null) {
				SelectSide (selectedSide);
			}
		}
		#endif

		#if UNITY_STANDALONE
		if (Input.GetMouseButtonUp (0)) {//Avoid accidental clicking
			PinZiPP selectedSide = GetSelectedSide (Input.mousePosition);
			if (selectedSide != null) {
				SelectSide (selectedSide);
			}
		}
		#endif

	}

	public void ClickedHere(Vector3 touchPosition){//Receive touchInput from RotateCube
		PinZiPP selectedSide = GetSelectedSide (Input.mousePosition);
		if (selectedSide != null) {
			SelectSide (selectedSide);
		}
	}

	/**
	* Gets the side being selected based on position
	* Returns null if no side is being selected
	*/
	private PinZiPP GetSelectedSide (Vector3 position) {
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hitInfo;
		PinZiPP side = null;
		
		if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("PinZiSide")))
		{
			if (hitInfo.collider.gameObject.GetComponent<PinZiPP> () != null) {
				side = hitInfo.collider.gameObject.GetComponent<PinZiPP> ();
			}
		}
		return side;
	}

	private void SelectSide(PinZiPP side) {
		if (!isPlaying) {
			return;
		}
		if (curSelections [0] == null) {
			displayController.SelectSide (side);
			curSelections [0] = side.sidename;
			Debug.Log ("curSelection[0] is " + side.sidename);
		} else {
			if (curSelections [0] == side.sidename) {
				curSelections = new string[2];
				Debug.Log ("curSelection[0] is " + "de-selected.");
				displayController.SelectSide (side);
			} else {
				displayController.SelectSide (side);
				curSelections [1] = side.sidename;
				Debug.Log ("curSelection[1] is " + side.sidename);

				if (HasPlayerWon ()) {
					if (hasWon) {
						Debug.Log ("has already won this round");
						return;
					}
					hasWon = true;
					isPlaying = false;
                    audioSource.PlayOneShot(winSound);
					displayController.DisplayWin ();
					dataController.WinThisRound ();
				} else {
					Debug.Log ("Wrong Selection!" + " Correct answer is" + curWord.correctSides[0] + ", " + curWord.correctSides[1] );
					displayController.UnselectAllSides ();
					curSelections = new string[2];
                    audioSource.PlayOneShot(loseSound);
					isPlaying = false;
                    displayController.DisplayLose ();
				}
			}
		}
	}

	private bool AreSelectionsFilled() {
		return curSelections [0] != null && curSelections [1] != null;
	}

	private bool HasPlayerWon () {
		if (!AreSelectionsFilled()) {
			return false;
		}
		string[] correctSelections = curWord.correctSides;
		return Array.Exists (correctSelections, element => string.Equals (element, curSelections [0]))
			&& Array.Exists (correctSelections, element => string.Equals (element, curSelections [1]));
	}
		
}
