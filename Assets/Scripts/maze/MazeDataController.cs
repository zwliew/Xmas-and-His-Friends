using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataController : MonoBehaviour {

    int test = 12;
	void Start () {
        distribute(test);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private int[] distribute(int number)
    {
        int extra = number - 5;
        int[] numberPerRow = { 0,0,0,0,0 };
        int pointer = 0;
        while (extra > 0)
        {
            int curRow = Random.Range(0, 4);
            if(numberPerRow[curRow] < 4)
            {
                numberPerRow[curRow] += 1;
                extra -= 1;
            }
        }
        
        for(int i = 0; i < 6; i++)
        {
            pointer = numberPerRow[i];


        }
        foreach(int no in numberPerRow)
        {
            Debug.Log(no);
			Debug.Log (pointer);
        }
        Debug.Log("distribution complete: " + Time.deltaTime);
        return numberPerRow;
    }
}
