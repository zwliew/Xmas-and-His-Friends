using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallGameManager : MonoBehaviour {
    private List<string> corSentence = new List<string>();
    private List<string> randSentence = new List<string>();
    public MazeDisplayController displayController;
    public MazeDataController dataController;

	public GameObject StartPage;
	public GameObject WinPage;
	public GameObject LostPage;
	// Use this for initialization
	void Start () {
        Debug.Log("OGM has started");
		StartPage.SetActive (true);
		WinPage.SetActive (false);
		LostPage.SetActive (false);

    }

	public void StartNewRound(){
		dataController.StartNewRound ();
		displayController.StartNewRound ();
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

    public void Repeat()
    {
        displayController.Repeat();
    }
    public void HandOverData(List<string> corSentence, List<string>randSentence)
    {

        Debug.Log("passing data");
    }

    void Result(bool win)
    {
		dataController.RoundEnd(win);
        displayController.RoundEnd(win);
        Debug.Log("-------------"+"The game is won: " + win);
		if (win) {
			StartPage.SetActive (false);
			WinPage.SetActive (true);
			LostPage.SetActive (false);
		} else {
			StartPage.SetActive (false);
			WinPage.SetActive (false);
			LostPage.SetActive (true);
		}
    }
}
