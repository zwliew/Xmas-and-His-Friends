using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EditorModeItemData : ItemData{
	//public int cost;
	//public string fullName;
	public GameObject furniture;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;

	public bool isSelected;
	public Vector3 position;
	public Vector3 rotation;
}

public class EditorModeDataController : MonoBehaviour {

	private PlayerDataController playerDataController;

	private List<string> purchasedItemNames;
	private List<string> equippedItemNames;
	private EditorModeItemData[] itemsDataArray;//It is a bit unnecessary as I convert the itemsDataArray to itemsData(which is a list)

	public List<EditorModeItemData> itemsData;
	public List<EditorModeItemData> purchasedItemsData;
	public List<EditorModeItemData> equippedItemsData;

	public void Initialize()
	{
		playerDataController = GameObject.FindGameObjectWithTag("Persistent")//Get the PlayerDataController
			.GetComponent<PlayerDataController>();

		if (playerDataController)
			//Debug.Log ("playerDataController is found successfully");

		purchasedItemsData = new List<EditorModeItemData>();
		equippedItemsData = new List<EditorModeItemData> ();

		equippedItemNames = playerDataController.GetPlayerData ().equippedItems;//Get the List<string> of items
//		Debug.Log("equipped Items are" + equippedItemNames[0].ToString());
		purchasedItemNames = playerDataController.GetPlayerData ().purchasedShopItems;
//		Debug.Log("purchased Items are" + purchasedItemNames[0].ToString());

		TextAsset dataAsJson = Resources.Load<TextAsset> ("EditorMode/EditorModeData");//Load textual data for EditorModeItemData
		EditorModeJsonData editorModeJsonData = JsonUtility.FromJson<EditorModeJsonData>(dataAsJson.text);
		itemsDataArray = editorModeJsonData.editorModeItemsData;

		itemsData = new List<EditorModeItemData> ();//Convert to list, fill up the rest of the EditorModeItemData
		foreach (EditorModeItemData itemData in itemsDataArray) {
			itemsData.Add(itemData);
		}
		equippedItemsData = ParseItems (equippedItemNames, "equippedItems");
		purchasedItemsData = ParseItems (purchasedItemNames, "purchasedItems");
		
	}

	private List<EditorModeItemData> ParseItems(List<string> itemsStringList, string mode){//Self-explainatory method, also modifies the itemsData

		List<EditorModeItemData> tempItemsData = new List<EditorModeItemData> ();

		switch (mode) {
		case "equippedItems":
			foreach (string itemName in itemsStringList) {
				foreach (EditorModeItemData itemData in itemsData) {
					if (itemData.fullName.Equals (itemName)) {//I don't know if this part works
						itemData.equipped = true;
						tempItemsData.Add (itemData);
						Debug.Log ("ItemSetEquippedSuccessfully" + itemData.fullName);
					}
				}
			}
			break;

		case "purchasedItems":
			foreach (string itemName in itemsStringList) {
				foreach (EditorModeItemData itemData in itemsData) {
					if (itemData.fullName.Equals (itemName)) {
						itemData.purchased = true;
						tempItemsData.Add (itemData);
						Debug.Log ("ItemSetPurchasedSuccessfully" + itemData.fullName);
					}
				}
			}
			break;
		}


		return tempItemsData;
	}

	private EditorModeItemData ParseItems(string itemsString, string mode = ""){//Overload method for single string

		EditorModeItemData tempItemsData = new EditorModeItemData ();
		foreach (EditorModeItemData itemData in itemsData) {
			if (itemData.fullName.Equals (itemsString)) {
				itemData.equipped = false;
				tempItemsData = itemData;
				Debug.Log ("ItemRemovedSuccessfully" + itemData.fullName);
			}
		}

		return tempItemsData;
	}

	public List<EditorModeItemData> GetEquippedItemsData(){
		return equippedItemsData;
	}
	public List<EditorModeItemData> GetPurchasedItemsData(){
		return purchasedItemsData;
	}

	public void SelectItem(EditorModeItem item)
	{

		//Debug.Log ("EMDC is selecting items");
		if (item.isSelected) {
			Debug.Log ("Removing equippedItem in EditorModeData: " + item.fullName);
			equippedItemsData.Remove(ParseItems(item.fullName));
		} else {
			Debug.Log ("Adding equippedItem in EditorModeData: " + item.fullName);
			equippedItemsData.Add (ParseItems(item.fullName));
		}
	}

	public void EndandSave(){
		equippedItemNames.Clear ();
		foreach (EditorModeItemData itemData in equippedItemsData) {
			equippedItemNames.Add (itemData.fullName.ToString ());
		}
		playerDataController.UpdateEquippedEditorModeItem (equippedItemNames);
	}
		
}
