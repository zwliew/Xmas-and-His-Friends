using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour {

    [SerializeField]
    GameObject character;
    float maxThrowVelocity;
    float maxX;
    float minX;
    
        
	// Use this for initialization
	void Start () {
        maxX = 1.97f;
        minX = -1.97f;
        maxThrowVelocity = 15f;
        StartCoroutine(characterSP());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator characterSP()
    {
        GameObject _character = Instantiate(character);
        float number = Random.Range(minX, maxX);
        _character.transform.position = new Vector3(number, this.transform.position.y, _character.transform.position.z);
        _character.GetComponent<Rigidbody2D>().AddForce(new Vector2(-number, maxThrowVelocity), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(characterSP());
    }
}
