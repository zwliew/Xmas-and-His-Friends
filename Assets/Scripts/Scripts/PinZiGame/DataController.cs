using System.IO;
using UnityEngine;
/**
 * Controls the loading and serving of game data,
 * containing all the possible words and their sides
 */
public class DataController : MonoBehaviour {

	private string dataFileName = "PinZiData.json";

	private Word[] words;

	public void Initialize () {
		words = null;
		LoadData ();
	}

	private void LoadData () {
		string dataFilePath = null;
		if (AppUtility.platform == RuntimePlatform.Android) {
			dataFilePath = Path.Combine("jar:file://" + Application.dataPath + "!/assets/" + dataFileName);
		} else if (AppUtility.platform == RuntimePlatform.WindowsPlayer) {
			dataFilePath = Path.Combine (Application.streamingAssetsPath, dataFileName);
		} else if (AppUtility.platform == RuntimePlatform.OSXPlayer) {
			dataFilePath = Application.dataPath + "/Raw" + dataFileName;
		} else {
			Debug.LogError("Unrecognized platform! dataFilePath is set to null.");
		}

		if (File.Exists (dataFilePath)) {
			string dataAsJson = File.ReadAllText (dataFilePath);

			JsonData wordData = JsonUtility.FromJson<JsonData> (dataAsJson);
			words = wordData.words;
		} else {
			Debug.LogError ("PinZiData.json does not exist! dataFilePath is: " + dataFilePath);
			words = null;
		}
	}

	public Word GetRandomWord() {
		int wordIndex = UnityEngine.Random.Range(0, words.Length);
		return words[wordIndex];
	}
}
