using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinZiGameDebug : Monobehaviour{
    
    public DataController datacontroller;
    public GameObject[] 
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
        
    }
    
}
