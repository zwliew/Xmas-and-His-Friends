using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class MazeDataController : MonoBehaviour {
    public int length = 12;
    private string[] sentences;
    public OverallGameManager ogm;
    public List<string> corSentence = new List<string>();
    public List<string> randSentence = new List<string>();
   // private PlayerDataController playerData = new PlayerDataController();
	private PlayerDataController playerData;
	// Use this for initialization
	public void Start () {
		GameObject playerDatagO = GameObject.FindGameObjectWithTag ("Persistent");
		if(playerDatagO != null)
			playerData = playerDatagO.GetComponent<PlayerDataController> ();
		//StartNewRound ();
    }

	public void StartNewRound(){
		sentences = new string[52];
		ReadString();
		Refresh();
		Debug.Log(corSentence[0]);
		// sentence = GetRandomSentence();
	}

    // Update is called once per frame
    void Update () {
		
	}

    public void Refresh()
    {
        corSentence = GetCorrectSentence();
        randSentence = GetRandomSentence();
    }

    void ReadString()
    {
        string path = "Assets/Resources/Maze/CharSpawner.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path, Encoding.GetEncoding("gb2312") );
        for(int i = 0; i < 51; i++)
        {
            sentences[i] = reader.ReadLine();
        }

        reader.Close();
    }

     private List<string> GetCorrectSentence()
    {
        List<string> characters = new List<string>();
        int index = Random.Range(0, 50);
        for (int i = 0; i < sentences[index].Length; i++)
        {
            string indiChar = sentences[index].Substring(i, 1);
            characters.Add(indiChar);
            Debug.Log(i + " " + characters[i]);
        }
        return characters;
    }

    private List<string> GetRandomSentence()
    {
        List<string> characters = new List<string>();
        for (int i = 0; i < 25; i++)
        {
            int randomInt = Random.Range(0, 50);
            characters.Add(sentences[randomInt].Substring(1, 1));
            Debug.Log("characters: " + i +" " + characters[i]);
        }
        return characters;
    }
    private void UpdatePlayerData(bool win)
    {
        if (win)
        {
			if (playerData != null) {
				playerData.UpdatePlayerCoins (5);
				Debug.Log ("Player Data(coins) updated");
			}
        }

    }
    public void RoundEnd(bool win)
    {
        UpdatePlayerData(win);
    }
}
