using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataController : MonoBehaviour {
    private List<Vector2> numberOfCharacters;
    
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            List<Vector2> nodes = new List<Vector2>();
            nodes = GetNodes();
        }*/

          numberOfCharacters =  GetNodes(12);
        Debug.Log(numberOfCharacters.Count);

        
    }

    public List<Vector2> GetNodes(int number)
    {
        List<Vector2> nodes = new List<Vector2>();
        int row = 0;
        int col = Random.Range(0, 6);
        Vector2 newNode = new Vector2(row, col);
        nodes.Add(newNode);

        while (row < 6)
        {
            int noOfSteps = 0;
            int direction = 999;//0 forward. -1 left. 1 right.
            while (direction != 0)
            {
                if (direction > 4)
                {//Toss a five-sided coin to decide where to go[first time only]
                    int coin = Random.Range(0, 1001);//0 forward, 1,2 left, 3,4 right
                    if(coin > 501)
                    {
                        direction = 1;
                    }
                    if (coin >1 && coin < 502)
                    {
                        direction = -1;
                    }
                    if (coin == 1)
                    {
                        direction = 0;
                    }
                   /* switch (coin)
                    {
                        case 0:
                            direction = 0;
                            break;
                        case 1:
                            direction = -1;
                            break;
                        case 2:
                            direction = -1;
                            break;
                        case 3:
                            direction = 1;
                            break;
                        case 4:
                            direction = 1;
                            break;
                    }*/
                }

                if ((col + direction) > 4 || (col + direction) < 0)
                {//decide whether moving horizontally is possible
                    direction = 0;
                }

                float anotherCoin = Random.Range(0f, 1f);// decide whether to move horizontally
                if (anotherCoin > 0.3f)
                {

                }
                else
                {
                    direction = 0;
                }

                if (noOfSteps >= 4)
                {// decide whether Xmas has been at this row for too long
                    direction = 0;
                }

                if (direction != 0)
                {
                    col += direction;
                    noOfSteps += 1;
                    newNode = new Vector2(row, col);
                    nodes.Add(newNode);
                }

            }

            row += 1;
            newNode = new Vector2(row, col);
            nodes.Add(newNode);

        }

        foreach (Vector2 thisNode in nodes)
        {
        }
        //Debug.Log("------------" + nodes.Count + "-----------------");
        while(nodes.Count != number)
        {
            nodes = GetNodes(number);
        }
        
        return nodes;
    }

   

}
