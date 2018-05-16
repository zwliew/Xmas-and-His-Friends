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
            Word word = words[i];
            string[] sides = word.sides;
	    texture2DSides = new Texture2D[sides.Length];
            for (int i = 0; i < sides.Length; i++) {
			string strTexturePath = "PinZiPianPang/" + sides [i].ToString ();
		    	//Debug.Log ("Loading " + (i+1) + " " +strTexturePath);
		    	texture2DSides[i] =	Resources.Load (strTexturePath) as Texture2D;
	    		texture2DSidesSelected[i] =	Resources.Load (strTexturePath+"r") as Texture2D;
    		//Debug.Log ("Loaded " + texture2DSides [i].name);
	    	//Debug.Log ("Loading... " + (i+1) + "/" + sides.Length);
    		yield return new WaitForFixedUpdate ();
    	    }
            string strCorrectTexturePath = "PinZiPianPang/" + word.name.ToString ();
	    	texture2DAns =	Resources.Load (strCorrectTexturePath) as Texture2D;
            for (int i = 0; i < sides.Length; i++) {
			    priPrefabPianPangs[i] = GameObjectUtility.customInstantiate (prefabPianPangs [i], goPlaceHolders[i].transform.position, goPlaceHolders[i].transform.rotation);
		    	priPrefabPianPangs [i].transform.parent = goTetra.transform; 
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
