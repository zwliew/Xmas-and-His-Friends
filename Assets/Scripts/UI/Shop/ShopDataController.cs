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
	public bool isBuyable;
	public bool isOnSale;
	public bool isSelected;
}

public class ShopDataController : MonoBehaviour
{
    private PlayerDataController playerDataController;

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

		if (playerDataController)
			Debug.Log ("playerDataController is found successfully");

		purchasedItemsData = new List<ShopItemData>();

		PlayerData playerData = playerDataController.GetPlayerData ();
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
						//Debug.Log ("ItemSetEquippedSuccessfully: " + itemData.fullName);
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
						//Debug.Log ("ItemSetPurchasedSuccessfully: " + itemData.fullName);
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
					itemData.equipped = false;
					tempItemsData = itemData;
					Debug.Log ("Item Deselected Successfully: " + itemData.fullName);
				}
			}
			break;
		case "add":
			foreach (ShopItemData itemData in itemsData) {
				if (itemData.fullName.Equals (itemsString)) {
					itemData.equipped = true;
					tempItemsData = itemData;
					Debug.Log ("Item Selected Successfully: " + itemData.fullName);
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
    }

    public void UnselectSelectedItem()
    {
        curSelectedItem = null;
    }

    public bool PurchaseSelectedItem()
    {
        if (curSelectedItem == null)
        {
            // No item to purchase
            return false;
        }
        bool success = playerDataController.PurchaseShopItem(curSelectedItem.fullName, curSelectedItem.cost);//Do not do this. It messes up two controllers. Use UpdatePlayerCoins and UpdatePurchasedItems instead
        if (success) {
             //purchasedItems.Add(curSelectedItem);
        }
        return success;
    }
}
