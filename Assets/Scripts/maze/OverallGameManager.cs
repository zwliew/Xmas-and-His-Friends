using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallGameManager : MonoBehaviour {
    private List<string> corSentence = new List<string>();
    private List<string> randSentence = new List<string>();
    public MazeDisplayController displayController;
    public MazeDataController dataController;
	// Use this for initialization
	void Start () {
        Debug.Log("OGM has started");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewRound()
    {
        Debug.Log("starting a new round");
        dataController.Refresh();
        displayController.NewRound();
    }

    void Repeat()
    {
        displayController.Repeat();
    }
    public void HandOverData(List<string> corSentence, List<string>randSentence)
    {

        Debug.Log("passing data");
    }

    void Result(bool win)
    {
        displayController.RoundEnd(win);
        dataController.RoundEnd(win);
        Debug.Log("The game is won: " + win);
    }
}
