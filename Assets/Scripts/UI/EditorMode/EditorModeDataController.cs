using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EditorModeItemData : ItemData{
	//public int cost;
	//public string fullName;
	//public bool equipped;
	//public bool purchased;
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

	public void Initialize()//One bug lies here: when loaded, the equipped items data is not cleared
	{
		playerDataController = GameObject.FindGameObjectWithTag("Persistent")//Get the PlayerDataController
			.GetComponent<PlayerDataController>();

		if (playerDataController)
			//Debug.Log ("playerDataController is found successfully");

		purchasedItemsData = new List<EditorModeItemData>();
		equippedItemsData = new List<EditorModeItemData> ();

		PlayerData playerData = playerDataController.GetPlayerData ();
		equippedItemNames = playerData.equippedItems;
		purchasedItemNames = playerData.purchasedShopItems;

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
						Debug.Log ("ItemSetEquippedSuccessfully: " + itemData.fullName);
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
						Debug.Log ("ItemSetPurchasedSuccessfully: " + itemData.fullName);
					}
				}
			}
			break;
		}


		return tempItemsData;
	}

	private EditorModeItemData ParseItems(string itemsString, string mode){

		EditorModeItemData tempItemsData = new EditorModeItemData ();
		switch(mode){
		case "remove":
			foreach (EditorModeItemData itemData in itemsData) {
				if (itemData.fullName.Equals (itemsString)) {
					itemData.equipped = false;
					tempItemsData = itemData;
					Debug.Log ("ItemRemovedSuccessfully: " + itemData.fullName);
				}
			}
			break;
		case "add":
			foreach (EditorModeItemData itemData in itemsData) {
				if (itemData.fullName.Equals (itemsString)) {
					itemData.equipped = true;
					tempItemsData = itemData;
					Debug.Log ("ItemAddedSuccessfully: " + itemData.fullName);
				}
			}
			break;
		default:
			Debug.Log("ParseItems(single string version) got error");
			break;
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
		if (item.isSelected) {//The logic here a bit confusing since displayController will select the item first
			Debug.Log ("Removing equippedItem in EditorModeData: " + item.fullName);
			Debug.Log("No of equippedItemsData before removing: " + equippedItemsData.Count);
			equippedItemsData.Remove(ParseItems(item.fullName, "remove"));
			Debug.Log("No of equippedItemsData after removing: " + equippedItemsData.Count);
		} else {
			Debug.Log ("Adding equippedItem in EditorModeData: " + item.fullName);
			equippedItemsData.Add (ParseItems(item.fullName, "add"));
		}
	}

	public void EndandSave(){
		equippedItemNames.Clear ();
		foreach (EditorModeItemData itemData in equippedItemsData) {
			equippedItemNames.Add (itemData.fullName.ToString ());
		}
		playerDataController.UpdateEquippedEditorModeItem (equippedItemNames);
		playerDataController.SavePlayerData ();
		equippedItemsData.Clear ();
//		purchasedItemsData.Clear ();
//		itemsData.Clear ();
//		purchasedItemNames.Clear ();
//		equippedItemNames.Clear();
	}
		
}
