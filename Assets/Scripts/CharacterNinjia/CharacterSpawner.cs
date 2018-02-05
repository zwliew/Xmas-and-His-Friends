using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour {

    [SerializeField]
    GameObject character;
    float maxThrowVelocity;
    float maxX;
    float minX;

	private Word[] words;
	private DataController dataController;

	private static Dictionary <string, string[]> dicAnswers = new Dictionary<string, string[]>();
	private static Sprite[] sprWords;
	private static Sprite[] sprSides;
        
	// Use this for initialization
	void Start () {
        maxX = 1.97f;
        minX = -1.97f;
        maxThrowVelocity = 12f;

		sprWords = Resources.LoadAll<Sprite>("CharNinjia/Words");
		sprSides = Resources.LoadAll<Sprite>("CharNinjia/Sides");

		dataController = GetComponent<DataController> ();
		dataController.Initialize ();
		words = dataController.GetAllWords ();
		PrepareDicAnswers ();

        StartCoroutine(characterSP());
	}

	private void PrepareDicAnswers(){
		foreach (Word word in words) {
			dicAnswers.Add (word.name, word.correctSides);
		}
	}

	public static Sprite GetRandomSprite(){
		return sprWords[Random.Range (0, sprWords.Length - 1)];
	}

	public static Sprite[] GetSprSides(string charSpriteName){
		
		string[] strCorrectSides = null;
		Sprite[] sprCorrectSides = null;
		
		if (dicAnswers.TryGetValue (charSpriteName.ToUpper(), out strCorrectSides)) {
			sprCorrectSides = new Sprite[2];
			sprCorrectSides [0] = null;
			Debug.Log (strCorrectSides [0]);
			sprCorrectSides [1] = null;
			Debug.Log (strCorrectSides [1]);
		} else {
			Debug.Log ("Did not get the correct sides for this word: " + charSpriteName.ToUpper());
		}

		return sprCorrectSides;
	}

    IEnumerator characterSP()
    {
		GameObject _character =GameObjectUtility.customInstantiate(character, Vector3.zero);
        float number = Random.Range(minX, maxX);
        float waitTime = Random.Range(0.5f, 3f);
        _character.transform.position = new Vector3(number, this.transform.position.y, _character.transform.position.z);
        _character.GetComponent<Rigidbody2D>().AddForce(new Vector2(-number, maxThrowVelocity), ForceMode2D.Impulse);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(characterSP());
    }
}
