using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour {
	[HideInInspector]
	public string holdFlag;
	[HideInInspector]
	public bool holdFlagTwo;

//	private UnityEngine.AI.NavMeshAgent navAgent;

//	private Rigidbody rbXmas;

	private Animator xmasAnimator;

//	private int stepCountOne = 0;
//	private int stepCountTwo = 0;

	void Awake(){
		holdFlag = "";
		holdFlagTwo = false;
//		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
//		rbXmas = GetComponent<Rigidbody> ();
		xmasAnimator = GetComponent<Animator> ();
		xmasAnimator.SetInteger ("MoveState", 0);
	}
		
	public void SetMovingState(int movingState){
		xmasAnimator.SetInteger ("MoveState", movingState);
	}

	IEnumerator EnterGame(string gameName){
		yield return new WaitForSeconds (1f);
		switch (gameName) {
		case "PinZiGame":
			SceneManager.LoadScene ("PinZiGame");
			break;
		case "GuiChu":
			SceneManager.LoadScene ("tongue");
			break;
		case "RenZhe":
			SceneManager.LoadScene ("CharacterRenZhe");
			break;
		}
	}
	void OnTriggerStay (Collider other){
		FurnitureProperty furScript = null;
		furScript = other.gameObject.GetComponent<FurnitureProperty> ();
		if (furScript != null) {
			if(furScript.furnitureTag == holdFlag){
				Camera.main.GetComponent<CameraMovement> ().followState = 1;
				StartCoroutine (EnterGame (furScript.furnitureTag));
			}
		}
	}

		/*
		if(other.gameObject.tag.Equals("ElevatorOne") && holdFlagTwo){
			Debug.Log("Going up");
			holdFlagTwo = false;
			navAgent.isStopped = true;
			navAgent.enabled = false;
			
			//Bring the character to second floor
			stepCountOne = 0;
			stepCountTwo = 0;
			StartCoroutine(GoUp ());
			//TODO after things are done, set active
			
		}
	}
	*/
	/*
	IEnumerator ElevatorUp(){
		stepCountOne = 0;
	    stepCountTwo = 0;
		yield return StartCoroutine(GoUp ());
	}


	IEnumerator GoUp(){
		rbXmas.MovePosition (transform.position + new Vector3(0f, 0.5f, 0f));
		Debug.Log ("GoUpMove " + stepCountOne);
		Debug.Log (GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled);
		while(stepCountOne < 20) {
			stepCountOne += 1;
			yield return new WaitForFixedUpdate ();
			StartCoroutine (GoUp ());
		}
		Debug.Log ("Go up finished");
		StartCoroutine(GoForward ());
	}
	IEnumerator GoForward(){
		rbXmas.MovePosition(transform.position + new Vector3(0f, 0f, 0.5f));
		Debug.Log ("GoForwardMove " + stepCountTwo);
		if (stepCountTwo < 20) {
			stepCountTwo += 1;
			yield return new WaitForFixedUpdate ();
			StartCoroutine (GoForward ());
		}
	}
*/

}
