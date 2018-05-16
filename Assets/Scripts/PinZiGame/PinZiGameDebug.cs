using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinZiGameDebug : Monobehaviour{
    
    public DataController datacontroller;
    public GameObject[] formRows;
    private float xAxis;
    private float yAxis;
    private final float zAxis = 0f;
    
    private Word[] allWords;
    private Texture2D texture2DSides;
    private Texture2D texture2DAns;
	private GameObject[] priPrefabPianPangs = new GameObject[5];

    
    public void Start(){
        datacontroller.Initialize();
        allWords = datacontroller.GetAllWords();
    }
    
    public void Update(){
        if(Input.GetMouseBotton(0)){
            Debug.log("Displaying all words");
            DisplayAllWords();
        }
    }
    
    private void DisplayAllWords(){
        Debug.log("There are " + words.size.ToString() + " words to be displayed");
	    
        for(int i = 0; i<100; i++){
	    	xAxis = 10 * i;
	    	yAixs = 0;
		
        	Word word = words[i];
			string[] sides = word.sides;
			texture2DSides = new Texture2D[sides.Length];
		
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
		
			for (int i = 0; i < sides.Length; i++) {
				yAxis = 10 * i;
				priPrefabPianPangs[i] = GameObjectUtility.customInstantiate (prefabPianPangs [i], Vector3.zero);
				priPrefabPianPangs[i].transform.position = new Vector3(xAxis, yAxis,zAxis);
				//Debug.Log ("Getting pinZiScript");
				PinZiPP pinZiScript = priPrefabPianPangs[i].GetComponent<PinZiPP> ();
				//Debug.Log ("Initializing");
				pinZiScript.Initialize ();
				//Debug.Log ("Setting texture");
				pinZiScript.SetDisplay (texture2DSides [i], texture2DSidesSelected[i]);
				pinZiScript.sidename = texture2DSides [i].name;
				//Debug.Log ("-----------Assigned: " + (i + 1) + "/" + sides.Length + "------------");
				yield return new WaitForFixedUpdate ();
	    	}
    	}
	}
}
