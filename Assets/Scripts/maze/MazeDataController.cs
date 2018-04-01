using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MazeDataController : MonoBehaviour {
    public int length = 12;
    private string[] sentences;
	// Use this for initialization
	void Start () {
        sentences = new string[52];
        List<string>[] sentence = new List<string>[2];
        ReadString();
       // sentence = GetRandomSentence();
        //Debug.Log(sentence[0][0]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ReadString()
    {
        string path = "Assets/Resources/Maze/CharSpawner.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader == null);
        for(int i = 0; i < 52; i++)
        {
            sentences[i] = reader.ReadLine();
            Debug.Log(sentences[i]);
        }

        reader.Close();
    }

   /*  private List<string>[] GetRandomSentence()
    {
       List<string>[] SenInstance = new List<string>[2];
        int index = Random.Range(0,20);
        if (sentences == null)
        {
            Debug.Log("sentences is null");
        }
        string thisSentence = sentences[index];
        for (int i = 0; i < thisSentence.corSentence.Length; i++)
        {
            SenInstance[1].Add(thisSentence.corSentence.Substring(i, 1));
        }
        for (int i = 0; i < thisSentence.randSentence.Length; i++)
        {
            SenInstance[2].Add(thisSentence.corSentence.Substring(i, 1));
        }
        return SenInstance;
    }*/
    private void Refresh()
    {
        
    }

    private void UpdatePlayerData(bool win)
    {

    }
    public void RoundEnd(bool win)
    {
        UpdatePlayerData(win);
    }
}
