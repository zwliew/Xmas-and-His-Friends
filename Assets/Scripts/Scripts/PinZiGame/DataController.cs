using System.IO;
using UnityEngine;
/**
 * Controls the loading and serving of game data,
 * containing all the possible words and their sides

 */
public class DataController {

	private string dataFileName = "PinZiData.json";

	private Word[] words;

	public void Initialize () {
		LoadData ();
	}

	private void LoadData () {		string dataFilePath = Path.Combine(Application.streamingAssetsPath, dataFileName);
		if (File.Exists (dataFilePath)) {
			string dataAsJson = File.ReadAllText (dataFilePath);

			JsonData wordData = JsonUtility.FromJson<JsonData> (dataAsJson);
			words = wordData.words;
		} else {
			Debug.LogError ("PinZiData.json does not exist!");
			words = new Word[0];
		}
	}

	public Word GetRandomWord() {
		int wordIndex = UnityEngine.Random.Range(0, words.Length);
		return words[wordIndex];
	}
}
