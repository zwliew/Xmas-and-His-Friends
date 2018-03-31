using System.IO;
using UnityEngine;
/**
 * Controls the loading and serving of game data,
 * containing all the possible words and their sides
 */
public class DataController : MonoBehaviour {

	//private string dataFileName = "PinZiData.json";

	private Word[] words;
	private PlayerDataController playerDataController;


	public void Initialize () {
		words = null;
		LoadData ();
	}

	private void LoadData () {
		TextAsset dataAsJson = Resources.Load<TextAsset> ("PinZiPianPang/PinZiData");
		JsonData wordData = JsonUtility.FromJson<JsonData>(dataAsJson.text);
		words = wordData.words;
	}

	public Word GetRandomWord() {
		int wordIndex = UnityEngine.Random.Range(0, words.Length);
		Word thisWord = words [wordIndex];
		string[] names = new string[4];
		for(int i = 0; i < 4; i++){
			names [i] = thisWord.sides [i];
		}

		int coin = Random.Range (0,3);
		string tempName = new string ();
		switch (coin) {
		case 0:
			
			break;
		case 1:
			break;
		case 2:
			break;
		default:
			break;
			
		}

		return thisWord;
	}

	public Word[] GetAllWords(){
		return words;
	}
	public void WinThisRound(){//TODO
//		
//		playerDataController = GameObject.FindGameObjectWithTag("Persistent")//Get the PlayerDataController
//			.GetComponent<PlayerDataController>();
//		if (playerDataController)
//			Debug.Log ("playerDataController is found successfully");
//
//		playerDataController.UpdatePlayerCoins (10);
//
//		
	}
	//string dataFilePath = "jar:file:///data/app/com.hci.xmas-2/base.apk!/assets/PinZiData.json"; //Path.Combine (Application.streamingAssetsPath, dataFileName);
	//dataFilePath = "PinZiData.json";
	/*if (File.Exists (dataFilePath)) {
			string dataAsJson;
			if (AppUtility.platform == RuntimePlatform.Android) {
				WWW reader = new WWW (dataFilePath);
				while (!reader.isDone) {
				}
				dataAsJson = reader.text;
			} else {
				dataAsJson = File.ReadAllText (dataFilePath);
			}
			JsonData wordData = JsonUtility.FromJson<JsonData> (dataAsJson);
			words = wordData.words;
		} else {
			Debug.LogError ("PinZiData.json does not exist! dataFilePath is: " + dataFilePath);
			words = null;
		}
		*/
	/*
	string filePath = Path.Combine(Application.streamingAssetsPath, dataFilePath);
	if (File.Exists(filePath))
	{
		string dataAsJson = File.ReadAllText(filePath);
		JsonData wordData = JsonUtility.FromJson<JsonData>(dataAsJson);
		words = wordData.words;
	}
	else
	{
		Debug.LogError("PinZiData file does not exist!");
	}
	*/

}
