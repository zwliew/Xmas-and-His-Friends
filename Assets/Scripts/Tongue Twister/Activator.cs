using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public Touch touch;
    public KeyCode key;
    bool active = false;
    GameObject note;
    SpriteRenderer sr;
    Color old;
    public bool createMode;
    public GameObject n;
    // Use this for initialization
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start () {
        old = sr.color;
        PlayerPrefs.SetInt("score", 0);

    }

    // Update is called once per frame
    void Update () {
        if (createMode)
        {
            if (Input.GetKeyDown(key)) {
                Instantiate(n, transform.position, Quaternion.identity);
}

        }
        else
        {
            if (Input.GetKeyDown(key))
            {
                StartCoroutine(pressed());
            }
            if (Input.GetKeyDown(key) && active)//Input.GetTouch(0).position.x>-6&& Input.GetTouch(0).position.x<-3&&Input.GetTouch(0).position.y>-4&& Input.GetTouch(0).position.x < -2 && active) //detect area touched
            {
                Destroy(note);
                addScore();
                active = false;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "note")
        {
            active = true;
            note = col.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    void addScore()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);

    }

    IEnumerator pressed()
    {
        old = sr.color;
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.02f);
        sr.color = old;
    }
}

