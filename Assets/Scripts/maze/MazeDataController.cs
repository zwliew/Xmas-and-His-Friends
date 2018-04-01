using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class MazeDataController : MonoBehaviour {
    public int length = 12;
    private string[] sentences;
	// Use this for initialization
	void Start () {
        sentences = new string[52];
        List<string>[] sentence = new List<string>[2];
        ReadString();
        GetRandomSentence();
       // sentence = GetRandomSentence();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ReadString()
    {
        string path = "Assets/Resources/Maze/CharSpawner.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path, Encoding.GetEncoding("gb2312") );
        Debug.Log(reader == null);
        for(int i = 0; i < 51; i++)
        {
            sentences[i] = reader.ReadLine();
            Debug.Log(sentences[i]);
        }

        reader.Close();
    }

     private List<string> GetRandomSentence()
    {
        List<string> characters = new List<string>();
        int index = Random.Range(0, 49);
        for (int i = 0; i < sentences[index].Length; i++)
        {
            string indiChar = sentences[index].Substring(i, 1);
            characters.Add(indiChar);
            Debug.Log(i + " " + characters[i]);
        }
        for(int i = 0; i < (25 - sentences[index].Length); i++)
        {
            int randomInt = Random.Range(0, 100);
            Debug.Log(i);
        }
        return characters;
    }
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
