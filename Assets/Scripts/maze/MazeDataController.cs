using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataController : MonoBehaviour {
    public int length = 12;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
