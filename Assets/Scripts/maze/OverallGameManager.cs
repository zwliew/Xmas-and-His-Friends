using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallGameManager : MonoBehaviour {
    private MazeDisplayController displayController;
    private MazeDataController dataController;
	// Use this for initialization
	void Start () {
        displayController = GetComponent<MazeDisplayController>();
        dataController = GetComponent<MazeDataController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void NewRound()
    {

    }

    void Repeat()
    {

    }

    void Result(bool win)
    {
        displayController.RoundEnd(win);
        dataController.RoundEnd(win);
        Debug.Log("The game is won: " + win);
    }
}
