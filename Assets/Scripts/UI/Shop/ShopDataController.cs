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
public class ShopItemData{//The Class for data 
	public int cost;
	public string fullName;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;
	public bool isBuyable;
	public bool isOnSale;
	public bool isSelected;
}

public class ShopDataController : MonoBehaviour
{
    private PlayerDataController playerDataController;

    private ShopItemData[] shopItems;
	private ShopItem curSelectedItem;

	public List<ShopItemData> purchasedItems;

    public void Initialize()
    {
        curSelectedItem = null;
        // TODO: Add a "Persistent" game object scene with the PlayerDataController script attached
		playerDataController = GameObject.FindGameObjectWithTag("Persistent")
                .GetComponent<PlayerDataController>();

        TextAsset dataAsJson = Resources.Load<TextAsset> ("Shop/ShopData");
        ShopJsonData shopJsonData = JsonUtility.FromJson<ShopJsonData>(dataAsJson.text);
        shopItems = shopJsonData.shopItemsData;

		foreach (ShopItemData shopItem in shopItems) {//Need two steps: One, Load the data from Json(cost, fullName, etc); Two, Load the corresponding sprites and assign to the shop item
            if (playerDataController.IsShopItemPurchased(shopItem.fullName)) {
                purchasedItems.Add(shopItem);
            }
        }
    }

    public void SelectItem(ShopItem item)
    {
		//Debug.Log("ShopDataController is selecting items");
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
        bool success = playerDataController.PurchaseShopItem(curSelectedItem.fullName, curSelectedItem.cost);
        if (success) {
            purchasedItems.Add(curSelectedItem);
        }
        return success;
    }
}
