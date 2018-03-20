using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles displaying of the game, including the
 * 4 sides, the selections, and the winning screen
 */
public class DisplayController : MonoBehaviour{

	// The current word being displayed
	private Word word;

	public GameObject[] goPlaceHolders;
	public Image ansImage;
	public CanvasGroup endGameUICanvasGroup;


	private Texture2D[] texture2DSides;
	private Texture2D texture2DAns;

	private Vector3[] v3Positions = new Vector3[5]{
		new Vector3(-1.64f, 1.96f, 0f), 
		new Vector3(-0.04f, 3.32f, 0f), 
		new Vector3(1.72f, 2.32f, 0f), 
		new Vector3(0.02f, -0.37f, 0f),
		new Vector3(0f, 1.5f, -3f)
	};

	private PinZiPP[] selectedSides = new PinZiPP[2];

	
	public GameObject[] prefabPianPangs;
	private GameObject[] priPrefabPianPangs = new GameObject[5];
	public GameObject goTetra;
	public GameObject goRotateCubeScriptHolder;
	private RotateCube rotateCubeScript;

	public void Initialize (Word word) {
		this.word = word;
		selectedSides = new PinZiPP[2];
		rotateCubeScript = goRotateCubeScriptHolder.GetComponent<RotateCube> ();
		Reset ();
		priPrefabPianPangs = new GameObject[5];
		DisplayAllSides ();
		Resources.UnloadUnusedAssets ();
		endGameUICanvasGroup.gameObject.SetActive (false);
	}

	public void Reset(){

		rotateCubeScript.PlayRotateAnimation ();

		for (int i = 0; i < 5; i++) {
			if (priPrefabPianPangs[i] != null) {
				GameObjectUtility.customDestroy (priPrefabPianPangs [i]);
			}
		}
	}

	private void DisplayAllSides () {
		StartCoroutine (LoadPinZiResources ());
	}

	IEnumerator LoadPinZiResources(){
		string[] sides = word.sides;
		texture2DSides = new Texture2D[sides.Length];

		//Debug.Log ("Start Loading");

		for (int i = 0; i < sides.Length; i++) {
			string strTexturePath = "PinZiPianPang/" + sides [i].ToString ();
			//Debug.Log ("Loading " + (i+1) + " " +strTexturePath);
			texture2DSides[i] =	Resources.Load (strTexturePath) as Texture2D;
			//Debug.Log ("Loaded " + texture2DSides [i].name);
			//Debug.Log ("Loading... " + (i+1) + "/" + sides.Length);
			yield return new WaitForFixedUpdate ();
		}

		string strCorrectTexturePath = "PinZiPianPang/" + word.name.ToString ();
		texture2DAns =	Resources.Load (strCorrectTexturePath) as Texture2D;
		//Debug.Log ("Loaded " + texture2DAns.name);

		//Debug.Log ("Start assigning");

		for (int i = 0; i < sides.Length; i++) {
			priPrefabPianPangs[i] = GameObjectUtility.customInstantiate (prefabPianPangs [i], goPlaceHolders[i].transform.position, goPlaceHolders[i].transform.rotation);
			priPrefabPianPangs [i].transform.parent = goTetra.transform; 
			//Debug.Log ("Getting pinZiScript");
			PinZiPP pinZiScript = priPrefabPianPangs[i].GetComponent<PinZiPP> ();
			//Debug.Log ("Initializing");
			pinZiScript.Initialize ();
			//Debug.Log ("Setting texture");
			pinZiScript.SetDisplay (texture2DSides [i]);
			pinZiScript.SetOriginalPosition (v3Positions [i]);
			pinZiScript.sidename = texture2DSides [i].name;
			//Debug.Log ("-----------Assigned: " + (i + 1) + "/" + sides.Length + "------------");
			yield return new WaitForFixedUpdate ();
		}

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

	public void DisplayWin () {
		
		
		Debug.Log ("Win!");
		endGameUICanvasGroup.gameObject.SetActive (true);
		Sprite ans = Sprite.Create (texture2DAns, new Rect(0f, 0f, texture2DAns.width, texture2DAns.height), new Vector2(0f, 0f));
		ansImage.sprite = ans;
		
	}

}
