using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Class handling the updates to the player data based on
 * actions made in the shop (purchasing, etc.)
 * 
 * TODO Load up the corresponding item sprite after loading item data(see line 43)
 */
[System.Serializable]
public class ShopItemData : ItemData{//The Class for data 
	//public int cost;
	//public string fullName;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;
	public bool isBuyable;//being displayed in the shop but cannot be purchased
	public bool isOnSale;//being displayed in the shop and can be purchased
	public bool isSelected;
}

public class ShopDataController : MonoBehaviour
{
    private PlayerDataController playerDataController;
    private PlayerData playerData;

	private ShopItem curSelectedItem;


	private List<ShopItemData> itemsData;
	private ShopItemData[] itemsDataArray;
	private List<string> purchasedItemNames;
	private List<ShopItemData> purchasedItemsData;
	private List<string> displayedItemNames;
	private List<ShopItemData> displayedItemsData;

	public void Initialize()//One bug lies here: when loaded, the equipped items data is not cleared
	{
		playerDataController = GameObject.FindGameObjectWithTag("Persistent")//Get the PlayerDataController
			.GetComponent<PlayerDataController>();

		if (playerDataController) {
			Debug.Log ("playerDataController is found successfully");
		} else {
			Debug.Log ("Did not find playerDataController, please ensure that the playDataController has a tag 'persistent'");
		}


		purchasedItemsData = new List<ShopItemData>();

		playerData = playerDataController.GetPlayerData ();
		purchasedItemNames = playerData.purchasedShopItems;
		displayedItemNames = playerData.displayedShopItems;

		TextAsset dataAsJson = Resources.Load<TextAsset> ("Shop/ShopData");//Load textual data for ShopItemData
		ShopJsonData shopJsonData = JsonUtility.FromJson<ShopJsonData>(dataAsJson.text);
		itemsDataArray = shopJsonData.shopItemsData;

		itemsData = new List<ShopItemData> ();//Convert to list, fill up the rest of the ShopSItemData
		foreach (ShopItemData itemData in itemsDataArray) {
			itemsData.Add(itemData);
		}
			
		purchasedItemsData = ParseItems (purchasedItemNames, "purchasedItems");
		displayedItemsData = ParseItems (displayedItemNames, "displayedItems");

	}

	private List<ShopItemData> ParseItems(List<string> itemsStringList, string mode){//Self-explainatory method, also modifies the itemsData

		List<ShopItemData> tempItemsData = new List<ShopItemData> ();

		switch (mode) {
		case "displayedItems":
			foreach (string itemName in itemsStringList) {
				foreach (ShopItemData itemData in itemsData) {
					if (itemData.fullName.Equals (itemName)) {
						itemData.isOnSale = true;
						tempItemsData.Add (itemData);
						Debug.Log ("ItemSetDisplayedSuccessfully: " + itemData.fullName);
					}
				}
			}
			break;

		case "purchasedItems":
			foreach (string itemName in itemsStringList) {
				foreach (ShopItemData itemData in itemsData) {
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

	private ShopItemData ParseItems(string itemsString, string mode){

		ShopItemData tempItemsData = new ShopItemData ();
		switch(mode){
		case "remove":
			foreach (ShopItemData itemData in itemsData) {
				if (itemData.fullName.Equals (itemsString)) {
					itemData.isOnSale = false;
					tempItemsData = itemData;
					Debug.Log ("Item Deselected Successfully: " + itemData.fullName);
				}
			}
			break;
		case "add":
			foreach (ShopItemData itemData in itemsData) {
				if (itemData.fullName.Equals (itemsString)) {
					itemData.isOnSale = true;
					tempItemsData = itemData;
					Debug.Log ("Item Selected Successfully: " + itemData.fullName);
				}
			}
			break;
		case "purchase":
			foreach (ShopItemData itemData in itemsData) {
				if (itemData.fullName.Equals (itemsString)) {
					itemData.purchased = true;
					tempItemsData = itemData;
					Debug.Log ("Item Purchased Successfully: " + itemData.fullName);
				}
			}
			break;

		default:
			Debug.Log("ParseItems(single string version) got error");
			break;
		}

		return tempItemsData;
	}

	public List<ShopItemData> GetDisplayedItems(){
		return displayedItemsData;
	}

    public void SelectItem(ShopItem item)
    {
        curSelectedItem = item;
        item.isBuyable = ((int)item.cost <= (int)playerData.coins);
    }

    public void UnselectSelectedItem()
    {
        curSelectedItem = null;
    }

    public void PurchaseSelectedItem()
    {
		//Debug.Log (purchasedItemsData[0]);
		purchasedItemsData.Add(ParseItems(curSelectedItem.fullName, "purchase"));
		displayedItemsData.Remove(ParseItems(curSelectedItem.fullName, "remove"));
		playerDataController.UpdatePlayerCoins (-(int)curSelectedItem.cost);
		EndandSave ();
		GetComponent<ShopDisplayController> ().Initialize ();
    }

	public void EndandSave(){
		purchasedItemNames.Clear ();
		foreach (ShopItemData	 itemData in purchasedItemsData) {
			purchasedItemNames.Add (itemData.fullName.ToString ());
		}
		playerDataController.UpdatePurchasedShopItem (purchasedItemNames);

		displayedItemNames.Clear ();
		foreach (ShopItemData itemData in displayedItemsData) {
			displayedItemNames.Add (itemData.fullName.ToString ());
		}
		playerDataController.UpdateDisplayedShopItem (displayedItemNames);
	}
}
