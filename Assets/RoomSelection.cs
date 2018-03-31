using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelection : MonoBehaviour {
	public Button previousBtn;
	public Button nextBtn;
	public Button roomOneBtn;
	public Button roomTwoBtn;
	public Image fadeImage;
	private int currentRoomNumber;

	void Start(){
		currentRoomNumber = 0;
		Refresh ();
	}

	private void Refresh(){
		//fadeImage.GetComponent<Animator> ().SetTrigger ("Exit");
		switch (currentRoomNumber) {
		case 0:
			roomOneBtn.gameObject.SetActive (true);
			roomTwoBtn.gameObject.SetActive (false);
			//Debug.Log ("Now showing roomOne");
			break;
		case 1:
			roomOneBtn.gameObject.SetActive (false);
			roomTwoBtn.gameObject.SetActive (true);
			//Debug.Log ("Now showing roomTwo");
			break;
		default:
			Debug.Log ("Now showing default");
			break;
		}
	}

	public void PreviousRoom(){
		currentRoomNumber = 0;
		Refresh ();
	}

	public void NextRoom(){
		currentRoomNumber = 1;
		Refresh ();
	}
}
