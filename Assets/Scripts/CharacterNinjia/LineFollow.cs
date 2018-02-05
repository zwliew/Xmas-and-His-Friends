using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour {

    LineRenderer line;
    private int vertexCount;
    private bool mousedown = false;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        PlayerPrefs.SetInt("score", 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mousedown = true;
        }
        if (mousedown)
        {
            line.positionCount = vertexCount + 1;
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15f));
            line.SetPosition(vertexCount, mousepos);
            vertexCount++;
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            collider.transform.position = line.transform.position;
            collider.size = new Vector2(0.2f, 0.2f);
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            vertexCount = 0;
            line.positionCount = 0;
            BoxColliderDestroyer();
            mousedown = false;

        }
        
        /* if (!mousedown)
         {
         } */
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.tag = "destroyed";
		GameObjectUtility.customDestroy(collision.gameObject);
        /*if(collision.gameObject == null){
            addScore(); }*/
    }

    void addScore()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
    }
    void BoxColliderDestroyer()
    {
        BoxCollider2D[] boxes = line.GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D _box in boxes)
        {
            Destroy(_box);
        }
    }

}
