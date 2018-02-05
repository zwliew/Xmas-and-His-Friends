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
	private static Dictionary<string, Sprite> dicSides = new Dictionary<string, Sprite> ();
	private static Sprite[] sprWords;
	private static Sprite[] sprSides;
        
	// Use this for initialization
	void Start () {
        maxX = 1.97f;
        minX = -1.97f;
        maxThrowVelocity = 12f;
		dicAnswers = new Dictionary<string, string[]>();
		dicSides = new Dictionary<string, Sprite> ();

		dataController = GetComponent<DataController> ();
		dataController.Initialize ();
		words = dataController.GetAllWords ();

		foreach(Word curword in words){
			
			string sidepath = "CharNinjia/Sides/" + curword.correctSides [0];
			dicSides.Add(curword.correctSides [0], Resources.Load<Sprite>(sidepath));

			sidepath = "CharNinjia/Sides/" + curword.correctSides [1];
			dicSides.Add(curword.correctSides [1], Resources.Load<Sprite>(sidepath));

			dicAnswers.Add (curword.name, curword.correctSides);
		}
		sprWords = Resources.LoadAll<Sprite>("CharNinjia/Words");

        StartCoroutine(characterSP());
	}



	public static Sprite GetRandomSprite(){
		return sprWords[Random.Range (0, sprWords.Length - 1)];
	}

	public static Sprite[] GetSprSides(string charSpriteName){
		
		string[] strCorrectSides = null;
		Sprite[] sprCorrectSides = null;
		
		if (dicAnswers.TryGetValue (charSpriteName.ToUpper(), out strCorrectSides)) {
			sprCorrectSides = new Sprite[2];

			if(dicSides.TryGetValue(strCorrectSides [0], out sprCorrectSides[0])){
			}else{
				Debug.Log("Did not get side one for this word: " + strCorrectSides[0]);
			}

			if(dicSides.TryGetValue(strCorrectSides [1], out sprCorrectSides[1])){
			}else{
				Debug.Log("Did not get side one for this word: " + strCorrectSides[1]);
			}

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
