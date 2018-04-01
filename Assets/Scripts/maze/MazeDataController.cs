using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataController : MonoBehaviour {
    public int length = 12;
    private Sentences[] sentences;
	// Use this for initialization
	void Start () {
        List<string>[] sentence = new List<string>[2];
        
        LoadData();
        sentence = GetRandomSentence();
        Debug.Log(sentence[0][0]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadData()
    {
        TextAsset dataAsJson = Resources.Load<TextAsset>("CharSpawner");
        JsonData2 sentenceData = JsonUtility.FromJson<JsonData2>(dataAsJson.text);
        Debug.Log(sentenceData);
        sentences = sentenceData.sentences;
        Debug.Log("sentences: " + sentenceData.sentences);
    }

    private List<string>[] GetRandomSentence()
    {
        List<string>[] SenInstance = new List<string>[2];
        int index = 0;
        if (sentences == null)
        {
            Debug.Log("sentences is null");
        }
        Sentences thisSentence = sentences[index];
        for (int i = 0; i < thisSentence.corSentence.Length; i++)
        {
            SenInstance[1].Add(thisSentence.corSentence.Substring(i, 1));
        }
        for (int i = 0; i < thisSentence.randSentence.Length; i++)
        {
            SenInstance[2].Add(thisSentence.corSentence.Substring(i, 1));
        }
        return SenInstance;
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
