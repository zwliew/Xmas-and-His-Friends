using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorModeDataController : MonoBehaviour {

	private PlayerDataController playerDataController;

	private List<string> purchasedItemNames;
	private List<string> equippedItemNames;
	private EditorModeItemData[] itemsDataArray;//It is a bit unnecessary as I convert the itemsDataArray to itemsData(which is a list)

	public List<EditorModeItemData> itemsData;
	public List<EditorModeItemData> purchasedItemsData;
	public List<EditorModeItemData> equippedItemsData;

	private Dictionary<EditorModeItem, EditorModeItemData> itemMappingDictionary;

	public void Initialize()
	{
		playerDataController = GameObject.FindGameObjectWithTag("Persistent")//Get the PlayerDataController
			.GetComponent<PlayerDataController>();

		itemMappingDictionary = new Dictionary<EditorModeItem, EditorModeItemData> ();//Empty dictionary and lists
		purchasedItemsData = new List<EditorModeItemData>();
		equippedItemsData = new List<EditorModeItemData> ();

		equippedItemNames = playerDataController.GetPlayerData ().equippedItems;//Get the List<string> of items
		purchasedItemNames = playerDataController.GetPlayerData ().purchasedShopItems;

		TextAsset dataAsJson = Resources.Load<TextAsset> ("EditorMode/EditorModeData");//Load textual data for EditorModeItemData
		EditorModeJsonData editorModeJsonData = JsonUtility.FromJson<EditorModeJsonData>(dataAsJson.text);
		itemsDataArray = editorModeJsonData.editorModeItemsData;

		itemsData = new List<EditorModeItemData> ();//Convert to list, fill up the rest of the EditorModeItemData
		foreach (EditorModeItemData itemData in itemsDataArray) {
			itemsData.Add(itemData);
		}

		equippedItemsData = ParseItems (equippedItemNames);
		purchasedItemsData = ParseItems (purchasedItemNames);
	}

	private List<EditorModeItemData> ParseItems(List<string> itemsStringList){//Self-explainatory method

		List<EditorModeItemData> tempEquippedItemsData = new List<EditorModeItemData> ();
		foreach (string itemName in itemsStringList) {
			foreach (EditorModeItemData itemData in itemsData) {
				if (itemData.fullName.Equals (itemName)) {
					tempEquippedItemsData.Add (itemData);
					Debug.Log ("ItemFindSuccessfully" + itemData.fullName);
				}
			}
		}

		return tempEquippedItemsData;
	}

	private EditorModeItemData ParseItems(string itemsString){//Overload method for single string

		EditorModeItemData tempEquippedItemsData = new EditorModeItemData ();

		foreach (EditorModeItemData itemData in itemsData) {
			if (itemData.fullName.Equals (itemsString)) {
				tempEquippedItemsData = itemData;
				Debug.Log ("ItemFindSuccessfully" + itemData.fullName);
			}
		}
		return tempEquippedItemsData;
	}

	public List<EditorModeItemData> GetEquippedItemsData(){
		return equippedItemsData;
	}

	public void SelectItem(EditorModeItem item)
	{

		//Debug.Log ("EMDC is selecting items");
		if (item.isSelected) {
			Debug.Log ("Removing equippedItem in EditorModeData" + item.fullName);
			equippedItemsData.Remove(ParseItems(item.fullName));
		} else {
			Debug.Log ("Adding equippedItem in EditorModeData" + item.fullName);
			equippedItemsData.Add (ParseItems(item.fullName));
		}
	}
		
}
